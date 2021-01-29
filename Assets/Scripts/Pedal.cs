using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    private float speed = 7f; //pedalın hızı
    private float input; //input değerini taşımak için

    void Update()
    {
        if (GameManager.isDead) //eğer ölmüş ise
            return; //giriş alma
        input = Input.GetAxisRaw("Horizontal"); //ölmemiş ise girişi al

    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * input * speed; //giriş değeri kadar hareket ettir
    }
}
