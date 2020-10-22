using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NGUI_Image_BeginAlpha : MonoBehaviour {

	Image m_Image;

	void Awake()
	{
		m_Image = GetComponent<Image> ();
	}

	void OnEnable()
	{
		StartCoroutine (Run());
	}

	IEnumerator Run()
	{
		float fSec = 0.0f;
		while (true) {
			fSec += Time.deltaTime;
			if (fSec > 1.0f)
				break;

			Color clr = m_Image.color;
			clr.a = clr.a + 5;
			m_Image.color = clr;

			yield return null;
		}

		fSec = 0.0f;
		while (true) {
			fSec += Time.deltaTime;
			if (fSec > 1.0f)
				break;

			Color clr = m_Image.color;
			clr.a = clr.a - 5;
			m_Image.color = clr;

			yield return null;
		}

		yield break;
	}
}
