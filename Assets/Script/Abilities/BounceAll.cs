using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceAll : MonoBehaviour
{
    //Spawner to get all the balls
    public Spawner spawner;

    //SO that holds the value for the ball bounce thrust
    public IntVar ballBounceThrustSO;

    private UIBarControl barCD;
    public IntVar barCDMax;

    //How to get the location of the ball
    public Text scoreIncreaseTextLocation;

    // Start is called before the first frame update
    void Start()
    {
        barCD = this.GetComponent<UIBarControl>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            BounceAllBalls();
        }
    }

    public void BounceAllBalls()
    {
        if(barCD.cooldownBar <= 1.0f)
        {
            int overallScore = 0;
            foreach (GameObject ball in spawner.ballList)
            {
                Ball component = ball.GetComponent<Ball>();
                component.bounceAll = true;
                component.BallBounce(ballBounceThrustSO.value, false);
                overallScore += component.GetScore();
                Vector2 textPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
                ball.GetComponent<Ball>().AddScoreBall(overallScore, textPos);
                barCD.ResetCD(barCDMax.value);
            }
        }
    }
}
