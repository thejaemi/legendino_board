using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_TableParser : MonoBehaviour
{
    public delegate void Parser(string[] s);
    
    public void Read_Resources_Bytes(string Name, Parser parser)
    {
        TextAsset asset = Resources.Load(Name) as TextAsset;
        if (asset == null)
        {
            Debug.Log("asset == null");
            return;
        }

        StringReader reader = new StringReader(asset.text);
        if (reader == null)
        {
            Debug.Log("asset.txt not found or not readable");
            return;
        }

        int lineCount = 0;
        string tmp = reader.ReadLine();

        while (tmp != null)
        {
            string[] stringList = tmp.Split(',');
            if (stringList.Length == 0)
                continue;

            parser(stringList);

            tmp = reader.ReadLine();
            lineCount++;
        }
    }

    public void Read_Resources_Csv(string Name, Parser parser)
    {
        string Path = string.Format("{0}/Resources/{1}.csv", Application.dataPath, Name);
        if (File.Exists(Path))
        {
            StringReader reader = new StringReader(System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(Path)));
            if (reader == null)
            {
                Debug.Log("reader.txt not found or not readable");
                return;
            }

            int lineCount = 0;
            string tmp = reader.ReadLine();

            while (tmp != null)
            {
                string[] stringList = tmp.Split(',');
                if (stringList.Length == 0)
                    continue;

                parser(stringList);

                tmp = reader.ReadLine();
                lineCount++;
            }
        }
    }
}
