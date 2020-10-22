using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Begin_Move : MonoBehaviour {

	public Vector3 m_vMoveDistane = Vector3.zero;
	public float m_fMoveSec = 1.5f;
	Vector3 m_vStartPosition;
	Vector3 m_vEndPosition;

	float m_StartTime;

	void Awake() {
		m_vEndPosition = transform.position;
		m_vStartPosition = m_vEndPosition + m_vMoveDistane;
	}

	// Update is called once per frame
	void Update () {
		float fPlayTime = Time.time - m_StartTime;
		float fPer = fPlayTime / m_fMoveSec;
		float fRatio = Mathf.SmoothStep(0.0f, 1.0f, fPer);
		transform.position = Vector3.Lerp (m_vStartPosition, m_vEndPosition, fRatio);
	}

	public void Reset()
	{
		m_StartTime = Time.time;
		transform.position = m_vStartPosition;
	}

	void OnEnable()
	{
		Reset ();
	}
}
