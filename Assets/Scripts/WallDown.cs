using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDown : MonoBehaviour
{
    public GameObject ball; //top nesnesi
    private bool isColliding; //kontrol
    private Ball ballScript; //bal scripti içindeki fonksiyona erişmek için

    void Start()
    {
        ballScript = ball.GetComponent<Ball>(); //ball scriptini toptan al
    }

    void Update()
    {
        isColliding = false; //kontrolü sıfırla
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) //eğer çarpışma devam ediyor ise
            return; //bir şey yapma
        isColliding = true; //kontrolü ayarla
        ballScript.Respawn(); //topu başlangıca alan fonkiyonu çağır
    }
}
