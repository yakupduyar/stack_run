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


    private void OnEnable()
    {
        GameManager.Instance.OnLevelSuccess.AddListener(OnLevelSuccess);
    }

    public void StopTrack()
    {
        lookCam.gameObject.SetActive(false);
    }

    private void OnLevelSuccess()
    {
        StartCoroutine(RotateAroundPlayer());
    }

    IEnumerator RotateAroundPlayer()
    {
        CinemachineComposer composer = lookCam.GetRig(0).GetCinemachineComponent<CinemachineComposer>();
        while (true)
        {
            composer.m_TrackedObjectOffset = Vector3.Lerp(composer.m_TrackedObjectOffset, Vector3.zero, Time.deltaTime);
            lookCam.m_XAxis.Value += Time.deltaTime*finishTurnSpeed;
            yield return null;
        }
    }
}
