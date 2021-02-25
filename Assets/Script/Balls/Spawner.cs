using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

public class Spawner : MonoBehaviour
{
    public List<GameObject> ballList;
    public Transform ballTypes;
    public IntVar BallSpawnTimerSO;
    public float timeUntilNextBall;
    public int maxTimeBetweenBalls;
    public Text timeUntilNextBallText;
    public Text ballNum;
    private int countdown;

    //Max number of balls that can be in play
    [SerializeField]
    private IntVar maxBallSO;

    public BoolVar gameActiveVar;
    public BoolVar gameOverVar;
    public BoolVar maxBallsBoolSO;
    public IntVar numBallsVar;

    //For setting achievements
    public SteamAchievements steamAchieve;

    public bool testingTurnOffSpawning = false;
    //Spawn points
    // Start is called before the first frame update
    void Start()
    {
        ballList = new List<GameObject>();
        numBallsVar.value = 0;
        countdown = (int)timeUntilNextBall;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActiveVar.value == false)
        {
            return;
        }

        if (timeUntilNextBall >= 0)
        {
            timeUntilNextBall -= Time.deltaTime;
            if (timeUntilNextBall < countdown)
            {
                timeUntilNextBallText.GetComponent<Text>().text = "Next Target in: " + countdown;
                countdown -= 1;
            }
        }
        else
        {
            if(2 > maxBallSO.value - ballList.Count)
            {
                SpawnBall(maxBallSO.value - ballList.Count);
            }
            else
            {
                SpawnBall(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnBall();
        }
        ballNum.text = "Target Amount: " + ballList.Count;
        if(ballList.Count >= maxBallSO.value)
        {
            //If balls hit max, do something?
            //Half timer cd?
        }
    }

    public Vector3 RandomSpawnSpot()
    {
        //Get size of ball spawner object
        Vector3 size = GetComponent<BoxCollider>().size;
        Vector3 randomPosition = new Vector3(Random.Range(size.x / 2, -size.x / 2), Random.Range(size.y / 2, -size.y / 2), Random.Range(size.z / 2, -size.z / 2)) + this.gameObject.transform.position;
        return randomPosition;
    }

    public void StartGame()
    {
        SpawnBall(3);
    }
    private void SpawnBall()
    {
        if(testingTurnOffSpawning)
        {
            return;
        }


        //update num balls
        if (ballList.Count > numBallsVar.value)
        {
            numBallsVar.value = ballList.Count + 1;
        }

        Vector3 randPos = RandomSpawnSpot();

        //Randomly choose a ball to spawn
        float randFloatRange = Random.Range(0.0f, 1.0f);

        //Spawn ball and add it to the list of balls
        if (ballList.Count < maxBallSO.value)
        {
            Transform newBall = Instantiate(ballTypes, randPos, Quaternion.identity);
            steamAchieve.TestTripleAchieve(ballList.Count, new string[] { "25 Targets", "50 Targets", "100 Targets" }, new int[] { 25, 50, 100 });

            ballList.Add(newBall.gameObject);


            timeUntilNextBall = BallSpawnTimerSO.value;
            countdown = (int)timeUntilNextBall;

            if (BallSpawnTimerSO.value >= maxTimeBetweenBalls)
            {
                BallSpawnTimerSO.value -= 1;
            }
        }
        if(ballList.Count == maxBallSO.value) //Max ball count, reward player?
        {
            maxBallsBoolSO.value = true;
        }
        else
        {
            maxBallsBoolSO.value = false;
        }        
    }

    private void SpawnBall(int amt)
    {
        if(gameOverVar.value == true)
        {
            return;
        }
        for(int i = 0; i < amt; i++)
        {
            SpawnBall();
        }
    }

    public void Remove(GameObject ballToRemove)
    {
        ballList.Remove(ballToRemove);
    }

    public void RemoveAll()
    {
        foreach (GameObject b in ballList)
        {
            Destroy(b);
        }
        Destroy(this);
    }

    private void TestAchieve(string[] IDs, int[] numsToPass)
    {
        //Check if all Target achievements are obtained
        bool dontLoop = false;
        SteamUserStats.GetAchievement(IDs[2], out dontLoop);

        //Bool to test if achievements are unlocked
        bool unlocked = false;
        //Check if 25 Targets is unlocked
        SteamUserStats.GetAchievement(IDs[0], out unlocked);

        if (!dontLoop)
        {
            if (ballList.Count >= numsToPass[0] && !unlocked)
            {
                //Unlock Achievement
                steamAchieve.UnlockSteamAchievement(IDs[0]);
            }
            else if (ballList.Count <= numsToPass[0])
            {

            }
            else
            {
                SteamUserStats.GetAchievement(IDs[1], out unlocked);
                if (ballList.Count >= numsToPass[1] && !unlocked)
                {
                    steamAchieve.UnlockSteamAchievement(IDs[1]);
                }
                else if (ballList.Count <= numsToPass[1])
                {

                }
                else
                {
                    SteamUserStats.GetAchievement(IDs[2], out unlocked);
                    if (ballList.Count >= numsToPass[2] && !unlocked)
                    {
                        steamAchieve.UnlockSteamAchievement(IDs[2]);
                    }
                }
            }
        }
    }
}
