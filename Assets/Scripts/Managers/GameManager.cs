using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    #endregion

    #region Events

    public UnityEvent OnLevelStart = new UnityEvent();
    public UnityEvent OnLevelSuccess = new UnityEvent();
    public UnityEvent OnLevelFail = new UnityEvent();

    #endregion


    public void StartLevel()
    {
        OnLevelStart.Invoke();
    }

    public void SuccessLevel()
    {
        OnLevelSuccess.Invoke();
    }

    public void FailLevel()
    {
        OnLevelFail.Invoke();
    }
}