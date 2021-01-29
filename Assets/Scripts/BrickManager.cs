using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickManager : MonoBehaviour
{
    public List<GameObject> bricks = new List<GameObject>(); //ekrandaki tuğlaları tutan liste
    public GameObject brickPrefab; //prefabi tutan değişken
    public GameObject GameOverUI; //oyun bitti ekranı
    public Text LeftBrictText; //kalan tuğla sayısının yazıldığı Text
    public Text BrokenBrickText;//kırılan tuğla sayısının yazıldığı Text
    public float spacing; //tuğlalar arası boşluk
    public int rows; //satır sayısı
    public int columns;//sütun sayısı

    private GameManager GameManager;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //GameManager nesnesini al
        GameManager.showToast("Boşluk tuşuna basarak oyuna başlayabilirsiniz.\nSağ ve sol ok tuşları ile pedalı hareket ettirebilirsiniz.", 3); //ekranda toast mesaj göster
        ResetLevel(); //leveli resetle
    }

    void Update()
    {
        if (bricks.Count == 0) //tuğla sayısı sıfır ise
            LevelUp(); //sonraki seviyeye geç
        GameManager.Level = rows - 1; //leveli ayarla
        LeftBrictText.text = "Kalan Tuğla\n" + bricks.Count; //ekranda kalan tuğlayı göster
        BrokenBrickText.text = "Kırılan Tuğla\n" + ((rows - 1) * columns - bricks.Count); //ekranda kırılan tuğlayı göster

    }

    public void ResetLevel()
    {
        foreach (GameObject brick in bricks) //tuğlalarda gezin
            Destroy(brick); //hepsini yok et
        bricks.Clear(); //listeyi temizle

        for (int x = 0; x < columns; x++) //satır
        {
            for (int y = 0; y < rows; y++) //sütun
            {
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(
                    x * (brickPrefab.transform.localScale.x + spacing),
                    -y * (brickPrefab.transform.localScale.y + spacing)); //parametrelere göre tuğlaların oluşacağı konumu hesapla
                GameObject brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity); //tuğlaları oluştur
                bricks.Add(brick); //tuğlayı listeye ekle
            }
        }
    }

    public void LevelUp()
    {
        if (rows <= 25) //25 satırdan az ise
        {
            if (rows > 1) //birinci seviyede değilse
                GameManager.showToast("Tebrikler, yeni bölüme geçtiniz!\nBoşluk tuşuna basarak oyuna başlayabilirsiniz.", 3); //tebrik mesajı göster
            rows++; //satırı artır
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().Respawn(isLevelUp: true); //topu başlangıca al
            ResetLevel();//tuğlaları sıfırla ve baştan oluştur
            GameManager.Live = 2; //can sayısını her seviye için 3 olarak ayarla (kalan can 2)
        }
        else //25satırı geçerse sn seviye bitmiş demek
        {
            GameManager.showToast("Tebrikler, oyunu bitirdiniz.", 3); //tebrik mesaji göster
            GameOverUI.SetActive(true); //oyun bitti ekranı göster
        }
    }
}
