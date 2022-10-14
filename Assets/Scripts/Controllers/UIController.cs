using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject endPanel;

    private void Awake() 
    {
        GameManager.AfterStateChanged += OnAfterStateChanged;
    }

    private void OnAfterStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.BoardState:
                endPanel.SetActive(false);
                break;
            case GameState.FinalState:
                endPanel.SetActive(true);
                break;
        }
    }
}
