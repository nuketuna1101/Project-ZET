using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class EnemyWikiUI : BaseUI
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Transform enemyWikiGrid;
    [SerializeField] public EnemySlotHandler enemySlotPrefab;

    // 필요한 속성: Name, Description, Attack, MaxHP, DropItem
    [SerializeField] private Image imageSelectedEnemy;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textAttack;
    [SerializeField] private TextMeshProUGUI textMaxHP;
    [SerializeField] private TextMeshProUGUI textDropItem;



    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        exitButton.onClick.AddListener(OnClickExitButton);
        InitEnemySlots();
    }

    private void InitEnemySlots()
    {
        int enemyNum = DataManager.Instance.GetEnemyDataSOCount();
        for (int i = 0; i < enemyNum; i++)
        {
            var newEnemySlot  = Instantiate(enemySlotPrefab, enemyWikiGrid);
            newEnemySlot.Init(this, i);
        }
    }

    public void OnClickExitButton()
    {
        _UIManager.ExitEnemyWiki();
    }

    public void OnClickEnemySlot(EnemyData _EnemyData, int index)
    {
        imageSelectedEnemy.sprite = DataManager.Instance.GetEnemyImageSprite(index);

        textName.text = $"Name : {_EnemyData.Name}";
        textDescription.text = $"Description : {_EnemyData.Description}";
        textAttack.text = $"Attack : {_EnemyData.Attack}";
        textMaxHP.text = $"MaxHP : {_EnemyData.MaxHP}";
        textDropItem.text = $"DropItem : {_EnemyData.DropItem}";
    }


    protected override UIState GetUIState()
    {
        return UIState.EnemyWiki;
    }
}
