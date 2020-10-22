using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NGUI_Label_SceneName : MonoBehaviour {

	void Awake()
	{
		GetComponent<Text> ().text = SceneManager.GetActiveScene ().name;
	}
}
