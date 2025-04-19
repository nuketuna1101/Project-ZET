using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Game,
    EnemyWiki,
}

public class UIManager : MonoBehaviour
{
    
    GameUI gameUI;
    EnemyWikiUI enemyWikiUI;
    private UIState currentState;

    private void Awake()
    {

        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        enemyWikiUI = GetComponentInChildren<EnemyWikiUI>(true);
        enemyWikiUI.Init(this);

        ChangeState(UIState.Game);
    }

    #region GAMEUI CONTROL

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP / maxHP);
    }
    #endregion

    #region ENEMYWIKIUI CONTROL

    public void PopUpEnemyWiki()
    {
        ChangeState(UIState.EnemyWiki);
        GameManager.Instance.PauseTime();
    }

    public void ExitEnemyWiki()
    {
        ChangeState(UIState.Game);
        GameManager.Instance.ResumeTime();
    }

    #endregion

    public void ChangeState(UIState state)
    {
        currentState = state;
        gameUI.SetActive(currentState);
        enemyWikiUI.SetActive(currentState);
    }
    
}