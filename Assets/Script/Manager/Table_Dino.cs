using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Dino : Tool_TableParser
{
    public Dictionary<int, Dino> m_Dic = new Dictionary<int, Dino>();


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
        Read_Resources_Bytes("Data/dino", Parser_);
    }

    void Parser_(string[] val)
    {
        if (val[0] == "id")
            return;

        Dino tmp = new Dino(val);
        m_Dic.Add(int.Parse(val[1]), tmp);
    }
}
