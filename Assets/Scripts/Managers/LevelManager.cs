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
    [SerializeField] private int[] levelLength;

    [HideInInspector] public Vector3 StartPathPos => _currentFinishPath.transform.position;

    private GameObject _currentFinishPath;
    private int _level;

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
    }

    private void OnLevelStart()
    {
        if (!_currentFinishPath)
        {
            GameObject newFinish = Instantiate(finishPath,
                Vector3.forward * levelLength[_level] * StackController.Instance.PathLength, Quaternion.identity);
        }

    }
}
