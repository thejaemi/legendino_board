using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public UILabel playerFirstAttackPower;
    public UILabel PlayerFirstDefencePower;

    public UILabel enemyFirstAttackPower;
    public UILabel enemyFirstDefencePower;

    public UILabel playerSecondAttackPower;
    public UILabel playerSecondDefencePower;

    public UILabel enemySecondAttackPower;
    public UILabel enemySecondDefencePower;


    public void SetScoreBoardText(Dinosaur playerDinosaur, Dinosaur enemyDinosaur)
    {
        switch(playerDinosaur.selectedAction)
        {
            case DinoAction.NormalAttack :
            case DinoAction.Defence :
            case DinoAction.SpecialAttack :
                playerFirstAttackPower.text = GetAttackPower(playerDinosaur.currentAttackPower);
                PlayerFirstDefencePower.text = GetDefencePower(playerDinosaur.currentDefencePower);
                playerSecondAttackPower.text = GetAttackPower();
                playerSecondDefencePower.text = GetDefencePower();
                break;

            case DinoAction.Counter :
                playerFirstAttackPower.text = GetAttackPower();
                PlayerFirstDefencePower.text = GetDefencePower(playerDinosaur.currentDefencePower);
                playerSecondAttackPower.text = GetAttackPower(playerDinosaur.currentAttackPower);
                playerSecondDefencePower.text = GetDefencePower();
                break;
        }

        switch(enemyDinosaur.selectedAction)
        {
            case DinoAction.NormalAttack :
            case DinoAction.Defence :
            case DinoAction.SpecialAttack :
                enemyFirstAttackPower.text = GetAttackPower(enemyDinosaur.currentAttackPower);
                enemyFirstDefencePower.text = GetDefencePower(enemyDinosaur.currentDefencePower);
                enemySecondAttackPower.text = GetAttackPower();
                enemySecondDefencePower.text = GetDefencePower();
                break;

            case DinoAction.Counter :
                enemyFirstAttackPower.text = GetAttackPower();
                enemyFirstDefencePower.text = GetDefencePower(enemyDinosaur.currentDefencePower);
                enemySecondAttackPower.text = GetAttackPower(enemyDinosaur.currentAttackPower);
                enemySecondDefencePower.text = GetDefencePower();
                break;
        }
    }


    private string GetAttackPower(int value = 0)
    {
        string powerValue = string.Format("공격 +{0}", value);
        return powerValue;
    }

    private string GetDefencePower(int value = 0)
    {
        string powerValue = string.Format("방어 +{0}", value);
        return powerValue;
    }
}
