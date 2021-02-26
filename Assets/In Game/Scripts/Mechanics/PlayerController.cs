using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Forward,
    Turning,
    Dead,
    Passing
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    [SerializeField] private float turningSpeed;
    [SerializeField] private float turnRotateSpeed;
    [SerializeField] private float carAlignSpeed;

    [SerializeField] private float levelPassCarSpeed;

    public State playerState;

    bool ableToTurn;

    GameObject objectToHook;

    Direction turnDirection;

    Rigidbody rb;
    RopeDrawer rope;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rope = GetComponent<RopeDrawer>();

        PlayerActions.EnteredCorner += EnteredCorner;
        PlayerActions.ExitedCorner += ExitedCorner;
        EventManager.GameFailed += FailedGame;
        EventManager.GameRestarted += ResetPlayer;
        EventManager.LevelPassed += PassedLevel;
        EventManager.LevelExited += ExitedLevel;
    }

    private void ResetPlayer()
    {
        playerState = State.Idle;
        ableToTurn = false;
        objectToHook = null;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    private void Update()
    {
        if (playerState != State.Idle && playerState != State.Dead)
            Movement();

        PlayerInput();
    }

    private void Movement()
    {
        switch(playerState)
        {
            case State.Forward:
                rb.velocity = transform.forward * carSpeed;
                break;
            case State.Turning:
                rb.velocity = transform.forward * turningSpeed;
                break;
            case State.Passing:
                rb.velocity = transform.forward * levelPassCarSpeed;
                break;
        }
        
    }

    private void PlayerInput()
    {
#if UNITY_EDITOR

        if (playerState == State.Idle && Input.GetMouseButton(0))
        {
            EventManager.StartGame();
            playerState = State.Forward;
        }

        if (playerState != State.Idle && playerState != State.Dead)
        {
            if (ableToTurn)
            {
                if (Input.GetMouseButton(0))
                {
                    Turn();
                    rope.DrawRope(objectToHook);
                    playerState = State.Turning;
                }
                else
                {
                    rope.ResetRope();
                    playerState = State.Forward;
                }
            }
        }
#else
if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            switch(_touch.phase)
            {
                case TouchPhase.Began:
                    if (playerState == State.Idle)
                    {
                        EventManager.StartGame();
                        playerState = State.Forward;
                    }

                    if (playerState != State.Idle && playerState != State.Dead)
                    {
                        if (ableToTurn)
                        {
                            Turn();
                            rope.DrawRope(objectToHook);
                            playerState = State.Turning;
                        }
                    }
                    break;
                case TouchPhase.Canceled:
                    rope.ResetRope();
                    playerState = State.Forward;
                    break;
                case TouchPhase.Ended:
                    rope.ResetRope();
                    playerState = State.Forward;
                    break;
            }
        }
#endif
    }

    private void EnteredCorner(GameObject barrel)
    {
        ableToTurn = true;
        objectToHook = barrel;
    }

    private void ExitedCorner()
    {
        ableToTurn = false;
        objectToHook = null;
        playerState = State.Forward;
    }

    private void PassedLevel()
    {
        playerState = State.Passing;
    }

    private void ExitedLevel()
    {
        playerState = State.Forward;
    }


    private void Turn()
    {
        Vector3 _barrelDir = (objectToHook.transform.position - transform.position).normalized;
        float _turnDir = AngleDir(transform.forward, _barrelDir, transform.up);

        transform.Rotate(Vector3.up * _turnDir * turnRotateSpeed); // Multiply with up vector to turn car in Y axis.
    }

    private void FailedGame()
    {
        playerState = State.Dead;
        rb.velocity = Vector3.zero;
        rope.ResetRope();
    }

    public void AlignCarDirection(Vector3 direction)
    {
        Quaternion _nextRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * carAlignSpeed);
        transform.rotation = _nextRotation;
    }


    /// <summary>
    /// Determine if an object is on the right or left of an another object.
    /// </summary>
    /// <param name="fwd">Forward vector for reference object.</param>
    /// <param name="targetDir">Vector points from reference object to target object.</param>
    /// <param name="up">Up vector of reference object.</param>
    /// <returns></returns>
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
            return 1f;
        else
            return -1f;
    }

}
