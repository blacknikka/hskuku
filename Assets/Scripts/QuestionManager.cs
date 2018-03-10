using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class QuestionManager : MonoBehaviour
{
    private class QuesitonData
    {
        public QuestionItem[] Items = new QuestionItem[4];

        public GameObject[] QuestionArray = new GameObject[4];
    }

    // 問題のプレハブ
    public GameObject QuestionPrefab;

    private QuesitonData Datas = new QuesitonData();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuestionInit()
    {
        Datas.Items[0] = QuestionGenerator.MakeQuestion();
        Datas.Items[1] = QuestionGenerator.MakeQuestion();
        Datas.Items[2] = QuestionGenerator.MakeQuestion();
        Datas.Items[3] = QuestionGenerator.MakeQuestion();

        Datas.QuestionArray[0] = Instantiate(QuestionPrefab, QuestionPrefab.transform.position, QuestionPrefab.transform.rotation);
        Datas.QuestionArray[0].GetComponent<QuestionControl>().DoAnimationOnTarget();

        Datas.QuestionArray[1] = Instantiate(QuestionPrefab, QuestionPrefab.transform.position, QuestionPrefab.transform.rotation);
        Datas.QuestionArray[1].GetComponent<QuestionControl>().DoAnimationNext2();

        Datas.QuestionArray[2] = Instantiate(QuestionPrefab, QuestionPrefab.transform.position, QuestionPrefab.transform.rotation);
        Datas.QuestionArray[2].GetComponent<QuestionControl>().DoAnimationNext1();

        Datas.QuestionArray[3] = Instantiate(QuestionPrefab, QuestionPrefab.transform.position, QuestionPrefab.transform.rotation);
        Datas.QuestionArray[3].GetComponent<QuestionControl>().DoAnimationFrameIn();
    }

    public void QuestionsNextMove()
    {
        // 各問題を次に進める
        GameObject.Destroy(Datas.QuestionArray[0], 1.0f);
        Datas.QuestionArray[0] = Datas.QuestionArray[1];
        Datas.QuestionArray[0].gameObject.GetComponent<QuestionControl>().DoAnimationOnTarget();

        Datas.QuestionArray[1] = Datas.QuestionArray[2];
        Datas.QuestionArray[1].gameObject.GetComponent<QuestionControl>().DoAnimationNext1();

        Datas.QuestionArray[2] = Datas.QuestionArray[3];
        Datas.QuestionArray[2].gameObject.GetComponent<QuestionControl>().DoAnimationNext2();

        // プレハブ化
        // arrayの３が一番遠いオブジェクト 
        Datas.QuestionArray[3] = Instantiate(QuestionPrefab, QuestionPrefab.transform.position, QuestionPrefab.transform.rotation);
        Datas.QuestionArray[3].gameObject.GetComponent<QuestionControl>().DoAnimationFrameIn();
    }

    private class QuestionItem
    {
        public string Question;

        public int Result;
    }

    private static class QuestionGenerator
    {
        private static List<QuestionItem> _questionList = new List<QuestionItem>
        {
            new QuestionItem { Question = "1 * 2", Result = 2},
            new QuestionItem { Question = "2 * 4", Result = 8},
            new QuestionItem { Question = "5 * 8", Result = 40},
            new QuestionItem { Question = "6 * 9", Result = 54},
            new QuestionItem { Question = "10 * 2", Result = 20},
            new QuestionItem { Question = "8 * 8", Result = 64},
            new QuestionItem { Question = "4 * 2", Result = 8},
            new QuestionItem { Question = "3 * 3", Result = 9},
            new QuestionItem { Question = "8 * 9", Result = 72},
            new QuestionItem { Question = "4 * 1", Result = 4},
            new QuestionItem { Question = "7 * 8", Result = 56},
            new QuestionItem { Question = "9 * 9", Result = 81},
            new QuestionItem { Question = "7 * 4", Result = 28},
        };

        public static QuestionItem MakeQuestion()
        {
            var q = Random.Range(0, _questionList.Count);
            return new QuestionItem
            {
                Question = _questionList[q].Question,
                Result = _questionList[q].Result,
            };
        }
    }
}
