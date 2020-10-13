using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_DeckMade : MonoBehaviour
{
    public UISprite[] m_Sprite_Cards;
    public UILabel m_Label_Attack;
    public UILabel m_Label_Defence;
    public UILabel m_Label_Counter;
    public UILabel m_Label_Special;

    private void OnEnable()
    {
        m_Sprite_Cards[0].spriteName = string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Equip_Amulet);
        m_Sprite_Cards[1].spriteName = string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Equip_Ring);
        m_Sprite_Cards[2].spriteName = string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Equip_Glove);
        m_Sprite_Cards[3].spriteName = string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Equip_Map);
        m_Sprite_Cards[4].spriteName = string.Format("card_{0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Equip_Belt);


        m_Label_Attack.text = string.Format("+ {0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Attack);
        m_Label_Defence.text = string.Format("+ {0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Defence);
        m_Label_Counter.text = string.Format("+ {0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Counter);
        m_Label_Special.text = string.Format("+ {0}", CM_Singleton<GameData>.instance.m_MyInfo.m_Special);
    }
}
