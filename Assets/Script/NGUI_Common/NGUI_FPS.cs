using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NGUI_FPS : MonoBehaviour {

	int m_nFrameCounter = 0;
	float m_tTimeCounter = 0.0f;
	float m_tRefreshTime = 0.5f;
	int m_nFPS;

	Text m_Label;

	void Awake()
	{
		m_Label = GetComponent<Text> ();
	}

	void Update()
	{
		if (m_tTimeCounter < m_tRefreshTime) {
			m_tTimeCounter += Time.deltaTime;
			++m_nFrameCounter;
		} else {
			m_nFPS = (int)(m_nFrameCounter / m_tTimeCounter);
			m_nFrameCounter = 0;
			m_tTimeCounter = 0.0f;

			m_Label.text = m_nFPS.ToString ();
		}
	}
}
