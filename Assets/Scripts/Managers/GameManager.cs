using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    // Pause option
    private float previousTimeScale = 1.0f;

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
