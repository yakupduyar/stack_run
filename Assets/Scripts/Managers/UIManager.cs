using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private FailPanel failPanel;
    [SerializeField] private SuccessPanel successPanel;

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStart.AddListener(OnLevelStart);
        GameManager.Instance.OnLevelSuccess.AddListener(OnLevelSuccess);
        GameManager.Instance.OnLevelFail.AddListener(OnLevelFail);
    }

    private void OnLevelStart()
    {
        mainMenu.gameObject.SetActive(false);
    }

    private void OnLevelSuccess()
    {
        successPanel.gameObject.SetActive(true);
    }

    private void OnLevelFail()
    {
        failPanel.gameObject.SetActive(true);
    }
}
