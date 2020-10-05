using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_QRCode : Tool_TableParser
{
    Dictionary<int, QRCode> m_Dic = new Dictionary<int, QRCode>();


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
        Read_Resources_Bytes("Data/medal", Parser_);
    }

    void Parser_(string[] val)
    {
        if (val[0] == "id")
            return;

        m_Dic.Add(int.Parse(val[1]), new QRCode(val));
    }
}
