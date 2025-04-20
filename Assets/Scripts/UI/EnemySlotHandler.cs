using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlotHandler : MonoBehaviour
{
    [SerializeField] private Button enemySlotButton;
    [SerializeField] private Image imageEnemySlot;
    private EnemyWikiUI _EnemyWikiUI;
    private EnemyData _EnemyData;
    private int index;

    public void Init(EnemyWikiUI _EnemyWikiUI, int index)
    {
        this._EnemyWikiUI = _EnemyWikiUI;
        this.index = index;

        _EnemyData = DataManager.Instance.GetEnemyData(index);

        SetEnemySlotImage();

        enemySlotButton.onClick.AddListener(OnClickEnemySlot);
    }

    public void SetEnemySlotImage()
    {
        imageEnemySlot.sprite = DataManager.Instance.GetEnemyImageSprite(index);
    }

    public void OnClickEnemySlot()
    {
        _EnemyWikiUI.OnClickEnemySlot(_EnemyData, index);
    }
}
