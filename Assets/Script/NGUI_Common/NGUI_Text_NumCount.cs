using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NGUI_Text_NumCount : MonoBehaviour {

	[ReadOnly]
	public int m_nCurCount;
	[ReadOnly]
	public int m_nCount;

	Text m_Text;
	bool m_IsCounting = false;

	void Awake()
	{
		m_Text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (m_nCount != m_nCurCount)
			StartCoroutine (Counting());
	}

	IEnumerator Counting()
	{
		if (m_IsCounting)
			yield break;

		while (true) {
			m_IsCounting = true;

			if (Mathf.Abs (m_nCount - m_nCurCount) <= 9) {
				m_nCurCount = m_nCount;
				m_Text.text = m_nCount.ToString ();
				break;
			} else {
				int nGab = m_nCount - m_nCurCount;
				m_nCurCount += (int)(nGab * 0.1f);
				m_Text.text = m_nCurCount.ToString ();
			}

			yield return null;
		}

		m_IsCounting = false;
	}
}
