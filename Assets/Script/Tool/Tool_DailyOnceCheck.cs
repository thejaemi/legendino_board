using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_DailyOnceCheck : MonoBehaviour
{
    Dictionary<string, DateTime> m_Map = new Dictionary<string, DateTime>();

    public void Add(string key)
    {
        if (m_Map.ContainsKey(key))
            m_Map.Remove(key);

        m_Map.Add(key, DateTime.Now);
    }

    public bool Check(string key)
    {
        if(m_Map.ContainsKey(key))
            if (m_Map[key].Date == DateTime.Now.Date)
                return false;

        return true;
    }

    public bool CheckAdd(string key)
    {
        if (Check(key))
        {
            Debug.LogFormat("Tool_DailyOnceCheck Add : {0}", key);
            Add(key);
            return true;
        }

        Debug.LogFormat("Tool_DailyOnceCheck Aleady : {0}", key);
        return false;
    }
}
