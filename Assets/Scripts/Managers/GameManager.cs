using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    public PlayerController player { get; private set; }
    //private ResourceController _playerResourceController;
    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    // Pause option
    private float previousTimeScale = 1.0f;


    protected override void Awake()
    {
        base.Awake();
        player = FindFirstObjectByType<PlayerController>();
        enemyManager = GetComponentInChildren<EnemyManager>();
    }

    public void StartGame()
    {
        
    }

    public void GameOver()
    {
        //enemyManager.StopWave();
    }

    #region PAUSE OPTION
    public void PauseTime()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
    }
    public void ResumeTime()
    {
        Time.timeScale = previousTimeScale;
    }
    #endregion
}
