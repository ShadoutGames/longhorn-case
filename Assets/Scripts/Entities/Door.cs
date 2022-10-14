using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject doorUI;

    private void Awake() 
    {
        GameManager.AfterStateChanged += OnAfterStateChanged;
        doorUI.SetActive(false);
    }

    private void OnMouseDown() 
    {
        if(GameManager.Instance.State != GameState.DoorState) return;

        LeanTween.cancel(gameObject);
        LeanTween.rotateY(gameObject, -70, 1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(()=> GameManager.Instance.ChangeGameState(GameState.FinalState));
    }

    private void OnAfterStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.DoorState:
                doorUI.SetActive(true);
                LeanTween.rotateAroundLocal(doorUI, doorUI.transform.right, 360, 1).setLoopCount(0);
                break;
            case GameState.FinalState:
                doorUI.SetActive(false);
                break;
        }
    }
}
