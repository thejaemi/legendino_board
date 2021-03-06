﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Kerisdiagramm : MonoBehaviour
{
    public UISprite m_SpriteCircle;
    public GameObject m_Desc;

    float m_CurRatio = 0.0f;

    Dictionary<GameObject, Vector2> m_Map = new Dictionary<GameObject, Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Add_(0.2f, Color.red));
        //StartCoroutine(Add_(0.3f, Color.blue));
        //StartCoroutine(Add_(0.1f, Color.green));
        //StartCoroutine(Add_(0.2f, Color.yellow));
        //StartCoroutine(Add_(0.1f, Color.cyan));
        //StartCoroutine(Add_(0.1f, Color.gray));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Add(float ratio, Color color)
    {
        GameObject tmp = Instantiate(m_SpriteCircle.gameObject, gameObject.transform) as GameObject;
        tmp.GetComponent<UISprite>().fillAmount = ratio;
        tmp.GetComponent<UISprite>().color = color;
        tmp.transform.rotation = Quaternion.AngleAxis(m_CurRatio * -360, Vector3.forward);
        m_CurRatio += ratio;
        return tmp;
    }

    public IEnumerator Add_(float ratio, Color color)
    {
        GameObject tmp = Instantiate(m_SpriteCircle.gameObject, gameObject.transform) as GameObject;
        yield return null; // 생성 직후 fillamount 입력시 값이 변경 안되는 경우가 있음
        tmp.GetComponent<UISprite>().fillAmount = ratio;
        tmp.GetComponent<UISprite>().color = color;
        tmp.transform.rotation = Quaternion.AngleAxis(m_CurRatio * -360, Vector3.forward);
        m_CurRatio += ratio;
    }

    public IEnumerator Add_(float ratio, Color color, string desc)
    {
        GameObject tmp = Instantiate(m_SpriteCircle.gameObject, gameObject.transform) as GameObject;
        GameObject Desc = Instantiate(m_Desc, gameObject.transform) as GameObject;
        yield return null; // 생성 직후 fillamount 입력시 값이 변경 안되는 경우가 있음
        tmp.name = desc;
        tmp.GetComponent<UISprite>().fillAmount = ratio;
        tmp.GetComponent<UISprite>().color = color;
        tmp.transform.rotation = Quaternion.AngleAxis(m_CurRatio * -360, Vector3.forward);

        m_Map.Add(tmp, new Vector2(m_CurRatio, m_CurRatio + ratio));

        Desc.GetComponentInChildren<UILabel>().text = desc;
        Desc.transform.rotation = Quaternion.AngleAxis((m_CurRatio + (ratio * 0.5f)) * -360, Vector3.forward);

        m_CurRatio += ratio;
    }

    /// <summary>
    /// 당첨된 그래프의 색 반환
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public string Win(float f)
    {
        foreach (var tmp in m_Map)
        {
            if (tmp.Value.x <= f && f <= tmp.Value.y)
            {
                ColorChange(tmp.Key);
                return tmp.Key.name;
            }
        }

        return "";
    }

    IEnumerator ColorChange(GameObject obj)
    {
        UISprite tmp = obj.GetComponent<UISprite>();
        Color backup = tmp.color;

        tmp.color = Color.white;

        yield return new WaitForEndOfFrame();

        tmp.color = backup;

        yield return new WaitForEndOfFrame();

        tmp.color = Color.white;

        yield return new WaitForEndOfFrame();
        tmp.color = backup;

        yield return new WaitForEndOfFrame();

        tmp.color = Color.white;

        yield return new WaitForEndOfFrame();
        tmp.color = backup;

    }
}
