using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI_Kerisdiagramm : MonoBehaviour
{
    public Image m_SpriteCircle;
    public GameObject m_Desc;

    float m_CurRatio = 0.0f;

    Dictionary<GameObject, Vector2> m_Map = new Dictionary<GameObject, Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Clear()
    {
        foreach (var tmp in m_Map)
            Destroy(tmp.Key);

        m_Map.Clear();
    }

    public GameObject Add(float ratio, Color color)
    {
        GameObject tmp = Instantiate(m_SpriteCircle.gameObject, gameObject.transform) as GameObject;
        tmp.GetComponent<Image>().fillAmount = ratio;
        tmp.GetComponent<Image>().color = color;
        tmp.transform.rotation = Quaternion.AngleAxis(m_CurRatio * -360, Vector3.forward);
        m_CurRatio += ratio;
        return tmp;
    }

    public IEnumerator Add_(float ratio, Color color)
    {
        GameObject tmp = Instantiate(m_SpriteCircle.gameObject, gameObject.transform) as GameObject;
        yield return null; // 생성 직후 fillamount 입력시 값이 변경 안되는 경우가 있음
        tmp.GetComponent<Image>().fillAmount = ratio;
        tmp.GetComponent<Image>().color = color;
        tmp.transform.rotation = Quaternion.AngleAxis(m_CurRatio * -360, Vector3.forward);
        m_CurRatio += ratio;
    }

    public IEnumerator Add_(float ratio, Color color, string desc)
    {
        GameObject tmp = Instantiate(m_SpriteCircle.gameObject, gameObject.transform) as GameObject;
        GameObject Desc = Instantiate(m_Desc, gameObject.transform) as GameObject;
        yield return new WaitForEndOfFrame(); //yield return null; // 생성 직후 fillamount 입력시 값이 변경 안되는 경우가 있음
        tmp.name = desc;
        tmp.GetComponent<Image>().fillAmount = ratio;
        tmp.GetComponent<Image>().color = color;
        tmp.transform.rotation = Quaternion.AngleAxis(m_CurRatio * -360, Vector3.forward);

        m_Map.Add(tmp, new Vector2(m_CurRatio, m_CurRatio + ratio));

        Desc.transform.SetParent(tmp.transform);
        Desc.GetComponentInChildren<Text>().text = desc;
        Desc.transform.rotation = Quaternion.AngleAxis((m_CurRatio + (ratio * 0.5f)) * -360, Vector3.forward);

        m_CurRatio += ratio;

        yield break;
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
        Image tmp = obj.GetComponent<Image>();
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
