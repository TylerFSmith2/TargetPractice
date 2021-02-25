using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class Ball : MonoBehaviour
{
    //Ball Properties
    public Rigidbody rb;
    public Renderer rend;

    //Variables the ball uses/changes
    public IntVar scoreMultiplierSO, ballBounceThrustSO, menuTimeScale, timeSO, dragSO;

    public ScoreHandling scoreHandler;
    public BoolVar timePauseSO, maxBallsBoolSO;
    public Vector3 vectorTimePause;

    //How to get the location of the ball
    public Text scoreIncreaseTextLocation;

    //bool to make sure ball collides only once
    private bool isColliding;

    //bool to check if ball is has been bounced
    private bool bounced;
    //Cooldown that allows the ball to bounce other balls
    private float bouncedCD;
    private float bouncedCDMax;

    //Spawner
    public Spawner spawner;

    //Bounce ball after time pause
    public bool bouncePostTimePause;

    //For score checking, skip individual if true
    public bool bounceAll;

    public GameOver GameOverObj;

    //Score vars
    public GameObject LowScoreLine;
    public int timesBounced;
    public float individualTime;

    //For setting achievements
    public SteamAchievements steamAchieve;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = dragSO.value;
        rend = GetComponent<Renderer>();
        spawner = FindObjectOfType<Spawner>();
        GameOverObj = FindObjectOfType<GameOver>();
        scoreHandler = FindObjectOfType<ScoreHandling>();
        bounced = false;
        isColliding = false;
        bounceAll = false;
        LowScoreLine = GameObject.Find("LowScoreLine");
        scoreIncreaseTextLocation = GameObject.Find("scoreIncreaseTextLocation").GetComponent<Text>();
        timesBounced = 1;
        bouncedCDMax = 0.3f;
        individualTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if time pause is active
        if (timePauseSO.value)
        {
            rb.velocity = Vector3.zero;
            rb.velocity = Vector3.up / 6;
        }
        else if (bouncePostTimePause && !timePauseSO.value)
        {
            BallBounce(ballBounceThrustSO.value, false);
            bouncePostTimePause = false;
        }
        individualTime += Time.deltaTime;

        if(bouncedCD > 0)
        {
            bouncedCD -= Time.deltaTime;
        }
        else
        {
            bounced = false;
        }

        //Avoid clumping
        foreach(GameObject b in spawner.ballList)
        {
            if(b != gameObject)
            {
                if (Vector3.Distance(b.transform.position, transform.position) < 2.0f)
                {
                    Vector3 direction = b.transform.position - transform.position;
                    direction.Normalize();
                    rb.AddForce(-direction);
                }
            }
        }
    }

    //Bounce ball with a value of thrust and make bounced true if bounceable is true
    public void BallBounce(int thrust, bool bounceable)
    {
        bounced = bounceable;
        bouncedCD = bouncedCDMax;
        //If time pause is active
        if (timePauseSO.value && !bouncePostTimePause)
        {
            bouncePostTimePause = true;
            scoreMultiplierSO.value += 1;
            return;
        }
        else if(timePauseSO.value && bouncePostTimePause)
        {
            return;
        }
        else if (menuTimeScale.value == 0)
        {
            return;
        }
        else
        {
            scoreMultiplierSO.value++;
            timesBounced++;
            if (!bounceAll)
            {
                AddScoreBall();
            }
            rb.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 2, Random.Range(-0.5f, 0.5f)) * thrust);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        //If time pause is on, dont check collisions
        if (timePauseSO.value)
        {
            return;
        }

        //Check collisions and respond appropriately
        if (collision.transform.CompareTag("Floor"))//If ball hits the floor
        {
            if (isColliding)//If ball is already hitting the floor
            {
                return;
            }
            GameOverObj.GameOverEvent();
        }
        else if (collision.transform.CompareTag("Ceiling"))//If ball hits ceiling
        {
            rb.AddForce(new Vector3(Random.Range(-0.25f, 0.75f), -1, Random.Range(-0.25f, 0.75f))); //Add force down
        }
        else if (collision.gameObject.layer == 8)//If ball hits another ball
        {
            //If ball was bounced
            if (bounced)
            {
                collision.gameObject.GetComponent<Ball>().BallBounce(ballBounceThrustSO.value / 3, false);
            }
        }
    }

    public void BallDestruction()
    {
        spawner.Remove(gameObject);
        Destroy(gameObject);
    }

    //Calculate and add score to the score SO while displaying the text
    public void AddScoreBall()
    {
        int scoreToAdd = 1 + (timesBounced / 4) + ((int)individualTime / 2) + (scoreMultiplierSO.value / 4);
        if (maxBallsBoolSO.value)
        {
            scoreToAdd *= 2;
        }
        if (testBelowLowScoreLine())
        {
            scoreToAdd *= 2;
        }
        scoreHandler.AddScore(scoreToAdd);
        Vector2 textPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        scoreIncreaseTextLocation.transform.position = textPos;
        scoreIncreaseTextLocation.fontSize = 20;
        scoreIncreaseTextLocation.text = "+" + scoreToAdd;
        StartCoroutine(ShowText());
    }

    public bool testBelowLowScoreLine()
    {
        return transform.position.y < LowScoreLine.transform.position.y;
    }

    //Add score but with a pos to give for where the text should be
    public void AddScoreBall(int scoreToAdd, Vector3 pos)
    {
        scoreHandler.AddScore(scoreToAdd);
        scoreIncreaseTextLocation.transform.position = pos;
        scoreIncreaseTextLocation.fontSize = 80;
        scoreIncreaseTextLocation.text = "+" + scoreToAdd;
        StartCoroutine(ShowTextALL());
        bounceAll = false;
    }

    //Calculates the score of the ball at its location
    public int GetScore()
    {
        int scoreToAdd = timesBounced + (int)individualTime + scoreMultiplierSO.value;
        if (maxBallsBoolSO.value)
        {
            timesBounced *= 2;
        }
        if (transform.position.y < LowScoreLine.transform.position.y)
        {
            timesBounced *= 2;
        }
        return timesBounced;
    }

    public IEnumerator ShowText()
    {
        Color col;
        scoreIncreaseTextLocation.color = Random.ColorHSV(0.1f, 1f, 1f, 1f, 1f, 1f);
        while (scoreIncreaseTextLocation.color.a > 0)
        {
            col = scoreIncreaseTextLocation.color;
            col.a -= .007f;
            scoreIncreaseTextLocation.color = col;
            yield return null;
        }
        yield break;
    }
    public IEnumerator ShowTextALL()
    {
        Color col;
        scoreIncreaseTextLocation.color = Random.ColorHSV(0.1f, 1f, 1f, 1f, 1f, 1f);
        int t = 0;
        while (scoreIncreaseTextLocation.color.a > 0)
        {
            col = scoreIncreaseTextLocation.color;
            t++;
            col.a -= .007f;
            scoreIncreaseTextLocation.color = col;
            
            yield return null;
        }
        yield break;
    }
}
