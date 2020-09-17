using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TBEasyWebCam;
using TBEasyWebCam.Setting;
using System;

using ZXing;
using ZXing.QrCode;

/// <summary>
/// 유니티 UI rawImage 컴포넌트
/// </summary>
public class QRCamScreen : MonoBehaviour {

	public DeviceCamera WebCam
	{
		get
		{ 
			return webcam;
		}
	}
	private DeviceCamera webcam;
	private RawImage rawTexture;
	ScreenOrientation orientation;

	//QR 코드 연산 관련
	private BarcodeReader barcodeReader;
	private bool decodeComplete = false;
	private bool decodeThreadChecker = false;

	private bool enableDecode = true;
	
	private Color32[] targetColorARR;
	private int posX = 0;
	private int posY = 0;
	// private int W, H;
	private Color[] orginalc;   	//the colors of the camera data.
	private int blockWidth = 480;
	
	//QR디코드 실행 빈도수 연산
	private int frameRate = 0;
	public bool PlayingQRCamera{
		#if UNITY_ANDROID && !UNITY_EDITOR
		get{return EasyWebCam.isPlaying();}
		#else
		get{return frameRate > 0;}
		#endif
	}
	public int decodeInterval = 9;

	private string completeDecodeCode = null;
	public Action<string> COMPLETE_DECODE_QR;

	Vector3 and_lRot, and_lrRot;

	public bool isPortrait = false;
	void Awake()
	{
		barcodeReader = new BarcodeReader ();
		barcodeReader.AutoRotate = false;
		barcodeReader.TryInverted = true;
		
		decodeComplete = false;
		decodeThreadChecker = false;
		enableDecode = true;
		frameRate = 0;
		decodeInterval = 9;
		completeDecodeCode = null;
	}
	
	
	// Use this for initialization
	void Start () {
		SetRawTexture();
		DrawCamTexture();
		initRotation();
	}
	
	// Update is called once per frame
	void Update () {
		SetRotation();
		if(enableDecode == false)
			return;

		if(decodeComplete == false)
		{
			frameRate++;
			int interval = frameRate % decodeInterval;
			if(interval == 0)
			{
				MakeDecodingData();
				Loom.RunAsync(TryQRDecode);
				frameRate = 0;
			}
		}
		else
		{
			// Debug.Log("Update CallBack...");
			// Debug.Log("decodeComplete : " + decodeComplete);
			// Debug.Log("decodeThreadChecker : " + decodeComplete);
			if(decodeComplete != decodeThreadChecker)
			{
				InvokeCompleteEvent();
			}
			decodeThreadChecker = decodeComplete;
		}
	}

	private void SetRawTexture()
	{
		if(rawTexture != null)
			return;

		RawImage rawImage = GetComponent<RawImage>();
		if(rawImage != null){
			if(isPortrait)
			{
				Canvas parentCanvas = GetComponentInParent<Canvas>();
				// Debug.Log(parentCanvas.scaleFactor);
				float scaleFactor = parentCanvas.scaleFactor;
				float width = parentCanvas.pixelRect.size.y / scaleFactor;
				float height = parentCanvas.pixelRect.size.x / scaleFactor;
				Debug.Log(width);
				Debug.Log(height);
				rawImage.rectTransform.sizeDelta = new Vector2(width, height);
				// rawImage.texture.height = (int)height;
				rawImage.transform.localEulerAngles = new Vector3(0, 0, -90f);
			}

			rawTexture = rawImage;
		}

		// if(rawTexture.texture.width)
	}

	private void DrawCamTexture()
	{
		if(rawTexture == null)
			return;
		#if UNITY_ANDROID && !UNITY_EDITOR
		webcam = new DeviceCamera(1280, 720, true);
		#else
		webcam = new DeviceCamera(1280, 720, false);
		#endif
		rawTexture.texture = webcam.preview;
		
		int W = webcam.Width();					// get the image width
		int H = webcam.Height();			// get the image height
		
		posX = ((W-blockWidth)>>1);//
		posY = ((H-blockWidth)>>1);

		blockWidth = (int)((Math.Min(W,H)/3f) *2);
		targetColorARR = new Color32[blockWidth * blockWidth];
		
		webcam.Play();
		// ResetAutoFocus();
	}

