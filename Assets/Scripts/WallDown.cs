using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDown : MonoBehaviour
{
    public GameObject ball;
    private bool isColliding;

    private Ball ballScript;
    // Start is called before the first frame update
    void Start()
    {
        ballScript = ball.GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
        {
            return;
        }
        isColliding = true;
        ballScript.Respawn();
    }
}
