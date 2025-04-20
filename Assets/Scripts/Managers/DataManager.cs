using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DataManager : BaseSingleton<DataManager>
{
    [Header("EnemyData")]
    [SerializeField] private EnemyDataSO _EnemyDataSO;

    [Header("ItemData")]
    [SerializeField] private ItemDataSO _ItemDataSO;

    [Header("TestData")]
    [SerializeField] private TestSO _TestSO;

    [Header("Enemy Image Sprites")]
    [SerializeField] private Sprite[] EnemyImageSprites;


    protected override void Awake()
    {
        base.Awake();
        //
        ResetData();
        SetEnemyData();
        SetItemData();
        SetTestSO();
    }

    private void ResetData()
    {
        _EnemyDataSO.ResetList();
        _ItemDataSO.ResetList();
        _TestSO.ResetList();
    }

    private void SetTestSO()
    {
        var dataList = new List<Dictionary<string, object>>();
        string file = "DataTable/Test";
        dataList = CSVReader.Read(file);
        int totalNum = CSVReader.GetLinesLength(file) - 3;
        for (int i = 0; i < totalNum; i++)
        {
            _TestSO.AddData(new TestData
            {
                ItemA = CSVReader.GetString(dataList, i, "ItemA"),
                ItemB = CSVReader.GetString(dataList, i, "ItemB"),
                ItemC = CSVReader.GetString(dataList, i, "ItemC"),
            }
            );
        }
    }
    private void SetEnemyData()
    {
        var dataList = new List<Dictionary<string, object>>();
        string file = "DataTable/Monster";
        dataList = CSVReader.Read(file);
        int totalNum = CSVReader.GetLinesLength(file) - 3;
        for (int i = 0; i < totalNum; i++)
        {
            _EnemyDataSO.AddData( new EnemyData
            {
                MonsterID = CSVReader.GetString(dataList, i, "MonsterID"),
                Name = CSVReader.GetString(dataList, i, "Name"),
                Description = CSVReader.GetString(dataList, i, "Description"),
                Attack = CSVReader.GetIntValue(dataList, i, "Attack"),
                AttackMul = CSVReader.GetFloatValue(dataList, i, "AttackMul"),
                MaxHP = CSVReader.GetIntValue(dataList, i, "MaxHP"),
                MaxHPMul = CSVReader.GetFloatValue(dataList, i, "MaxHPMul"),
                AttackRange = CSVReader.GetIntValue(dataList, i, "AttackRange"),
                AttackRangeMul = CSVReader.GetFloatValue(dataList, i, "AttackRangeMul"),
                AttackSpeed = CSVReader.GetFloatValue(dataList, i, "AttackSpeed"),
                MoveSpeed = CSVReader.GetFloatValue(dataList, i, "MoveSpeed"),
                MinExp = CSVReader.GetIntValue(dataList, i, "MinExp"),
                MaxExp = CSVReader.GetIntValue(dataList, i, "MaxExp"),
                DropItem = ConvertStringToIntArray(CSVReader.GetString(dataList, i, "DropItem")),
            }
            );
        }
    }
    private void SetItemData()
    {
        var dataList = new List<Dictionary<string, object>>();
        string file = "DataTable/Item";
        dataList = CSVReader.Read(file);
        int totalNum = CSVReader.GetLinesLength(file) - 3;
        for (int i = 0; i < totalNum; i++)
        {
            _ItemDataSO.AddData(new ItemData
            {
                ItemID = CSVReader.GetIntValue(dataList, i, "ItemID"),
                Name = CSVReader.GetString(dataList, i, "Name"),
                Description = CSVReader.GetString(dataList, i, "Description"),
                UnlockLev = CSVReader.GetIntValue(dataList, i, "UnlockLev"),
                MaxHP = CSVReader.GetIntValue(dataList, i, "MaxHP"),
                MaxHPMul = CSVReader.GetFloatValue(dataList, i, "MaxHPMul"),
                MaxMP = CSVReader.GetIntValue(dataList, i, "MaxMP"),
                MaxMPMul = CSVReader.GetFloatValue(dataList, i, "MaxMPMul"),
                MaxAtk = CSVReader.GetIntValue(dataList, i, "MaxAtk"),
                MaxAtkMul = CSVReader.GetFloatValue(dataList, i, "MaxAtkMul"),
                MaxDef = CSVReader.GetIntValue(dataList, i, "MaxDef"),
                MaxDefMul = CSVReader.GetFloatValue(dataList, i, "MaxDefMul"),
                Status = CSVReader.GetIntValue(dataList, i, "Status"),
            }
            );
        }
    }

    public EnemyData GetEnemyData(int index)
    {
        return _EnemyDataSO.GetEnemyData(index);
    }

    public int GetEnemyDataSOCount()
    {
        return _EnemyDataSO.GetListCount();
    }

    public Sprite GetEnemyImageSprite(int index)
    {
        return EnemyImageSprites[index];
    }

    private int[] ConvertStringToIntArray(string dropItemString)
    {
        if (string.IsNullOrEmpty(dropItemString))
        {
            return new int[0];
        }

        string[] parts = dropItemString.Split(',');
        int[] intArray = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i].Trim(), out int value))
            {
                intArray[i] = value;
            }
            else
            {
                Debug.LogError($"Failed to parse DropItem value: {parts[i]}");
                return new int[0];
            }
        }
        return intArray;
    }
}
