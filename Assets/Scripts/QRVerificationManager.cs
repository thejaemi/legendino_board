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
        // qrCamera.COMPLETE_DECODE_QR = ScanFinishedQRCode;
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

    private void ScanFinishedQRCode(string code)
    {
        char[] separator = { '\'' };
        string[] decodeCodes = code.Split(separator);
        int codeSection = 7;

        // if(decodeCodes.Length > codeSection)
            


    }
}
