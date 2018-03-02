using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class QuestionManager : MonoBehaviour
{
    // 問題のプレハブ
    public GameObject QuestionPrefab;

    private GameObject[] QuestionArray = new GameObject[4];

    public Animator QuestionAnimator;

    private TimeManager _timeMan = new TimeManager();

    // Use this for initialization
    void Start()
    {
        _timeMan.TimeElapsed += (s, e) =>
        {
            // プレハブ化
            // arrayの３が一番遠いオブジェクト 
            QuestionArray[3] = Instantiate(QuestionPrefab, QuestionPrefab.transform.position, QuestionPrefab.transform.rotation);

            // 一つ進める
            QuestionsNextMove();
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuestionsNextMove()
    {
        // 各問題を次に進める
        GameObject.Destroy(QuestionArray[0], 1.0f);
        QuestionArray[0] = QuestionArray[1];
        QuestionArray[1] = QuestionArray[2];
        QuestionArray[2] = QuestionArray[3];
        QuestionArray[3] = null;
    }
}
