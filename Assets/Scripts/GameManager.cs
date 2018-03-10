using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Animator CountDownAnimation;

    private QuestionManager QMan;

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
                // カウントダウン開始時には特に何もしない
                break;
            case GameState.MainGame:
                // メインゲームの開始
                QMan.QuestionInit();
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
        QMan = gameObject.GetComponent<QuestionManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
