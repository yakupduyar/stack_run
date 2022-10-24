using System;
using UnityEngine;

public class StackController : MonoBehaviour
{
     [SerializeField] private Transform pathsParent;
     [SerializeField] private Transform pathPrefab;
     
     private int _path;
     private float _transition = 0,_xDelta;
     private Transform _currentPath;
     private Vector3 _lastPathPosition,_lastPathScale;

     private void Awake()
     {
          _path = pathsParent.childCount - 1;
          _currentPath = pathsParent.GetChild(_path);
          _lastPathPosition = pathsParent.GetChild(_path - 1).localPosition;
          _lastPathScale = pathsParent.GetChild(_path - 1).localScale;
     }

     private void Update()
     {
          if (Input.GetMouseButtonDown(0))
          {
               PlacePath();
          }
          MovePath();
     }

     void MovePath()
     {
          _transition += Time.deltaTime;
          float targetX = Mathf.Sin(_transition) * _currentPath.localScale.x + _lastPathPosition.x;
          _currentPath.localPosition = new Vector3(targetX, 0, _currentPath.localPosition.z);
     }

     void PlacePath()
     {
          _xDelta = _currentPath.localPosition.x - _lastPathPosition.x;
          _currentPath.localPosition = new Vector3(_lastPathPosition.x+_xDelta*.5f,0, _currentPath.localPosition.z);
          _currentPath.localScale -= Vector3.right*Mathf.Abs(_xDelta);
          CreateRubble();
          SpawnPath();
     }

     void SpawnPath()
     {
          _lastPathPosition = _currentPath.localPosition;
          _lastPathScale = _currentPath.localScale;
          Transform newPath = pathsParent.GetChild(0);
          newPath.localScale = _currentPath.localScale;
          newPath.localPosition = _lastPathPosition + Vector3.forward * _lastPathScale.z;
          newPath.SetSiblingIndex(_path);
          _currentPath = newPath;
     }

     void CreateRubble()
     {
          Vector3 rubbleScale = new Vector3(_lastPathScale.x - _currentPath.localScale.x, 1, _currentPath.localScale.z);
          Vector3 rubblePos =  new Vector3( (_xDelta>0) ?
               _currentPath.position.x + (_currentPath.localScale.x*.5f+rubbleScale.x*.5f)
               : _currentPath.position.x - (_currentPath.localScale.x*.5f+rubbleScale.x*.5f)
               , _currentPath.position.y, _currentPath.position.z);
          Transform rubble = Instantiate(pathPrefab, rubblePos,Quaternion.identity);
          rubble.localScale = rubbleScale;
     }
}
