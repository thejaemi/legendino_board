using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCode : MonoBehaviour
{
    public int m_Id;
    public string m_QRCode;
    public int m_DinoId;

    public QRCode(string[] val)
    {
        m_Id = int.Parse(val[0]);
        m_QRCode = val[1];
        m_DinoId = int.Parse(val[2]);
    }
}