	private void MakeDecodingData()
	{
		if(targetColorARR == null)
		{
			targetColorARR= new Color32[blockWidth * blockWidth];
		}

		// Debug.Log("posx : " + posx + ", posy : " + posy + ", blockWidth : " + blockWidth);
		
		orginalc = webcam.GetPixels(posX, posY, blockWidth, blockWidth);// get the webcam image colors

		// rawTexture.
		//convert the color(float) to color32 (byte)
		for(int i=0;i!= blockWidth;i++)
		{
			for(int j = 0;j!=blockWidth ;j++)
			{
				targetColorARR[i + j*blockWidth].r = (byte)( orginalc[i + j*blockWidth].r*255);
				targetColorARR[i + j*blockWidth].g = (byte)(orginalc[i + j*blockWidth].g*255);
				targetColorARR[i + j*blockWidth].b = (byte)(orginalc[i + j*blockWidth].b*255);
				targetColorARR[i + j*blockWidth].a = 255;
			}
		}
	}

	/// <summary>
	/// 디코드 직전 데이터가 완성되면 QR디코드 시도
	/// </summary>
	private void TryQRDecode()
	{
		try
		{
			Result data;
			data = barcodeReader.Decode(targetColorARR,blockWidth,blockWidth);//start decode
			if (data != null) // if get the result success
			{
				decodeComplete = true;
				completeDecodeCode = data.Text;
				//Debug.Log("TryQRDecode decode success");
			}
		}
		catch (Exception e)
		{
			Debug.Log("QR Decode Error : " + e.Data.ToString());
			decodeComplete = false;
			//resumeText.text = "Decode Error : " + e.Data.ToString();
		}
	}

	private void InvokeCompleteEvent()
	{
		if(COMPLETE_DECODE_QR != null)
		{
			COMPLETE_DECODE_QR(completeDecodeCode);			
		}
		completeDecodeCode = null;
	}
	
	public void ResetAutoFocus()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(EasyWebCam.isPlaying())
		{
			//EasyWebCam.setFocusMode(FocusMode.Off);
			Debug.Log("EasyWebCam.setFocusMode(FocusMode.AutoFocus)");
			EasyWebCam.setFocusMode(FocusMode.AutoFocus);
		}
		#endif
	}

	public void Play()
	{
		webcam.Play();
		#if UNITY_ANDROID && !UNITY_EDITOR
		//EasyWebCam.setFocusMode(FocusMode.Off);
		EasyWebCam.setFocusMode(FocusMode.AutoFocus);
		#endif
	}

	public void Stop()
	{
		webcam.Stop();
	}

	public void StartDecode()
	{
		//Debug.Log("StartDecode ####");
		enableDecode = true;
		decodeComplete = false;
		decodeThreadChecker = false;
	}

	public void StopDecode()
	{
		//Debug.Log("StopDecode ####");
		enableDecode = false;
		decodeComplete = true;
		decodeThreadChecker = true;
	}

	private void SetRotation()
	{
		if (orientation != Screen.orientation) {
			updateRotation();
			orientation = Screen.orientation;
		}
	}

	private void initRotation()
	{
		assignAngleVectors ();// get the rotation by device model

		orientation = Screen.orientation;
		#if UNITY_ANDROID||UNITY_IOS
		updateRotation();
		#endif
	}

	void updateRotation()
	{
		if (Screen.orientation == ScreenOrientation.Landscape||
				Screen.orientation == ScreenOrientation.LandscapeLeft) {
			#if UNITY_ANDROID
			transform.localEulerAngles = and_lRot;
			#elif UNITY_IOS
			transform.localEulerAngles = and_lRot;
			#endif
		}
		else if(Screen.orientation == ScreenOrientation.LandscapeRight)
		{
			#if UNITY_ANDROID
			transform.localEulerAngles = and_lrRot;
			#elif UNITY_IOS

			transform.localEulerAngles = and_lrRot;
			#endif
		}
	}

	void assignAngleVectors()
	{
		and_lrRot = new Vector3(0,0,180);
		and_lRot = new Vector3(0,0,0);
	}
}
