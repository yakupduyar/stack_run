using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PathEvent : UnityEvent<Vector3>{}
public class StackController : MonoBehaviour
{
     #region Singleton

     private static StackController _instance;

     public static StackController Instance
     {
          get
          {
               if (_instance == null)
               {
                    _instance = GameObject.FindObjectOfType<StackController>();
               }

               return _instance;
          }
     }

     #endregion
     
     
     [SerializeField] private Transform pathsParent;
     [SerializeField] private Transform pathPrefab;
     [SerializeField] private float spawnSpan = 10,pathMoveSpeed = 5f;

     private bool _isSpawn=true;
     private int _path,_combo=0;
     private float _xDelta;
     private Transform _currentPath;
     private Vector3 _lastPathPosition,_lastPathScale;

     public PathEvent onPathPlaced = new PathEvent();

     private void OnEnable()
     {
          _path = pathsParent.childCount - 1;
          _currentPath = pathsParent.GetChild(_path);
          _lastPathPosition = pathsParent.GetChild(_path - 1).localPosition;
          _lastPathScale = pathsParent.GetChild(_path - 1).localScale;
          PlacePath();
          GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
     }

     private void OnLevelStart()
     {
          StartCoroutine(UpdateFrame());
     }

     private IEnumerator UpdateFrame()
     {
          while (true)
          {
               if (Input.GetMouseButtonDown(0))
               {
                    SetSpawnSide();
                    PlacePath();
               }
               MovePath();
               yield return null;
          }
     }

     private void MovePath()
     {
          _currentPath.Translate(Vector3.left*Time.deltaTime*pathMoveSpeed);
     }

     private void PlacePath()
     {
          _xDelta = _currentPath.localPosition.x - _lastPathPosition.x;
          
          if (Mathf.Abs(_xDelta) < .1)
          {
               _combo++;
               _currentPath.localPosition = new Vector3(_lastPathPosition.x,0, _currentPath.localPosition.z);
          }
          else if (Mathf.Abs(_xDelta) < _lastPathScale.x)
          {
               _combo = 0;
               _currentPath.localPosition = new Vector3(_lastPathPosition.x+_xDelta*.5f,0, _currentPath.localPosition.z);
               _currentPath.localScale -= Vector3.right*Mathf.Abs(_xDelta);
               CreateRubble();
          }
          else
          {
               pathMoveSpeed = 0;
               _currentPath.AddComponent<Rigidbody>();
               GameManager.Instance.FailLevel();
               return;
          }
          
          onPathPlaced.Invoke(_currentPath.position);
          if (_isSpawn)
          {
               SpawnPath();
          }
          else
          {
               LastPathPlaced();
          }
     }

     private void SpawnPath()
     {
          _lastPathPosition = _currentPath.localPosition;
          _lastPathScale = _currentPath.localScale;
          Transform newPath = pathsParent.GetChild(0);
          newPath.localScale = _currentPath.localScale;
          newPath.localPosition = _lastPathPosition + Vector3.right*spawnSpan + Vector3.forward * _lastPathScale.z;
          newPath.SetSiblingIndex(_path);
          _currentPath = newPath;
     }

     private void CreateRubble()
     {
          Vector3 rubbleScale = new Vector3(_lastPathScale.x - _currentPath.localScale.x, 1, _currentPath.localScale.z);
          Vector3 rubblePos =  new Vector3( (_xDelta>0) ?
               _currentPath.position.x + (_currentPath.localScale.x*.5f+rubbleScale.x*.5f)
               : _currentPath.position.x - (_currentPath.localScale.x*.5f+rubbleScale.x*.5f)
               , _currentPath.position.y, _currentPath.position.z);
          Transform rubble = Instantiate(pathPrefab, rubblePos,Quaternion.identity);
          rubble.localScale = rubbleScale;
     }

     private void SetSpawnSide()
     {
          spawnSpan *= -1;
          pathMoveSpeed *= -1;
     }

     public void PathReachToFinish()
     {
          _isSpawn = false;
     }

     void LastPathPlaced()
     {
          pathMoveSpeed = 0;
          GameManager.Instance.SuccessLevel();
     }
}
