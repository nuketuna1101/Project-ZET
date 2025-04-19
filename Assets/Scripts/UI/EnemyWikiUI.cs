using UnityEngine;
using UnityEngine.UI;

public class EnemyWikiUI : BaseUI
{
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickEnemySlot()
    {
        // show infos on left side
    }

    public void OnClickExitButton()
    {
        _UIManager.ExitEnemyWiki();
    }

    protected override UIState GetUIState()
    {
        return UIState.EnemyWiki;
    }
}
