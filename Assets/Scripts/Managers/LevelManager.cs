using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    #region Singleton

    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
            }

            return _instance;
        }
    }

    #endregion
    
    [SerializeField] private GameObject finishPath;
    [SerializeField] private int[] levelLengths;

    [HideInInspector] public Vector3 StartPathPos => _currentFinishPath.transform.position;

    private GameObject _currentFinishPath;
    private int _level;

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
        GameManager.Instance.OnLevelSuccess.AddListener(OnLevelSuccess);
    }

    private void OnLevelStart()
    {
        if (!_currentFinishPath)
        {
            Vector3 spawnPos = Vector3.forward * levelLengths[_level] * StackController.Instance.PathLength;
            
            GameObject newFinish = Instantiate(finishPath, spawnPos, Quaternion.identity);
            _currentFinishPath = newFinish;
        }
        else
        {
            Vector3 spawnPos = StartPathPos + Vector3.forward * levelLengths[_level] * StackController.Instance.PathLength;
            
            GameObject newFinish = Instantiate(finishPath, spawnPos, Quaternion.identity);
            _currentFinishPath = newFinish;
        }
    }

    private void OnLevelSuccess()
    {
        if (_level < levelLengths.Length - 1){_level++;}
        else {_level = 0;}
    }
}
