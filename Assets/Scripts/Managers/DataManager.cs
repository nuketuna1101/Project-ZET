using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Progress;

public class DataManager : BaseSingleton<DataManager>
{
    [Header("EnemyData")]
    [SerializeField] private EnemyDataSO _EnemyDataSO;

    [Header("ItemData")]
    [SerializeField] private ItemDataSO _ItemDataSO;

    [Header("TestData")]
    [SerializeField] private TestSO _TestSO;

    [Header("Image Sprites")]
    [SerializeField] private Sprite[] EnemyImageSprites;
    [SerializeField] private Sprite[] ItemImageSprites;

    private Dictionary<string, Sprite> _enemySpriteDictionary = new Dictionary<string, Sprite>();
    private Dictionary<int, Sprite> _itemSpriteDictionary = new Dictionary<int, Sprite>();


    #region Initialize/Set/Reset Data Resources using CSVReader

    protected override void Awake()
    {
        base.Awake();
        //
        ResetData();
        SetEnemyData();
        SetItemData();
        SetTestSO();
        //
        LoadSprites();
        CacheEnemySprites();
        CacheItemSprites();
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

    private void LoadSprites()
    {
        EnemyImageSprites = Resources.LoadAll<Sprite>("Sprites/SpritesData");
        Debug.Log($"[Sprite Loading] Loaded {EnemyImageSprites.Length} sprites from Resources/Sprites : " +
            $"{(EnemyImageSprites == null ? "Failed" : "Success")}");
    }
    #endregion

    #region Getter Functions

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

    public Sprite GetEnemyImageSpriteById(string enemyId)
    {
        if (_enemySpriteDictionary.ContainsKey(enemyId))
        {
            return _enemySpriteDictionary[enemyId];
        }
        else
        {
            Debug.LogWarning($"Item sprite with ID {enemyId} not found in ItemImageSprites.");
            return null;
        }
    }

    public ItemData GetItemData(int itemID)
    {
        if (_ItemDataSO == null || _ItemDataSO.ItemDataList == null)
        {
            Debug.LogError("ItemDataSO is NULL");
            return null;
        }

        foreach (var itemData in _ItemDataSO.ItemDataList)
        {
            if (itemData.ItemID == itemID)
            {
                return itemData;
            }
        }

        Debug.LogWarning($"CANNOT FIND ItemData with ID {itemID}");
        return null;
    }

    public string GetItemNameById(int itemID)
    {
        return GetItemData(itemID)?.Name;
    }

    public Sprite GetItemSprite(int itemID)
    {
        if (_itemSpriteDictionary.ContainsKey(itemID))
        {
            return _itemSpriteDictionary[itemID];
        }
        else
        {
            Debug.LogWarning($"Item sprite with ID {itemID} not found in ItemImageSprites.");
            return null;
        }
    }
    #endregion


    private void CacheEnemySprites()
    {
        // 효율적 검색을 위한 딕셔너리 캐싱
        if (EnemyImageSprites != null)
        {
            foreach (var sprite in EnemyImageSprites)
            {
                string itemID = sprite.name;
                if (!string.IsNullOrEmpty(itemID))
                {
                    if (!_enemySpriteDictionary.ContainsKey(itemID))
                    {
                        _enemySpriteDictionary.Add(itemID, sprite);
                    }
                    else
                    {
                        Debug.LogWarning($"Duplicate: {itemID} 이미지 중복");
                    }
                }
                else
                {
                    Debug.LogWarning($"Sprite name '{sprite.name}' INVALID error");
                }
            }
        }
    }

    private void CacheItemSprites()
    {
        // 효율적 검색을 위한 딕셔너리 캐싱
        if (ItemImageSprites != null)
        {
            foreach (var sprite in ItemImageSprites)
            {
                if (int.TryParse(sprite.name, out int itemID))
                {
                    if (!_itemSpriteDictionary.ContainsKey(itemID))
                    {
                        _itemSpriteDictionary.Add(itemID, sprite);
                    }
                    else
                    {
                        Debug.LogWarning($"Duplicate: {itemID} 이미지 중복");
                    }
                }
                else
                {
                    Debug.LogWarning($"Sprite name '{sprite.name}' INVALID error");
                }
            }
        }
    }



    private int[] ConvertStringToIntArray(string dropItemString)
    {
        // 쉼표(,) 기준 int[] 받기
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
