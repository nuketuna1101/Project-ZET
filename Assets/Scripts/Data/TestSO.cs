using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestData
{
    public string ItemA;
    public string ItemB;
    public string ItemC;
}

[CreateAssetMenu(fileName = "TestData", menuName = "ScriptableObject/TestSO")]
public class TestSO : ScriptableObject
{
    public List<TestData> TestDataList;

    public void ResetList()
    {
        TestDataList.Clear();
    }

    public void AddData(TestData _TestData)
    {
        TestDataList.Add(_TestData);
    }
}