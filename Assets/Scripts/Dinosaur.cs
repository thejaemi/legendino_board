using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum DinoAction
{
    None = 0,
    NormalAttack = 1,
    Defence,
    Counter,
    SpecialAttack,
}

public class Deck
{
    private List<Dinosaur> m_dinosaurs;
    public List<Dinosaur> dinosuars{get{return m_dinosaurs;}}

    public Deck()
    {
        m_dinosaurs = new List<Dinosaur>();
    }

    public void AddDinosaurToDeck(Dinosaur dinosaur)
    {
        m_dinosaurs.Add(dinosaur);
    }

    public Dinosaur GetDinosaur(int index)
    {
        Dinosaur dinosaur = null;
        if(m_dinosaurs.Count > index)
            dinosaur = m_dinosaurs[index];
        return dinosaur;
            
    }

    public Dinosaur GetNextDinosaur()
    {
        Dinosaur dinosaur = null;
        foreach(Dinosaur dino in m_dinosaurs)
        {
            if(dino.currentHP > 0)
            {
                dinosaur = dino;
                break;
            }
        }

        return dinosaur;
    }
}

public class Dinosaur
{
    private int m_index = 0;
    public int index{get{return m_index;}}

    private int m_totalHP = 15;
    public int totalHP{get{return m_totalHP;}}

    private int m_currentHP = 15;
    public int currentHP{get{return m_currentHP;}}

    private int m_baseAttackPower = 3;
    private int baseAttackPower{get{return m_baseAttackPower;}}

    private int m_specialAttackPower = 5;
    private int specialAttackPower{get{return m_specialAttackPower;}}

    private int m_currentAttackPower = 0;
    public int currentAttackPower{get{return m_currentAttackPower;}}

    private int m_currentDefencePower = 0;
    public int currentDefencePower{get{return m_currentDefencePower;}}


    private DinoAction m_selectedAction = DinoAction.None;
    public DinoAction selectedAction{get{return m_selectedAction;}}

    private SkeletonAnimation m_dinoAnimationObject;

    public bool enableCounter
    {
        get
        {
            bool enable = false;
            if(m_currentHP > 0 && m_selectedAction == DinoAction.Counter)
                enable = true;

            return enable;
        }
    }


    /// <summary>
    /// 기본 생성자 _index =
    /// </summary>
    public Dinosaur(int _index)
    {
        m_index = _index;
        m_totalHP = 15;
        m_currentHP = m_totalHP;
        m_baseAttackPower = 3;
        m_specialAttackPower = 5;
        m_currentAttackPower = 0;
        m_currentDefencePower = 0;
    }

    public Dinosaur(int _index, SkeletonAnimation animation)
    {
        m_index = _index;
        m_totalHP = 15;
        m_currentHP = m_totalHP;
        m_currentAttackPower = 0;
        m_currentDefencePower = 0;
        m_dinoAnimationObject = animation;
    }

    public void SelectAction()
    {
        int actionValue = Random.Range(1, 5);
        m_selectedAction = Utils.ConvertIntToEnumData<DinoAction>(actionValue);

        // m_selectedAction = DinoAction.Counter;

        switch(m_selectedAction)
        {
            case DinoAction.NormalAttack :
                m_currentAttackPower = 4;
                m_currentDefencePower = 0;
                break;
            
            case DinoAction.Defence :
                m_currentAttackPower = 0;
                m_currentDefencePower = 3;
                break;
            
            case DinoAction.Counter :
                m_currentAttackPower = 2;
                m_currentDefencePower = 2;
                break;

            case DinoAction.SpecialAttack :
                m_currentAttackPower = 6;
                m_currentDefencePower = 0;
                break;
        }
    }

    public void Damaged(int attackPower)
    {
        int damagedValue = attackPower - m_currentDefencePower;

        if(damagedValue <= 0)
            damagedValue = 0;

        m_currentHP = m_currentHP - damagedValue;
        if(m_currentHP <= 0)
            m_currentHP = 0;
    }

    public void ProcessFirstAction(Dinosaur enemy)
    {
        switch(m_selectedAction)
        {
            case DinoAction.NormalAttack :
                m_dinoAnimationObject.AnimationState.SetAnimation(0, "attack_1", false);
                enemy.Damaged(m_currentAttackPower);
                break;
            case DinoAction.SpecialAttack :
                m_dinoAnimationObject.AnimationState.SetAnimation(0, "attack_3", false);
                enemy.Damaged(m_currentAttackPower);
                break;
            
            case DinoAction.Defence :
            case DinoAction.Counter :
                m_dinoAnimationObject.AnimationState.SetAnimation(0, "defense", false);
                // Damaged(enemy.currentAttackPower);
                break;
        }
        m_dinoAnimationObject.AnimationState.AddAnimation(0, "idle", true, 0f);
    }

    /// <summary>
    /// 카운터를 위한 후턴 계산
    /// </summary>
    /// <param name="enemy"></param>
    public void ProcessSecondAction(Dinosaur enemy)
    {
        if(m_currentHP <= 0)
            return;

        if(m_selectedAction != DinoAction.Counter)
            return;

        m_dinoAnimationObject.AnimationState.SetAnimation(0, "attack_1", false);
        enemy.Damaged(m_currentAttackPower);
        m_dinoAnimationObject.AnimationState.AddAnimation(0, "idle", true, 0f);
    }

    public void CounterDefence()
    {
        m_currentDefencePower = 0;
    }

    public void InitializeStatus()
    {
        m_currentAttackPower = 0;
        m_currentDefencePower = 0;
    }

    public void SetDinosaurAnimation()
    {
        m_dinoAnimationObject.gameObject.SetActive(true);
    }

    public void SetDisableDinosaurAnimation()
    {
        m_dinoAnimationObject.gameObject.SetActive(false);
    }




    public void TestPlayerSelectAction()
    {
        m_selectedAction = DinoAction.SpecialAttack;

        switch(m_selectedAction)
        {
            case DinoAction.NormalAttack :
                m_currentAttackPower = 4;
                m_currentDefencePower = 0;
                break;
            
            case DinoAction.Defence :
                m_currentAttackPower = 0;
                m_currentDefencePower = 3;
                break;
            
            case DinoAction.Counter :
                m_currentAttackPower = 2;
                m_currentDefencePower = 2;
                break;

            case DinoAction.SpecialAttack :
                m_currentAttackPower = 6;
                m_currentDefencePower = 0;
                break;
        }
    }

    public void TestEnemySelectAction()
    {
        m_selectedAction = DinoAction.Defence;

        switch(m_selectedAction)
        {
            case DinoAction.NormalAttack :
                m_currentAttackPower = 4;
                m_currentDefencePower = 0;
                break;
            
            case DinoAction.Defence :
                m_currentAttackPower = 0;
                m_currentDefencePower = 3;
                break;
            
            case DinoAction.Counter :
                m_currentAttackPower = 2;
                m_currentDefencePower = 2;
                break;

            case DinoAction.SpecialAttack :
                m_currentAttackPower = 6;
                m_currentDefencePower = 0;
                break;
        }
    }
}
