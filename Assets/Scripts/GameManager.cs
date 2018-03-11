using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    #region editor component
    [SerializeField]
    private Animator CountDownAnimation;

    // 問題作成用クラス
    private QuestionManager QMan;

    // 回答用オブジェクト
    [SerializeField]
    private GameObject AnswerObj;

    // 回答（左）
    [SerializeField]
    private Text Answer1_text;

    // 回答（右）
    [SerializeField]
    private Text Answer2_text;

    // 初回タッチ用ダミー（全画面タッチ判定用）
    [SerializeField]
    private GameObject TouchDummy;

    // スコア表示用テキスト
    [SerializeField]
    private Text ScoreText;
    #endregion

    // どっちが正解か
    private QuestionNotify.Answer Ans = QuestionNotify.Answer.Other;

    // 状態変更イベント
    public event EventHandler<GameState> StateChanged;

    // 連続性回数
    private int CorrectCnt = 0;

    #region state
    public enum GameState
    {
        WaitForStart,       // スタート待ち
        CountDown,          // カウントダウン中
        MainGame,           // メインゲーム中
    }

    private GameState State = GameState.WaitForStart;
    #endregion

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
                ScoreManager.InitScore();
                AnswerObj.SetActive(true);
                QMan.QuestionInit();
                break;
        }

        // イベントの通知
        StateChanged?.Invoke(this, state);
    }

    public void StartCountDown()
    {
        if (State == GameState.WaitForStart)
        {
            CountDownAnimation.SetTrigger("StartGame");
            ChangeGameState(GameState.CountDown);

            TouchDummy.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        // 回答編集用の設定
        QMan = gameObject.GetComponent<QuestionManager>();
        QMan.QuestionShowed += (s, e) =>
        {
            Answer1_text.text = e.Answer1.ToString();
            Answer2_text.text = e.Answer2.ToString();
            Ans = e.Ans;
        };

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Answer1_clicked()
    {
        if (Ans == QuestionNotify.Answer.LeftOK)
        {
            // 正解
            QuesitonCorrect();
        }
        else
        {
            // はずれ
            QuestionFailed();
        }

        NextQuestion();
    }

    public void Answer2_clicked()
    {
        if (Ans == QuestionNotify.Answer.RightOK)
        {
            // 正解
            QuesitonCorrect();
        }
        else
        {
            // はずれ
            QuestionFailed();
        }

        NextQuestion();
    }

    private void QuesitonCorrect()
    {
        ScoreManager.AddScore(100);
        CorrectCnt++;

        string seName = "";
        if(3 <= CorrectCnt)
        {
            seName = "correct_se3";
        }
        else if(2 <= CorrectCnt)
        {
            seName = "correct_se2";
        }
        else
        {
            seName = "correct_se1";
        }

        SoundManager.Inst.PlaySE(seName);
    }

    private void QuestionFailed()
    {
        CorrectCnt = 0;
        SoundManager.Inst.PlaySE("failed_se");
    }

    private void NextQuestion()
    {
        // スコア表示
        ScoreText.text = $"{ScoreManager.GetScore()}";

        // 次の問題
        QMan.QuestionsNextMove();
    }
}
