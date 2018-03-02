using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour {

	[SerializeField]
	private Animator CountDownAnimation;

	public void StartCountDown()
	{
		CountDownAnimation.SetTrigger("StartGame");
	}
}
