using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    
    #region Singleton

    private static CinemachineManager _instance;

    public static CinemachineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CinemachineManager>();
            }

            return _instance;
        }
    }

    #endregion
    
    [SerializeField] private CinemachineFreeLook lookCam;
    [SerializeField] private float finishTurnSpeed = 25;

    private Vector3 _startOffset;
    private CinemachineComposer _composer;
    private Coroutine _currentCoroutine;
    private void OnEnable()
    {
        _composer = lookCam.GetRig(0).GetCinemachineComponent<CinemachineComposer>();
        _startOffset = _composer.m_TrackedObjectOffset;
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
        GameManager.Instance.OnLevelSuccess.AddListener(OnLevelSuccess);
    }

    public void StopTrack()
    {
        lookCam.gameObject.SetActive(false);
    }

    private void OnLevelStart()
    {
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ToInGameCamera());
    }

    private void OnLevelSuccess()
    {
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(RotateAroundPlayer());
    }
    
    IEnumerator ToInGameCamera()
    {
        while (Mathf.Abs(lookCam.transform.position.x - lookCam.m_Follow.position.x)>0)
        {
            _composer.m_TrackedObjectOffset = Vector3.Lerp(_composer.m_TrackedObjectOffset, Vector3.zero, Time.deltaTime);
            lookCam.m_XAxis.Value += lookCam.transform.position.x - lookCam.m_Follow.position.x;
            print("Reset Cam");
            yield return null;
        }
    }

    IEnumerator RotateAroundPlayer()
    {
        while (true)
        {
            print("Rotate Cam");
            _composer.m_TrackedObjectOffset = Vector3.Lerp(_composer.m_TrackedObjectOffset, Vector3.zero, Time.deltaTime);
            lookCam.m_XAxis.Value += Time.deltaTime*finishTurnSpeed;
            yield return null;
        }
    }
}
