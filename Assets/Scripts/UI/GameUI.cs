using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    //[SerializeField] private Slider hpBar;
    [SerializeField] private Button waveStartButton;
    [SerializeField] private Button enemyWikiButton;

    private void Start()
    {
        //UpdateHPSlider(1);
        waveStartButton.onClick.AddListener(OnClickWaveStartButton);
        enemyWikiButton.onClick.AddListener(OnClickEnemyWikiButton);
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void OnClickWaveStartButton()
    {
        //
    }

    public void OnClickEnemyWikiButton()
    {
        _UIManager.PopUpEnemyWiki();
    }

    public void UpdateHPSlider(float value)
    {
        //hpBar.value = value;
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
