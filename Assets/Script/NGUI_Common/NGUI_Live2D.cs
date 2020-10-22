using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Live2D : MonoBehaviour {

	RectTransform m_Rect;
	public float m_fRatio = 1.0f;
	public float m_fSec = 1.0f;
	public bool m_bUseX = true;
	public bool m_bUseY = false;
	public bool m_bUseZ = false;

	void Awake()
	{
		m_Rect = GetComponent<RectTransform> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (Run());
	}

	// Use this for initialization
	IEnumerator Run () {
		yield return new WaitForSeconds (Random.Range(0.0f, 0.5f));

		Vector3 m_OriPos = m_Rect.position;
		float fSec = 0.0f;
		float fValue;
		while (true) {
			fSec += Time.deltaTime;
			fValue = Mathf.PingPong (fSec, m_fSec) * m_fRatio;
			if (m_bUseZ)
				m_Rect.position = new Vector3 (m_Rect.position.x, m_Rect.position.y, m_OriPos.z + fValue);
			if (m_bUseY)
				m_Rect.position = new Vector3 (m_Rect.position.x, m_OriPos.y + fValue, m_Rect.position.z);
			if (m_bUseX)
				m_Rect.position = new Vector3 (m_OriPos.x + fValue, m_Rect.position.y, m_Rect.position.z);
		
			yield return null;
		}
	}
}
