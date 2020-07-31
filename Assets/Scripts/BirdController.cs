using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BirdController : FloatEventInvoker {

    [SerializeField]
    RuntimeAnimatorController[] animatorControllers;

    const float RotateUpSpeedPerSecond = 360;
    const float RotateDownSpeedPerSecond = 220;
    const float FlapForce = 3f;
    const float FlappingDuration = 0.5f;
    const float FloatingAmplitude = 0.005f;

    Rigidbody2D rb2d;
    Animator birdAnimator;
    Timer flappingTimer;

    bool isFlapping = false;
    bool isFloating = false;
    Vector3 birdRotation = Vector3.zero;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        birdAnimator = GetComponent<Animator>();
        rb2d.gravityScale = 0.0f;

        //generate birds color
        int birdIndex = Random.Range(0, animatorControllers.Length);
        birdAnimator.runtimeAnimatorController = animatorControllers[birdIndex];

        //initialize flappingTimer
        flappingTimer = gameObject.AddComponent<Timer>();
        flappingTimer.Duration = FlappingDuration;
        flappingTimer.AddListener(OnFlapEnd);


        unityEvents.Add(EventName.ScoreIncreased, new ScoreIncreased());
        EventManager.AddInvoker(EventName.ScoreIncreased, this);
	}

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.CurrentState)
        {
            case GameStateName.Gameplay:
                {
                    if (Input.GetMouseButtonDown(0) && !ScreenUtils.IsOffTopScreenY(gameObject))
                    {
                        Flap();
                    }
                    FixRotation();
                    break;
                }
            case GameStateName.Preperation:
                {
                    if(!isFloating)
                    {
                        StartCoroutine(Float());
                    }
                    break;
                }
            case GameStateName.GameOver:
                {
                    FixRotation();
                    break;
                }
        }

        birdAnimator.SetBool("isFlapping", isFlapping);
    }

    void FixRotation()
    {
        float degsToRotate;
        if(isFlapping)
        {
            degsToRotate = RotateUpSpeedPerSecond;
        } else
        {
            degsToRotate = -RotateDownSpeedPerSecond;
        }

        birdRotation.z = Mathf.Clamp(birdRotation.z + degsToRotate * Time.deltaTime, -90, 25);
        transform.eulerAngles = birdRotation;
    }

    void Flap()
    {
        isFlapping = true;
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * FlapForce, ForceMode2D.Impulse);
        flappingTimer.Run();
        AudioManager.Play(AudioClipName.BirdSwoosh);
    }

    void OnFlapEnd()
    {
        isFlapping = false;
    }

    IEnumerator Float()
    {
        isFloating = true;
        isFlapping = true;
        rb2d.gravityScale = 0.0f;

        while (GameManager.CurrentState == GameStateName.Preperation)
        {
            transform.position += new Vector3(0, (Mathf.Sin(Time.fixedTime * Mathf.PI)) * FloatingAmplitude);
            yield return null;
        }

        isFloating = false;
        rb2d.gravityScale = 1;
        Flap();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EmptyPipe")
        {
            unityEvents[EventName.ScoreIncreased].Invoke(0);
        } else if (collision.gameObject.tag == "Pipe" && GameManager.CurrentState != GameStateName.GameOver)
        {
            AudioManager.Play(AudioClipName.BirdHit);
            GameManager.Instance.SetGameState(GameStateName.GameOver);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Base")
        {
            if(GameManager.CurrentState != GameStateName.GameOver)
            {
                GameManager.Instance.SetGameState(GameStateName.GameOver);
            }
            AudioManager.Play(AudioClipName.BirdDie);
            rb2d.velocity = Vector2.zero;
            rb2d.isKinematic = true;
            rb2d.freezeRotation = true;
        }
    }
}
