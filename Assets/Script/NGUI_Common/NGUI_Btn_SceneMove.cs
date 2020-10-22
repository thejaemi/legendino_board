using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NGUI_Btn_SceneMove : MonoBehaviour {

	public string m_sSceneName;
	public bool m_bUseLoading = false;

	public void Run()
	{
		SceneManager.LoadScene (m_sSceneName);
	}
}
