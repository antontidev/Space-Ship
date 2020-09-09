using InputSamples.Gestures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Burst;

[System.Serializable]
public class PlayerChecker3d
{
    Camera cam;

    public bool isOnPlayer;

    public PlayerChecker3d(Camera cam)
    {
        this.cam = cam;
        isOnPlayer = false;
    }

    public bool CheckPoint(Vector2 fingerContactPoint)
    {
        var worldPoint = cam.ScreenToWorldPoint(fingerContactPoint);

        RaycastHit hit;

        Physics.Raycast(worldPoint, cam.transform.forward, out hit);

        if (hit.collider != null)
        {
            isOnPlayer = hit.collider.gameObject.tag == "Player";
        }
        else
        {
            isOnPlayer = false;
        }

        return isOnPlayer;
    }
}

public class PlayerController3d : MonoBehaviour
{
    public enum Health
    {
        Alive,
        Died
    }

    [SerializeField]
    private GestureController gestureController;

    public ReactiveProperty<Health> health;

    public float moveSpeed = 0.5f;

    public Camera cam;

    public PlayerChecker3d playerChecker;

    private Rigidbody rb;

    private void Awake()
    {
        health = new ReactiveProperty<Health>(Health.Alive);
        rb = GetComponent<Rigidbody>();

        playerChecker = new PlayerChecker3d(cam);
    }

    private void OnEnable()
    {
        gestureController.Dragged += PlayerDragged;
        gestureController.Pressed += PlayerPressed;
    }

    private void OnDisable()
    {
        gestureController.Dragged -= PlayerDragged;
    }

    private void PlayerPressed(SwipeInput input)
    {
        var fingerContact = input.EndPosition;

        var isOnPlayer = playerChecker.CheckPoint(fingerContact);

        if (isOnPlayer)
        {
            gestureController.SwipeEnded += PlayerReleased;
        }
    }

    private void PlayerDragged(SwipeInput input)
    {
        var frameMovement = input.EndPosition - input.PreviousPosition;
        var speedMovement = frameMovement * Time.fixedDeltaTime * moveSpeed;

        Vector3 movement = new Vector3(speedMovement.x, 0.0f, speedMovement.y);

        rb.AddRelativeForce(movement);
    }

    private void PlayerReleased(SwipeInput input)
    {
        health.Value = Health.Died;

        var isOnPlayer = playerChecker.isOnPlayer;

        if (isOnPlayer)
        {
            gestureController.SwipeEnded -= PlayerReleased;
        }
    }
}
