using InputSamples.Gestures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using Zenject;

public class Planet : MonoBehaviour
{
    [SerializeField]
    public Transform spawnRocketPostition;

    public GestureController gestureController;

    void OnEnable()
    {
        if (gestureController != null)
        {
          //  gestureController.Dragged += GestureController_Dragged;
        }
    }

    private void OnDisable()
    {
       // gestureController.Dragged -= GestureController_Dragged;
    }

    public void GestureController_Dragged(SwipeInput input)
    {
        var delta = input.EndPosition - input.PreviousPosition;

        Debug.Log(delta);

        transform.Rotate(-delta.y * Time.deltaTime, delta.x * Time.deltaTime, 0, Space.Self);
    }
}
