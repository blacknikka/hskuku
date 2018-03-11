using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class QuestionNotify
{
    public int Answer1;

    public int Answer2;

    public enum Answer
    {
        Other,      // その他
        LeftOK,     // 右側が正解（Answer1が正解）
        RightOK,    // 左側が正解（Answer2が正解）
    }

    public Answer Ans;
}

public class QuestionManager : MonoBehaviour
{
    private enum QuestionState
    {
        FrameIn,
        Next1,
        Next2,
        OnTarget,
    }

    // 問題のプレハブ
    [SerializeField]
    private GameObject QuestionPrefab;

    // 問題の管理
    private List<QuestionItem> Datas = new List<QuestionItem>();

    // 問題の通知用イベント
    public event EventHandler<QuestionNotify> QuestionShowed;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NotifyAnswer()
    {
        QuestionShowed?.Invoke
                       (this, new QuestionNotify
                       {
                           Answer1 = Datas[0].Answer1,
                           Answer2 = Datas[0].Answer2,
                           Ans = Datas[0].Result == Datas[0].Answer1 ?
                                QuestionNotify.Answer.LeftOK :
                                QuestionNotify.Answer.RightOK,
                       });
    }

    public void QuestionInit()
    {
        Datas.Add(new QuestionItem());
        Datas.Add(new QuestionItem());
        Datas.Add(new QuestionItem());
        Datas.Add(new QuestionItem());

        Datas[0].MakeQuestion(QuestionPrefab);
        Datas[1].MakeQuestion(QuestionPrefab);
        Datas[2].MakeQuestion(QuestionPrefab);
        Datas[3].MakeQuestion(QuestionPrefab);

        Datas[0].SetAnimation(QuestionState.OnTarget);
        Datas[1].SetAnimation(QuestionState.Next2);
        Datas[2].SetAnimation(QuestionState.Next1);
        Datas[3].SetAnimation(QuestionState.FrameIn);

        NotifyAnswer();
    }

    public void QuestionsNextMove()
    {
        // 各問題を次に進める
        Datas[0].DestroyQuestion();
        Datas.RemoveAt(0);

        Datas[0].SetAnimation(QuestionState.OnTarget);
        Datas[1].SetAnimation(QuestionState.Next2);
        Datas[2].SetAnimation(QuestionState.Next1);

        Datas.Add(new QuestionItem());
        Datas[3].MakeQuestion(QuestionPrefab);
        Datas[3].SetAnimation(QuestionState.FrameIn);

        NotifyAnswer();
    }

    private class QuestionItem
    {
        // 問題文
        public string Question;

        // 結果
        public int Result;

        // 回答
        public int Answer1;
        public int Answer2;

        public GameObject QuestionObj;

        public void SetAnimation(QuestionState state)
        {
            switch (state)
            {
                case QuestionState.FrameIn:
                    QuestionObj.GetComponent<QuestionControl>().DoAnimationFrameIn();
                    break;
                case QuestionState.Next1:
                    QuestionObj.GetComponent<QuestionControl>().DoAnimationNext1();
                    break;
                case QuestionState.Next2:
                    QuestionObj.GetComponent<QuestionControl>().DoAnimationNext2();
                    break;
                case QuestionState.OnTarget:
                    QuestionObj.GetComponent<QuestionControl>().DoAnimationOnTarget();
                    break;
                default:
                    throw new System.Exception("target error");
            }
        }

        public void MakeQuestion(GameObject obj)
        {
            var v = QuestionGenerator.MakeQuestion();
            Question = v.Question;
            Result = v.Result;
            Answer1 = v.Answer1;
            Answer2 = v.Answer2;

            QuestionObj = Instantiate(obj, obj.transform.position, obj.transform.rotation);

            QuestionObj.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = Question;
        }

        public void DestroyQuestion()
        {
            Destroy(QuestionObj);
        }
    }

    private static class QuestionGenerator
    {
        private static List<QuestionItem> _questionList = new List<QuestionItem>
        {
            new QuestionItem { Question = "1 * 2", Result = 2, Answer1 = 2, Answer2 = 3},
            new QuestionItem { Question = "2 * 4", Result = 8, Answer1 = 6, Answer2 = 8},
            new QuestionItem { Question = "5 * 8", Result = 40, Answer1 = 40, Answer2 = 44},
            new QuestionItem { Question = "6 * 9", Result = 54, Answer1 = 56, Answer2 = 54},
            new QuestionItem { Question = "10 * 2", Result = 20, Answer1 = 20, Answer2 = 15},
            new QuestionItem { Question = "8 * 8", Result = 64, Answer1 = 64, Answer2 = 46},
            new QuestionItem { Question = "4 * 2", Result = 8, Answer1 = 4, Answer2 = 8},
            new QuestionItem { Question = "3 * 3", Result = 9, Answer1 = 9, Answer2 = 6},
            new QuestionItem { Question = "8 * 9", Result = 72, Answer1 = 71, Answer2 = 72},
            new QuestionItem { Question = "4 * 1", Result = 4, Answer1 = 7, Answer2 = 4},
            new QuestionItem { Question = "7 * 8", Result = 56, Answer1 = 65, Answer2 = 56},
            new QuestionItem { Question = "9 * 9", Result = 81, Answer1 = 88, Answer2 = 81},
            new QuestionItem { Question = "7 * 4", Result = 28, Answer1 = 26, Answer2 = 28},
        };

        public static QuestionItem MakeQuestion()
        {
            var q = UnityEngine.Random.Range(0, _questionList.Count);
            return new QuestionItem
            {
                Question = _questionList[q].Question,
                Result = _questionList[q].Result,
                Answer1 = _questionList[q].Answer1,
                Answer2 = _questionList[q].Answer2,
            };
        }
    }
}
