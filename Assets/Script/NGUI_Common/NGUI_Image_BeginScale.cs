using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Image_BeginScale : MonoBehaviour {

	RectTransform m_Rect;
	public float m_fStartScale = 0.1f;
	public float m_fTickAddScale = 0.1f;
	public float m_fEndScale = 1.0f;
	float m_fCurScale;

	void Awake()
	{
		m_Rect = GetComponent<RectTransform> ();
	}

	void OnEnable()
	{
		m_fCurScale = m_fStartScale;
		StartCoroutine (Run());
	}

	// Use this for initialization
	IEnumerator Run () {
		while (true) {
			
			if (m_fCurScale >= m_fEndScale)
				break;

			m_fCurScale += m_fTickAddScale;
			m_Rect.localScale = new Vector3(1.0f, m_fCurScale, 1.0f);
			
			yield return null;
		}
	}
}
