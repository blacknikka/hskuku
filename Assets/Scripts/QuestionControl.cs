using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionControl : MonoBehaviour {

    [SerializeField]
    private Text Quesiton;

    [SerializeField]
    private Animator QuestionAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WriteText(string s)
    {
        Quesiton.text = s;
    }

    public void DoAnimationFrameIn()
    {
        QuestionAnimator.SetTrigger("QuestionFrameIn");
    }

    public void DoAnimationNext1()
    {
        QuestionAnimator.SetTrigger("QuestionNext1");
    }

    public void DoAnimationNext2()
    {
        QuestionAnimator.SetTrigger("QuesitonNext2");
    }

    public void DoAnimationOnTarget()
    {
        QuestionAnimator.SetTrigger("QuestionOnTarget");
    }
}
