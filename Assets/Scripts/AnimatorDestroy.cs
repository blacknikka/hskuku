﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject GameMan;

    public void DestroyAnimation()
    {
        Destroy(gameObject);

        GameMan.GetComponent<GameManager>().ChangeGameState(GameManager.GameState.MainGame);
    }
}
