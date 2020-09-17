using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoField : MonoBehaviour
{
    public UISprite dinoMedal;

    private Dinosaur m_dinosaur;

    private bool m_rotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDinoMedal(Dinosaur _dinosaur)
    {
        m_dinosaur = _dinosaur;
        SetMedal();
    }

    private void SetMedal()
    {
        dinoMedal.spriteName = string.Format("dino_{0}", m_dinosaur.index);
    }

    public void RollingMedal()
    {
        m_rotate = true;
        StartCoroutine(RotateMedal());
    }

    private IEnumerator RotateMedal()
    {
        while(m_rotate)
        {
            yield return YieldHelper.waitForEndOfFrame();
            dinoMedal.transform.Rotate(Vector3.back * Time.deltaTime * 3000f);
        }
    }

    public void SelectDinoAction()
    {
        m_rotate = false;
    }

    // void Update()
    // {
    //     dinoMedal.transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
    // }

    private void ActionAttack()
    {

    }

    



    public void OnClickDinoField()
    {
        
    }

}
