using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_QRCode : Tool_TableParser
{
    // Dictionary<int, QRCode> m_Dic = new Dictionary<int, QRCode>();
    Dictionary<string, QRCode> m_Dic = new Dictionary<string, QRCode>();
    


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

        m_Dic.Add(val[1], new QRCode(val));
    }

    public int GetDinoIndex(string code)
    {
        int dinoIndex = 0;
        if(m_Dic.ContainsKey(code))
            dinoIndex = m_Dic[code].m_DinoId;
        else
            Debug.Log("ERROR !! ");
            
        return dinoIndex;   
    }
}
