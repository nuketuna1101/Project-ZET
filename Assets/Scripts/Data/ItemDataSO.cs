using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemData
{
    public int ItemID;
    public string Name;
    public string Description;
    public int UnlockLev;
    public int MaxHP;
    public float MaxHPMul;
    public int MaxMP;
    public float MaxMPMul;
    public int MaxAtk;
    public float MaxAtkMul;
    public int MaxDef;
    public float MaxDefMul;
    public int Status;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public List<ItemData> ItemDataList;

    public void ResetList()
    {
        ItemDataList.Clear();
    }

    public void AddData(ItemData _ItemData)
    {
        ItemDataList.Add(_ItemData);
    }

    public int GetListCount()
    {
        return ItemDataList.Count;
    }
}