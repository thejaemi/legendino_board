using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public int m_Id;
    enum DeckType
    {
        Attack = 1,
        Defence,
        Counter,
        Special,
    };
    public int m_Type;
    public int m_CardId;

    public Deck(string[] val)
    {
        m_Id = int.Parse(val[0]);
        m_Type = int.Parse(val[1]);
        m_CardId = int.Parse(val[2]);
    }
}
