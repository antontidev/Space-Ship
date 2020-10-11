using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSignalDirector : MonoBehaviour
{
    [SerializeField]
    private float rocketDeltaYMove = 20f;

    [SerializeField]
    private Transform rocketHolder;

    [SerializeField]
    private AnimationCurve easeOfRocketMoving;

    public void OnWinCutsceneRocketHolderMove()
    {
        rocketHolder.DOMoveY(rocketDeltaYMove, 15, true)
            .SetEase(easeOfRocketMoving)
            .From();
    }
}
