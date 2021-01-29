using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0.25f, 0.25f, 0.25f, 1f); //tuğla rengini ayarla
    }

    private void OnCollisionEnter2D(Collision2D collision) //çarpışma olursa
    {
        GameObject.FindGameObjectWithTag("BrickManager").GetComponent<BrickManager>().bricks.Remove(gameObject); //tuğlaları tutan listeden bu tuğlayı kaldır
        Destroy(gameObject); //bu tuğlayı yok et
    }
}
