using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {

    [SerializeField]
    private GameObject gObj;

    private void StateChangedFunc(object s, GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.CountDown:
                break;
            case GameManager.GameState.MainGame:
                SoundManager.Inst.PlayBGM("mainbgm");
                break;
            case GameManager.GameState.WaitForStart:
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        gObj.GetComponentInChildren<GameManager>().StateChanged += StateChangedFunc;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
