using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public Spawner spawner;
    public BoolVar gameOverSO;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOverSO.value)
        {
            return;
        }
        foreach (GameObject b in spawner.ballList)
        {
            if (Vector3.Distance(b.transform.position, transform.position) < 2.0f)
            {
                
                Vector3 direction = b.transform.position - transform.position;
                direction.Normalize();
                Rigidbody rb = b.GetComponent<Rigidbody>();
                rb.AddForce(-direction / 2);
            }
        }
    }
}
