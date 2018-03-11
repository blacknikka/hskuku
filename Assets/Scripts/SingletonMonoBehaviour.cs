using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{

    private static T inst;
    public static T Inst
    {
        get
        {
            if (inst == null)
            {
                inst = (T)FindObjectOfType(typeof(T));

                if (inst == null)
                {
                    Debug.LogError(typeof(T) + "is nothing");
                }
            }

            return inst;
        }
    }
}
