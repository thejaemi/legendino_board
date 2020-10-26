using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRVerificationManager : MonoBehaviour
{
    public QRCamScreen qrCamera;

    private bool m_finishedDecode = false;
    private const int focusInterval = 1500;

    void Awake()
    {
        //CM_Singleton<Table_QRCode>.instance.Load();
        qrCamera.COMPLETE_DECODE_QR = ScanFinishedQRCode;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitQRCameraPlaying());
    }

    private IEnumerator WaitQRCameraPlaying()
    {
        while(qrCamera.PlayingQRCamera == false)
        {
            yield return new WaitForEndOfFrame();
        }
		m_finishedDecode = false;
		StartCoroutineAutoFocusChecker();
    }

	private void StartCoroutineAutoFocusChecker()
	{
		StopCoroutine("CoroutineCheckInvokeAutoFocus");
		StartCoroutine("CoroutineCheckInvokeAutoFocus");
	}

	private void StopCoroutineAutoFocusChecker()
	{
		StopCoroutine("CoroutineCheckInvokeAutoFocus");
	}

	private IEnumerator CoroutineCheckInvokeAutoFocus()
	{
		while(m_finishedDecode == false)
		{
			yield return YieldHelper.waitForSeconds(focusInterval);

			if(m_finishedDecode == false)
				qrCamera.ResetAutoFocus();
		}
	}

    private void ScanFinishedQRCode(string data)
    {
        m_finishedDecode = true;
        Debug.Log(data);
        char[] separator = { '\'' };
        string[] decodeCodes = data.Split(separator);
        int codeSection = 7;
        string code = decodeCodes[codeSection];
        int dinoIndex = CM_Singleton<Table_QRCode>.instance.GetDinoIndex(code);

        GameObject.Find("Main").GetComponent<Scan>().ScanDino(code, dinoIndex);
    }
}
