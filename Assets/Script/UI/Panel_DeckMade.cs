using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_DeckMade : MonoBehaviour
{
    public Image[] m_Sprite_Cards;
    public Text m_Label_Attack;
    public Text m_Label_Defence;
    public Text m_Label_Counter;
    public Text m_Label_Special;
    public GameObject m_Button;

    private void OnEnable()
    {
        m_Sprite_Cards[0].sprite = CM_Singleton<GameData>.instance.m_Atlas_Card.GetSprite(string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Card_Amulet));
        m_Sprite_Cards[1].sprite = CM_Singleton<GameData>.instance.m_Atlas_Card.GetSprite(string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Card_Ring));
        m_Sprite_Cards[2].sprite = CM_Singleton<GameData>.instance.m_Atlas_Card.GetSprite(string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Card_Glove));
        m_Sprite_Cards[3].sprite = CM_Singleton<GameData>.instance.m_Atlas_Card.GetSprite(string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Card_Map));
        m_Sprite_Cards[4].sprite = CM_Singleton<GameData>.instance.m_Atlas_Card.GetSprite(string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Card_Belt));

        m_Label_Attack.text = string.Format("{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Attack);
        m_Label_Defence.text = string.Format("{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Defence);
        m_Label_Counter.text = string.Format("{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Counter);
        m_Label_Special.text = string.Format("{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Special);

        m_Button.SetActive(false);
        Invoke("ShowButton", 1.0f);
    }

    void ShowButton()
    {
        m_Button.SetActive(true);
    }
}
