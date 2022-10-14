using System;
using Core;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region SerializeFields

    #endregion

    #region Props

	public GameState State { get; private set; }

    #endregion

    #region Actions

    public static event Action<GameState> BeforeStateChanged;
	public static event Action<GameState> AfterStateChanged;

    #endregion

    #region Unity Methods

    private void Start() 
    {
		LeanTween.init(100);
		ChangeGameState(GameState.BoardState);
    }

    #endregion

    #region Methods

    public void ChangeGameState(GameState newState)
    {
        BeforeStateChanged?.Invoke(State);
		State = newState;
		AfterStateChanged?.Invoke(State);
    }

    #endregion

    #region Callbacks

    #endregion
}