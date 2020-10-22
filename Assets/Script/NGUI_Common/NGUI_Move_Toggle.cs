using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Move_Toggle : MonoBehaviour {

	public float m_fMoveSec = 1.5f;
	public Vector3 m_vStartLocalPosition = Vector3.zero;
	public Vector3 m_vEndLocalPosition = Vector3.zero;

	[ReadOnly]
	public bool m_bReverse = false;

	float m_StartTime;
	bool m_bActive = false;

	// Update is called once per frame
	void Update () {
		if (m_bActive == false)
			return;

		float fPlayTime = Time.time - m_StartTime;
		float fPer = fPlayTime / m_fMoveSec;
		float fRatio = Mathf.SmoothStep (0.0f, 1.0f, fPer);

		if(m_bReverse)
			transform.localPosition = Vector3.Lerp (m_vEndLocalPosition, m_vStartLocalPosition, fRatio);
		else
			transform.localPosition = Vector3.Lerp (m_vStartLocalPosition, m_vEndLocalPosition, fRatio);

		m_bActive = (fRatio != 1.0f);
		if (m_bActive == false)
			m_bReverse = !m_bReverse;
	}

	public void OnToggle()
	{
		if (m_bReverse)
			OnHide ();
		else
			OnShow ();
	}

	public void OnShow()
	{
		m_bActive = true;
		m_bReverse = false;
		m_StartTime = Time.time;
	}

	public void OnHide()
	{
		m_bActive = true;
		m_bReverse = true;
		m_StartTime = Time.time;
	}
}
