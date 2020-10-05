using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BattleScene : MonoBehaviour
{
    private Dinosaur m_playerDinosaur;
    private Dinosaur m_enemyDinosaur;

    private Decks m_playerDeck;
    private Decks m_enemyDeck;

    public DinoField playerDinoField;
    public DinoField enemyDinoField;

    public BattleHPGauge playerDinoHPGauge;
    public BattleHPGauge enemyDinoHPGauge;

    public UILabel playerActionName;
    public UILabel enemyActionName;

    public BattleResultPanel battleResultPanel;

    public ScoreBoard scoreBoard;

    public SkeletonAnimation[] dinoAnimations;


    private int m_turn = 0;

    void Awake()
    {
        MakePlayerDeck();
        MakeEnemyDeck();
        
        SetStartDinosaur();
        SetPlayerDinofield();
        SetEnemyDinofield();

        SetPlayerHPGauge();
        SetEnemyHPGauge();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTurn();
    }

    private void StartTurn()
    {
        m_turn++;
        playerDinoField.StartSpin();
        enemyDinoField.StartSpin();
    }

    private void MakePlayerDeck()
    {
        m_playerDeck = new Decks();
        m_playerDeck.AddDinosaurToDeck(new Dinosaur(123, dinoAnimations[0]));
        m_playerDeck.AddDinosaurToDeck(new Dinosaur(129, dinoAnimations[1]));
        m_playerDeck.AddDinosaurToDeck(new Dinosaur(168, dinoAnimations[2]));
    }

    private void MakeEnemyDeck()
    {
        m_enemyDeck = new Decks();
        m_enemyDeck.AddDinosaurToDeck(new Dinosaur(95, dinoAnimations[3]));
        m_enemyDeck.AddDinosaurToDeck(new Dinosaur(120, dinoAnimations[4]));
        m_enemyDeck.AddDinosaurToDeck(new Dinosaur(108, dinoAnimations[5]));
    }

    private void SetStartDinosaur()
    {
        m_playerDinosaur = m_playerDeck.GetNextDinosaur();
        m_enemyDinosaur = m_enemyDeck.GetNextDinosaur();

        m_playerDinosaur.SetDinosaurAnimation();
        m_enemyDinosaur.SetDinosaurAnimation();
    }

    private void SetPlayerDinofield()
    {
        if(m_playerDinosaur == null)
            return;

        playerDinoField.SetDinoMedal(m_playerDinosaur);
    }

    private void SetEnemyDinofield()
    {
        if(m_enemyDinosaur == null)
            return;

        enemyDinoField.SetDinoMedal(m_enemyDinosaur);
    }

    private void SetPlayerHPGauge()
    {
        if(m_playerDinosaur == null)
            return;

        playerDinoHPGauge.SetHPGauge(m_playerDinosaur.currentHP);
    }

    private void SetEnemyHPGauge()
    {
        if(m_enemyDinosaur == null)
            return;
            
        enemyDinoHPGauge.SetHPGauge(m_enemyDinosaur.currentHP);
    }

    private void SelectAction()
    {
        playerDinoField.StopSpin();
        enemyDinoField.StopSpin();
        
        m_playerDinosaur.SelectAction();
        m_enemyDinosaur.SelectAction();

        scoreBoard.SetScoreBoardText(m_playerDinosaur, m_enemyDinosaur);

        // m_playerDinosaur.TestPlayerSelectAction();
        // m_enemyDinosaur.TestEnemySelectAction();

        SetPlayerDinoSelectedActionName();
        SetEnemyDinoSelectedActionName();

        StartCoroutine(ProcessFirstHalfAction());
    }

    private void SetPlayerDinoSelectedActionName()
    {
        playerActionName.text = Utils.GetActionName(m_playerDinosaur.selectedAction);
    }

    private void SetEnemyDinoSelectedActionName()
    {
        enemyActionName.text = Utils.GetActionName(m_enemyDinosaur.selectedAction);
    }

    private IEnumerator ProcessFirstHalfAction()
    {
        yield return YieldHelper.waitForSeconds(1000);
        m_playerDinosaur.ProcessFirstAction(m_enemyDinosaur);
        m_enemyDinosaur.ProcessFirstAction(m_playerDinosaur);
        SetPlayerHPGauge();
        SetEnemyHPGauge();
        
        if(m_playerDinosaur.enableCounter || m_enemyDinosaur.enableCounter)
            StartCoroutine(ProcessSecondHalfAction());
        else
            StartCoroutine(ProcessTurnBattleResult());
    }

    private IEnumerator ProcessSecondHalfAction()
    {
        yield return YieldHelper.waitForSeconds(1000);
        m_playerDinosaur.CounterDefence();
        m_enemyDinosaur.CounterDefence();

        m_playerDinosaur.ProcessSecondAction(m_enemyDinosaur);
        m_enemyDinosaur.ProcessSecondAction(m_playerDinosaur);
        SetPlayerHPGauge();
        SetEnemyHPGauge();
        StartCoroutine(ProcessTurnBattleResult());
    }

    private IEnumerator ProcessTurnBattleResult()
    {
        yield return YieldHelper.waitForSeconds(800);
        CheckTurnBattle();
    }

    private void CheckTurnBattle()
    {
        if(m_playerDinosaur.currentHP <= 0)
        {
            m_playerDinosaur.SetDisableDinosaurAnimation();
            m_playerDinosaur = m_playerDeck.GetNextDinosaur();
            playerDinoHPGauge.InitializeHPGauge();

            if(m_playerDinosaur != null)
            {
                m_playerDinosaur.SetDinosaurAnimation();
                SetPlayerDinofield();
            }
        }

        if(m_enemyDinosaur.currentHP <= 0)
        {
            m_enemyDinosaur.SetDisableDinosaurAnimation();
            m_enemyDinosaur = m_enemyDeck.GetNextDinosaur();
            enemyDinoHPGauge.InitializeHPGauge();

            if(m_enemyDinosaur != null)
            {
                m_enemyDinosaur.SetDinosaurAnimation();
                SetEnemyDinofield();
            }
        }

        //승리
        if(m_playerDinosaur != null && m_enemyDinosaur == null)
        {
            battleResultPanel.SetWinResultText();
            return;   
        }
        //패배
        else if(m_enemyDinosaur != null && m_playerDinosaur == null)
        {
            battleResultPanel.SetLoseResultText();
            return;
        }
        //무승부
        else if(m_playerDinosaur == null && m_enemyDinosaur == null)
        {
            battleResultPanel.SetDrawResultText();
            return;
        }

        //
        m_playerDinosaur.InitializeStatus();
        m_enemyDinosaur.InitializeStatus();
        StartTurn();
    }

    public void OnClickPlayerDinoField()
    {
        SelectAction();
    }

    public void TestOnClickTurnStart()
    {

    }

    public void TestOnClickNormalAttack()
    {

    }

    public void TestOnClickDefence()
    {

    }

    public void TestOnClickCounter()
    {

    }

    public void TestOnClickSpecial()
    {

    }

}
