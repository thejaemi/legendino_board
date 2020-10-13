using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Deck : Tool_TableParser
{
    List<int> m_List_AttackDeck = new List<int>();
    List<int> m_List_DefenceDeck = new List<int>();
    List<int> m_List_CounterDeck = new List<int>();
    List<int> m_List_SpecialDeck = new List<int>();

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load()
    {
        Read_Resources_Bytes("Data/deck", Parser_);
    }

    void Parser_(string[] val)
    {
        if (val[0] == "id")
            return;

        if (val[1] == "1")
            m_List_AttackDeck.Add(int.Parse(val[2]));
        else if (val[1] == "2")
            m_List_DefenceDeck.Add(int.Parse(val[2]));
        else if (val[1] == "3")
            m_List_CounterDeck.Add(int.Parse(val[2]));
        else if (val[1] == "4")
            m_List_SpecialDeck.Add(int.Parse(val[2]));
    }

    public T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }

    public List<int> GetShuffledDeck(int Type)
    {
        if(Type == 1)
            return new List<int>(ShuffleArray<int>(m_List_AttackDeck.ToArray(), Time.frameCount));
        else if(Type == 2)
            return new List<int>(ShuffleArray<int>(m_List_DefenceDeck.ToArray(), Time.frameCount));
        else if(Type == 3)
            return new List<int>(ShuffleArray<int>(m_List_CounterDeck.ToArray(), Time.frameCount));
        else
            return new List<int>(ShuffleArray<int>(m_List_SpecialDeck.ToArray(), Time.frameCount));
    }
}
