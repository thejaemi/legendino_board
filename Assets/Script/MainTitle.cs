using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TBEasyWebCam;

public class MainTitle : MonoBehaviour
{
    public const string CAMERA_ACCESS_PERMISSION = "android.permission.CAMERA";
    public SimpleAnimation_Stop m_AniCtrl;
    public GameObject m_Button;

    private void Awake()
    {
        if (CM_Singleton<GameData>.instance)
            Debug.Log("Create GameData");
    }

    // Start is called before the first frame update
    void Start()
    {
        bool granted = CameraPermissionsController.IsPermissionGranted(CAMERA_ACCESS_PERMISSION);
        if(!granted)
            RequestCameraPermission();
    }

    private void RequestCameraPermission()
    {
        CameraPermissionsController.RequestPermission(new[] { EasyWebCam.CAMERA_PERMISSION }, new AndroidPermissionCallback(
            grantedPermission =>
            {
                // The permission was successfully granted, restart the change avatar routine
            },
            deniedPermission =>
            {
                // The permission was denied
            },
            deniedPermissionAndDontAskAgain =>
            {
                // The permission was denied, and the user has selected "Don't ask again"
                // Show in-game pop-up message stating that the user can change permissions in Android Application Settings
                // if he changes his mind (also required by Google Featuring program)
            }));
    }

    public void OnClickTitle()
    {
        m_Button.SetActive(false);

        m_AniCtrl.Play();

        Invoke("SceneMove", 2.0f);
    }

    void SceneMove()
    {
        SceneManager.LoadScene("Lobby");
    }
}
