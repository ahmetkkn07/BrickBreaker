using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = Color.black; //siyah
        //GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //rastgele renk
        GetComponent<Renderer>().material.color = new Color(0.25f, 0.25f, 0.25f, 1f);
        //UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.FindGameObjectWithTag("BrickManager").GetComponent<BrickManager>().bricks.Remove(gameObject);
        //GameObject.FindGameObjectWithTag("Ball").GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(gameObject);

    }
}
