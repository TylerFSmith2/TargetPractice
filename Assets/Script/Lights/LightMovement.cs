using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    private Color c;

    public bool lightsMove;

    private bool dir;

    private float changeDirTimer;

    [SerializeField]
    private float changeDirTimerMax = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        c = gameObject.GetComponent<Light>().color;
        lightsMove = true;
        dir = (Random.value > 0.5f);
        changeDirTimer = changeDirTimerMax;
    }

    // Update is called once per frame
    public bool GetDir()
    {
        return dir;
    }

    public void SetDir(bool newDir)
    {
        dir = newDir;
    }

    private void Update()
    {
        if(!lightsMove)
        {
            return;
        }

        //Change direction on timer
        if (changeDirTimer > 0.0f)
        {
            changeDirTimer -= Time.deltaTime;
        }
        else
        {
            dir = !dir;
            changeDirTimerMax = Random.Range(1.0f, 3.0f);
            changeDirTimer = changeDirTimerMax;
        }

        //Keep lights in X bounds
        if (transform.position.x > 5)
        {
            transform.position = new Vector3(4.9f, transform.position.y, transform.position.z);
            dir = !dir;
        }
        else if(transform.position.x < -5)
        {
            transform.position = new Vector3(-4.9f, transform.position.y, transform.position.z);
            dir = !dir;
        }

        //Keep lights in Z bounds
        if (transform.position.z > 1.5f)
        {
            transform.position = new Vector3(1.4f, transform.position.y, transform.position.z);
            dir = !dir;
        }
        else if (transform.position.x < -2)
        {
            transform.position = new Vector3(-1.9f, transform.position.y, transform.position.z);
            dir = !dir;
        }
    }


    public void ResetColor()
    {
        gameObject.GetComponent<Light>().color = c;
    }

    public void GreyscaleLight()
    {
        float randNumColor = Random.Range(0, 255)/255.0f;
        gameObject.GetComponent<Light>().color = new Color(randNumColor, randNumColor, randNumColor);
    }
}
