using UnityEngine;

public class EnemyWikiManager : MonoBehaviour
{
    public void UpdateWikiUI()
    {
        int enemyNum = DataManager.Instance.GetEnemyDataSOCount();
        for (int i = 0; i < enemyNum; i++)
        {

        }
    }
    public void AddSlot()
    {
        // 필요한 속성: Name, Description, Attack, MaxHP, DropItem

    }
}
