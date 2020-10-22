using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Ctrl_Position : NGUI_Ctrl {

	RectTransform m_RT;
	Vector3 m_vDefaultPosition;
	Vector3 m_vStartPosition = Vector3.zero;
	Vector3 m_vEndPosition = Vector3.zero;
	public Vector3 m_vMovePostion = Vector3.zero;
	public float m_fRunTime = 1.0f;
	[ReadOnly]
	public float m_fCurTime = 0.0f;
	bool m_bActive = false;

	void Awake()
	{
		m_RT = gameObject.GetComponent<RectTransform> (); 
		m_vDefaultPosition = m_RT.anchoredPosition3D;
		m_vStartPosition = m_RT.anchoredPosition3D;
		m_vEndPosition = m_vStartPosition + m_vMovePostion;
	}

	void OnDisable()
	{
		m_bActive = false;
	}

	// Update is called once per frame
	void Update () {
		if (m_bActive == false)
			return;

		m_fCurTime += Time.deltaTime;
		float fRatio = Mathf.Min(1.0f, m_fCurTime / m_fRunTime);
		float x = m_vDefaultPosition.x;
		if(m_vStartPosition.x != m_vEndPosition.x)
			x = Mathf.InverseLerp (m_vStartPosition.x, m_vEndPosition.x, fRatio);
		float y = m_vDefaultPosition.y;
		if(m_vStartPosition.y != m_vEndPosition.y)
			y = Mathf.InverseLerp (m_vStartPosition.y, m_vEndPosition.y, fRatio);
		float z = m_vDefaultPosition.z;
		if(m_vStartPosition.z != m_vEndPosition.z)
			z = Mathf.InverseLerp (m_vStartPosition.z, m_vEndPosition.z, fRatio);
		m_RT.anchoredPosition3D = new Vector3 (x, y, z);
	}

	override public void OnRun()
	{
		if (m_RT == null)
			m_RT = gameObject.GetComponent<RectTransform> ();

		// reset
		m_RT.anchoredPosition3D = m_vDefaultPosition;

		// run
		m_bActive = true;
		m_fCurTime = 0.0f;
	}
}
