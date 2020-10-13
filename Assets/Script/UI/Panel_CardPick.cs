using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_CardPick : MonoBehaviour
{
    GameData m_GameData;

    public GameObject m_UI_Pick;
    public UILabel m_Label_Count;
    public UISprite[] m_Sprite_Button;
    public int[] m_Button_Index = new int[3];

    List<int> m_List_Deck;
    int m_CurRound;
    int m_TotalRound;
    public GameObject m_UI_Change;
    public UISprite m_Sprite_CardOld;
    public UISprite m_Sprite_CardNew;
    int m_Id_CardNew;
    int m_Type;

    public GameObject m_UI_DeckMade;

    public UISprite[] m_Sprite_Deck;
    public UILabel m_Label_Attack;
    public UILabel m_Label_Defence;
    public UILabel m_Label_Counter;
    public UILabel m_Label_Special;
    public UISprite[] m_Sprite_MadeDeck;

    // Start is called before the first frame update
    void Start()
    {
        m_GameData = CM_Singleton<GameData>.instance;
        m_UI_Pick.SetActive(true);
        m_UI_Change.SetActive(false);
        m_UI_DeckMade.SetActive(false);
        ReflashInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReflashInfo()
    {
        if (m_GameData.m_MyInfo.m_Equip_Amulet > 0)
            m_Sprite_Deck[0].spriteName = string.Format("card_{0}", m_GameData.m_MyInfo.m_Equip_Amulet);
 
        if (m_GameData.m_MyInfo.m_Equip_Ring > 0)
            m_Sprite_Deck[1].spriteName = string.Format("card_{0}", m_GameData.m_MyInfo.m_Equip_Ring);

        if (m_GameData.m_MyInfo.m_Equip_Glove > 0)
            m_Sprite_Deck[2].spriteName = string.Format("card_{0}", m_GameData.m_MyInfo.m_Equip_Glove);

        if (m_GameData.m_MyInfo.m_Equip_Map > 0)
            m_Sprite_Deck[3].spriteName = string.Format("card_{0}", m_GameData.m_MyInfo.m_Equip_Map);

        if (m_GameData.m_MyInfo.m_Equip_Belt > 0)
            m_Sprite_Deck[4].spriteName = string.Format("card_{0}", m_GameData.m_MyInfo.m_Equip_Belt);

        m_Label_Attack.text = string.Format("+ {0}", m_GameData.m_MyInfo.m_Attack);
        m_Label_Defence.text = string.Format("+ {0}", m_GameData.m_MyInfo.m_Defence);
        m_Label_Counter.text = string.Format("+ {0}", m_GameData.m_MyInfo.m_Counter);
        m_Label_Special.text = string.Format("+ {0}", m_GameData.m_MyInfo.m_Special);
    }

    public void SetPickCards(List<int> DeckList)
    {
        m_List_Deck = DeckList;
        m_CurRound = 0;
        m_TotalRound = DeckList.Count / 3;

        SetPickCards();
    }

    public void SetPickCards()
    {
        ++m_CurRound;
        m_Label_Count.text = string.Format("({0}/{1})", m_CurRound, m_TotalRound);

        m_Sprite_Button[0].spriteName = string.Format("card_{0}", m_List_Deck[0]);
        m_Button_Index[0] = m_List_Deck[0];
        m_List_Deck.RemoveAt(0);

        m_Sprite_Button[1].spriteName = string.Format("card_{0}", m_List_Deck[0]);
        m_Button_Index[1] = m_List_Deck[0];
        m_List_Deck.RemoveAt(0);

        m_Sprite_Button[2].spriteName = string.Format("card_{0}", m_List_Deck[0]);
        m_Button_Index[2] = m_List_Deck[0];
        m_List_Deck.RemoveAt(0);
    }

    public void CardPick()
    {
        int n = int.Parse(UIButton.current.name);
        if(n < m_Button_Index.Length)
        {
            Card card = m_GameData.m_Table_Card.m_Dic[m_Button_Index[n]];
            switch(card.m_Attribute)
            {
                case 1:
                    if (m_GameData.m_MyInfo.m_Equip_Amulet == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Equip_Amulet, card);
                        return;
                    }
                    break;

                case 2:
                    if (m_GameData.m_MyInfo.m_Equip_Ring == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Equip_Ring, card);
                        return;
                    }
                    break;

                case 3:
                    if (m_GameData.m_MyInfo.m_Equip_Glove == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Equip_Glove, card);
                        return;
                    }
                    break;

                case 4:
                    if (m_GameData.m_MyInfo.m_Equip_Map == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Equip_Map, card);
                        return;
                    }
                    break;

                case 5:
                    if (m_GameData.m_MyInfo.m_Equip_Belt == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Equip_Belt, card);
                        return;
                    }
                    break;
            }

            ReflashInfo();
            if (m_CurRound < m_TotalRound)
                SetPickCards();
            else
            {
                m_UI_Pick.SetActive(false);
                m_UI_Change.SetActive(false);
                Invoke("ShowMadeDeck", 1.0f);
            }
        }
    }

    public void ShowChoice(int Old, Card card)
    {
        m_Type = card.m_Attribute;
        m_Id_CardNew = card.m_Id;

        m_UI_Pick.SetActive(false);
        m_UI_Change.SetActive(true);
        m_Sprite_CardOld.spriteName = string.Format("card_{0}", Old);
        m_Sprite_CardNew.spriteName = string.Format("card_{0}", card.m_Id);
    }

    public void Cancel()
    {
        if (m_CurRound < m_TotalRound)
        {
            m_UI_Pick.SetActive(true);
            m_UI_Change.SetActive(false);
            SetPickCards();
        }
        else
        {
            m_UI_Pick.SetActive(false);
            m_UI_Change.SetActive(false);
            Invoke("ShowMadeDeck", 1.0f);
        }
    }

    public void Change()
    {
        m_GameData.m_MyInfo.Set_Card(m_Type, m_Id_CardNew);

        ReflashInfo();
        if (m_CurRound < m_TotalRound)
        {
            m_UI_Pick.SetActive(true);
            m_UI_Change.SetActive(false);
            SetPickCards();
        }
        else
        {
            m_UI_Pick.SetActive(false);
            m_UI_Change.SetActive(false);
            Invoke("ShowMadeDeck", 1.0f);
        }
    }

    public void ShowMadeDeck()
    {
        m_UI_Change.SetActive(false);
        m_UI_Pick.SetActive(false);
        gameObject.SetActive(false);
        m_UI_DeckMade.SetActive(true);
    }
}
