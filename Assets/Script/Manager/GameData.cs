using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // for battle
    public int m_StageId;

    public PlayerInfo m_MyInfo = new PlayerInfo();
    public Dictionary<int, GameObject> m_MyDino_Object = new Dictionary<int, GameObject>();
    public PlayerInfo m_OtherInfo = new PlayerInfo();
    public Dictionary<int, GameObject> m_OtherDino_Object = new Dictionary<int, GameObject>();


    public CM_JobManager m_JobMng;
    public CM_JobQueue m_JobQueue;

    // for data
    public Table_Dino m_Table_Dino;
    public Table_QRCode m_Table_QRCode;
    public Table_Card m_Table_Card;
    public Table_Deck m_Table_Deck;
    public Table_Stage m_Table_Stage;
    public Dictionary<string, bool> m_StageClear = new Dictionary<string, bool>();


    private void Awake()
    {
        DontDestroyOnLoad(CM_Dispatcher.instance);
        m_JobMng = CM_JobManager.Make();
        m_JobQueue = CM_JobQueue.Make();

        (m_Table_Dino = CM_Singleton<Table_Dino>.instance).Load();
        m_Table_Dino.gameObject.transform.parent = transform;

        (m_Table_QRCode = CM_Singleton<Table_QRCode>.instance).Load();
        m_Table_QRCode.gameObject.transform.parent = transform;

        (m_Table_Card = CM_Singleton<Table_Card>.instance).Load();
        m_Table_Card.gameObject.transform.parent = transform;

        (m_Table_Stage = CM_Singleton<Table_Stage>.instance).Load();
        m_Table_Stage.gameObject.transform.parent = transform;

        (m_Table_Deck = CM_Singleton<Table_Deck>.instance).Load();
        m_Table_Deck.gameObject.transform.parent = transform;


        // stage table 의 갯수 만큼 체크
        //m_StageClear.Clear();
        //m_StageClear.Add("stage_1", GetLocalData("stage_1", 0) == 1);
        //m_StageClear.Add("stage_2", GetLocalData("stage_2", 0) == 1);
        //m_StageClear.Add("stage_3", GetLocalData("stage_3", 0) == 1);
    }

    /// <summary>
    /// 기기에 저장한 값을 얻어온다. 없으면 생성
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="DefaultValue"></param>
    /// <returns></returns>
    public int GetLocalData(string Key, int DefaultValue)
    {
        if (PlayerPrefs.HasKey(Key) == false)       // 저장된 값을 찾지 못했다
        {
            PlayerPrefs.SetInt(Key, DefaultValue);  // 디폴트 값으로 생성
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetInt(Key);             // 저장된 값을 반환
    }

    public void SetLocalData(string Key, int Value)
    {
        PlayerPrefs.SetInt(Key, Value);
        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Add_MyDino(int id)
    {
        Debug.LogFormat("Add MyDino {0}", id);
        m_MyInfo.Add_Dino(id);
        //StartCoroutine(LoadDino(id, 0));
        m_JobQueue.Enqueue(LoadDino(id, 0)).Start();
    }

    public void Add_OtherDino(int id)
    {
        Debug.LogFormat("Add OtherDino {0}", id);
        m_OtherInfo.Add_Dino(id);
        //StartCoroutine(LoadDino(id, 1));
        m_JobQueue.Enqueue(LoadDino(id, 1)).Start();
    }

    IEnumerator LoadDino(int id, int Owner)
    {
        ResourceRequest req = Resources.LoadAsync(string.Format("Model/SR_Dino ({0})", id));

        while (!req.isDone)
            yield return null;

        GameObject tmp = Instantiate(req.asset, gameObject.transform) as GameObject;
        
        if (Owner == 0)
            m_MyDino_Object.Add(id, tmp);
        else if (Owner == 1)
            m_OtherDino_Object.Add(id, tmp);
    }
}
