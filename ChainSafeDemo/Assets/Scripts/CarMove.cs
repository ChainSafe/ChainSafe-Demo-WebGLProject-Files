using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private GameObject LeftWall;
    private GameObject RightWall;
    public Vector3 targetPosition;
    private float moveSpeed = 20;
    public float direction = -1;
    
    void Awake()
    {
        // sets triggers to change directions
        LeftWall = GameObject.FindGameObjectWithTag("LeftCarWall");
        RightWall = GameObject.FindGameObjectWithTag("RightCarWall");
    }

     void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LeftCarWall")
        {
            direction = 1;
            gameObject.transform.Rotate(0, 180, 0);
        }

        if (collision.gameObject.tag == "RightCarWall")
        {
            direction = -1;
            gameObject.transform.Rotate(0, 180, 0);
        }
    }

    void Update()
    {
        // moves object
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime ,0,0);
    }
}
