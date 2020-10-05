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
        Debug.LogFormat("QRCode Load Start : {0}", Time.realtimeSinceStartup);
        Read_Resources_Bytes("Data/medal", Parser_);
        Debug.LogFormat("QRCode Load End : {0}", Time.realtimeSinceStartup);
    }

    void Parser_(string[] val)
    {
        if (val[0] == "id")
            return;

        m_Dic.Add(int.Parse(val[0]), new QRCode(val));
    }
}
