using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    private float speed = 7f;
    private float input;

    void Update()
    {
        if (GameManager.isDead)
            return;
        input = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * input * speed;
    }
}
