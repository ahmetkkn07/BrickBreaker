using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameObject pedal; //pedal nesnesi
    public GameObject GameOverUI; //oyun bitince gözüken ekran
    public Text LevelText; //toast mesaj yaptığımız text nesnesi
    public AudioSource Crash; //top, tuğla harici nesnelere çarpınca çalan ses
    public AudioSource Break; //tuğla kırılma sesi
    public bool isRespawned = true; //kontrol değişkeni

    private GameManager GameManager; //GameManager içerisindeki metotlara ulaşmak için referans
    private float speed = 8f; //topun hızı
    private float limit = 3f; //x eksenindeki atış vektörünün sınırı
    private float posx = -9999; //x ekseninde sıkışma kontrolü için konum
    private float posy = -9999; //y eksenindeki sıkışma kontrolü için konum
    private int colCountx = 0; //x ekseninde sekme sayısı
    private int colCounty = 0; // y ekseninde sekme sayısı

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //GameManager nesnesini al
        GameOverUI.SetActive(false); //oyun bitti ekranını devre dışı yap
        Respawn(true); //topu başlangıca getir
    }

    void Update()
    {
        if (isRespawned) //eğer yeniden doğmuş ise
        {
            pedal.transform.position = new Vector3(0, -4.5f, 0); //pedalı başlangıç konumuna getir
            if (Input.GetKeyDown(KeyCode.Space)) //boşluk tuşuna basılmış ise
                Push(); //topu fırlatan fonksiyonu çağır
        }
        if (colCountx >= 2 || colCounty >= 2) //eğer sıkışma var ise
        {
            GameManager.showToast("Top sıkıştığı için tekrar atış yapmalısınız!\nBoşluk tuşuna basarak atış yapabilirsiniz.", 3); //ekranda mesaj göster
            Initialize(); //başlangıç haline getir
            isRespawned = true; //yeniden başlatıldı
            colCountx = 0; //kontrol değişkenlerini temizle
            colCounty = 0;//kontrol değişkenlerini temizle
        }
    }
    private void Initialize()
    {
        transform.position = new Vector3(0, -4.1748f, 0); //topu başlangıç konumuna al
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//topun hız vektörünü sıfırla
        pedal.transform.position = new Vector3(0, -4.5f, 0);//pedalı başlangıç konumuna getir
    }
    public void Respawn(bool isLevelUp = false)
    {
        colCountx = 0;//kontrol değişkenlerini temizle
        colCounty = 0;//kontrol değişkenlerini temizle
        if (isLevelUp && GameManager.Live == 0) //eğer yeni seviyeye geçiliyor ise
            GameManager.Live++; //azalan canı artır
        if (GameManager.Live > 0) // eğer can 0'dan az ise
        {
            Initialize(); //başlangıç haline getir
            isRespawned = true; //yeniden başlatıldı
            if (!isLevelUp) //eğer yeni seviye değilse
                GameManager.Live--; //canı azalt
        }
        else //yeni seviyeye geçilmiyor ise
        {
            GameManager.isDead = true; //öldür
            gameObject.SetActive(false); //topu kaldır
            pedal.SetActive(false); //pedalı kaldır
            GameOverUI.SetActive(true); //oyun bitti ekranını göster
        }
    }

    public void Push()
    {
        float x = 0; //vektörün x bileşeni
        while (x < 0.5f && x > -0.5f) //top dikey açıyla gitmesin
            x = Random.Range(-limit, limit); //limitler arasında rastgele sayı üret
        float y = Mathf.Sqrt(speed * speed - x * x); //Pisagor teoremi ile bileşke vektörü sabit tutacak y bileşenini belirle
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse); //tek seferlik itiş gücü uygula
        isRespawned = false; //yeniden başlatıldı değişkenini false yap
    }

    public void Restart()
    {
        GameManager.Live = 3; //canı ilk haline getir
        GameManager.Level = 1; //seviyeyi başa al
        GameManager.isDead = false; //dirilt
        SceneManager.LoadScene("Game"); //sahneyi yükle
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Brick")) //çarpılan nesne tupla ise
            Break.Play(); //tuğla kırma sesi çal
        else//değilse
            Crash.Play();//sekme sesi çal


        if (collision.gameObject.name.Equals("WallUp")) //çarpılan nesne üst duvar ise
        {
            if (posx == gameObject.transform.position.x) //konum, önceki ile aynı ise
                colCountx++; //sekme sayısını artır
            else //değilse
                colCountx = 0;//sekme sayısını sıfırla
            posx = gameObject.transform.position.x; //x konumuna al
        }
        else if (collision.gameObject.name.Equals("WallLeft") || collision.gameObject.name.Equals("WallRight")) //sağ veya sol duvar ise
        {
            if (posy == gameObject.transform.position.y)//konum, önceki ile aynı ise
                colCounty++; //sekme sayısını artır
            else
                colCounty = 0;//sekme sayısını sıfırla
            posy = gameObject.transform.position.y; //y konumunu al
        }
    }
}
