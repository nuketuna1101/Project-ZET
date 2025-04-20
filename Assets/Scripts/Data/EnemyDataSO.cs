using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxHPMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public int[] DropItem;
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public List<EnemyData> EnemyDataList;

    public void ResetList()
    {
        EnemyDataList.Clear();
    }

    public void AddData(EnemyData _EnemyData)
    {
        EnemyDataList.Add( _EnemyData );
    }

    public int GetListCount()
    {
        return EnemyDataList.Count;
    }

    public EnemyData GetEnemyData(int index)
    {
        return EnemyDataList[index];
    }
}