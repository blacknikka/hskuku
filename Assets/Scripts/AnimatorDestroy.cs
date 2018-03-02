using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDestroy : MonoBehaviour
{
    [SerializeField]
    private Animator anime;

    [SerializeField]
    private string animeName;

    private AnimatorStateInfo animatorInf;

    private float mLength;
    private float mCur;

    // Use this for initialization
    void Start()
    {
        animatorInf = anime.GetCurrentAnimatorStateInfo(0);
        mLength = 0;
        mCur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mLength == 0
        && animatorInf.IsName(animeName))
        {
            mLength = animatorInf.length;
            mCur = 0;
        }

        if (0 < mLength)
        {
            mCur += Time.deltaTime;
            if (mCur > mLength)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
