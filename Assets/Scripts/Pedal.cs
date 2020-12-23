using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    public float speed = 20f;
    private float input;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed * input;
    }
}
