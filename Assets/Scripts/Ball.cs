using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject pedal;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        //transform.position = new Vector3(0, -2f, 0);
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
        pedal.transform.position = new Vector3(0, -4.5f, 0);
    }
}
