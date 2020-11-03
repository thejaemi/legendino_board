using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CardPick : MonoBehaviour
{
    GameData m_GameData;

    public GameObject m_UI_Pick;
    public Text m_Label_Count;
    public Image[] m_Sprite_Button;
    public int[] m_Button_Index = new int[3];

    List<int> m_List_Deck;
    int m_CurRound;
    int m_TotalRound;
    public GameObject m_UI_Change;
    public Image m_Sprite_CardOld;
    public Image m_Sprite_CardNew;
    int m_Id_CardNew;
    int m_Type;

    public GameObject m_UI_DeckMade;

    public Image[] m_Sprite_Deck;
    public Text m_Label_Attack;
    public Text m_Label_Defence;
    public Text m_Label_Counter;
    public Text m_Label_Special;

    private void Awake()
    {
        m_GameData = CM_Singleton<GameData>.instance;

        m_UI_Pick.SetActive(true);
        m_UI_Change.SetActive(false);
        m_UI_DeckMade.SetActive(false);
        ReflashInfo(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReflashInfo(bool UseAnimation)
    {
        if (m_GameData.m_MyInfo.m_Card_Amulet > 0)
            m_Sprite_Deck[0].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Amulet));
 
        if (m_GameData.m_MyInfo.m_Card_Ring > 0)
            m_Sprite_Deck[1].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Ring));

        if (m_GameData.m_MyInfo.m_Card_Glove > 0)
            m_Sprite_Deck[2].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Glove));

        if (m_GameData.m_MyInfo.m_Card_Map > 0)
            m_Sprite_Deck[3].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Map));

        if (m_GameData.m_MyInfo.m_Card_Belt > 0)
            m_Sprite_Deck[4].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Belt));

        if (UseAnimation)
        {
            if(m_Label_Attack.text != m_GameData.m_MyInfo.m_Stat_Card.m_Attack.ToString())
                m_Label_Attack.transform.parent.gameObject.GetComponent<Animation>().Play();
            if(m_Label_Defence.text != m_GameData.m_MyInfo.m_Stat_Card.m_Defence.ToString())
                m_Label_Defence.transform.parent.gameObject.GetComponent<Animation>().Play();
            if(m_Label_Counter.text != m_GameData.m_MyInfo.m_Stat_Card.m_Counter.ToString())
                m_Label_Counter.transform.parent.gameObject.GetComponent<Animation>().Play();
            if(m_Label_Special.text != m_GameData.m_MyInfo.m_Stat_Card.m_Special.ToString())
                m_Label_Special.transform.parent.gameObject.GetComponent<Animation>().Play();
        }

        m_Label_Attack.text = string.Format("{0}", m_GameData.m_MyInfo.m_Stat_Card.m_Attack);
        m_Label_Defence.text = string.Format("{0}", m_GameData.m_MyInfo.m_Stat_Card.m_Defence);
        m_Label_Counter.text = string.Format("{0}", m_GameData.m_MyInfo.m_Stat_Card.m_Counter);
        m_Label_Special.text = string.Format("{0}", m_GameData.m_MyInfo.m_Stat_Card.m_Special);
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

        m_Sprite_Button[0].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_List_Deck[0]));
        m_Sprite_Button[0].gameObject.GetComponent<ShaderLerp>().Run();
        m_Button_Index[0] = m_List_Deck[0];
        m_List_Deck.RemoveAt(0);

        m_Sprite_Button[1].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_List_Deck[0]));
        m_Sprite_Button[1].gameObject.GetComponent<ShaderLerp>().Run();
        m_Button_Index[1] = m_List_Deck[0];
        m_List_Deck.RemoveAt(0);

        m_Sprite_Button[2].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_List_Deck[0]));
        m_Sprite_Button[2].gameObject.GetComponent<ShaderLerp>().Run();
        m_Button_Index[2] = m_List_Deck[0];
        m_List_Deck.RemoveAt(0);
    }

    public void CardPick()
    {
        CardPick(int.Parse(UIButton.current.name));
    }

    public void CardPick(int n)
    {
        if (n < m_Button_Index.Length)
        {
            Card card = m_GameData.m_Table_Card.m_Dic[m_Button_Index[n]];
            switch (card.m_Attribute)
            {
                case 1:
                    if (m_GameData.m_MyInfo.m_Card_Amulet == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Card_Amulet, card);
                        return;
                    }
                    break;

                case 2:
                    if (m_GameData.m_MyInfo.m_Card_Ring == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Card_Ring, card);
                        return;
                    }
                    break;

                case 3:
                    if (m_GameData.m_MyInfo.m_Card_Glove == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Card_Glove, card);
                        return;
                    }
                    break;

                case 4:
                    if (m_GameData.m_MyInfo.m_Card_Map == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Card_Map, card);
                        return;
                    }
                    break;

                case 5:
                    if (m_GameData.m_MyInfo.m_Card_Belt == 0)
                        m_GameData.m_MyInfo.Set_Card(card.m_Attribute, card.m_Id);
                    else
                    {
                        ShowChoice(m_GameData.m_MyInfo.m_Card_Belt, card);
                        return;
                    }
                    break;
            }

            ReflashInfo(true);
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
        m_Sprite_CardOld.sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", Old));
        m_Sprite_CardNew.sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", card.m_Id));
    }

    public void Cancel()
    {
        m_UI_Pick.SetActive(true);
        m_UI_Change.SetActive(false);

        /*
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
        */
    }

    public void Change()
    {
        m_GameData.m_MyInfo.Set_Card(m_Type, m_Id_CardNew);

        ReflashInfo(true);
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
