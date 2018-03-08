using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Animator CountDownAnimation;

    public enum GameState
    {
        WaitForStart,       // スタート待ち
        CountDown,          // カウントダウン中
        MainGame,           // メインゲーム中
    }

    private GameState State = GameState.WaitForStart;

    public void ChangeGameState(GameState state)
    {
        State = state;

        switch (State)
        {
            case GameState.WaitForStart:
                break;
            case GameState.CountDown:
                break;
            case GameState.MainGame:
                break;
        }
    }

	public void StartCountDown()
	{
        CountDownAnimation.SetTrigger("StartGame");
		ChangeGameState(GameState.CountDown);
	}

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
