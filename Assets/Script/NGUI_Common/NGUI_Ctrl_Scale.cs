using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Ctrl_Scale : NGUI_Ctrl {

	Vector3 m_vDefaultScale;
	public Vector3 m_vStartScale = Vector3.one;
	public Vector3 m_vEndScale = Vector3.one;
	public float m_fRunTime = 1.0f;
	[ReadOnly]
	public float m_fCurTime = 0.0f;
	bool m_bActive = false;

	void Awake()
	{
		m_vDefaultScale = gameObject.transform.localScale;
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
		float x = m_vDefaultScale.x;
		if(m_vStartScale.x != m_vEndScale.x)
			x = Mathf.InverseLerp (m_vStartScale.x, m_vEndScale.x, fRatio);
		float y = m_vDefaultScale.y;
		if(m_vStartScale.y != m_vEndScale.y)
			y = Mathf.InverseLerp (m_vStartScale.y, m_vEndScale.y, fRatio);
		float z = m_vDefaultScale.z;
		if(m_vStartScale.z != m_vEndScale.z)
			z = Mathf.InverseLerp (m_vStartScale.z, m_vEndScale.z, fRatio);
		gameObject.transform.localScale = new Vector3 (x, y, z);
	}

	override public void OnRun()
	{
		// reset
		gameObject.transform.localScale = m_vDefaultScale;

		// run
		m_bActive = true;
		m_fCurTime = 0.0f;
		gameObject.transform.localScale = m_vStartScale;
	}
}
