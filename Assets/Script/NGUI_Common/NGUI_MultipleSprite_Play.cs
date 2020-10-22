using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NGUI_MultipleSprite_Play : MonoBehaviour {

	public GameObject m_Object;
	Image m_Image;
	public Sprite[] m_Sources;
	int m_nCurIndex = 0;

	void Awake()
	{
		if(m_Image == null)
			m_Image = m_Object.GetComponent<Image> ();
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Next", 0.0f, 0.3f);
	}

	void OnDestroy()
	{
		CancelInvoke ("Next");
	}

	void Next() {
		++m_nCurIndex;
		if (m_nCurIndex == m_Sources.Length)
			m_nCurIndex = 0;

		m_Image.sprite = m_Sources [m_nCurIndex];
	}
}
