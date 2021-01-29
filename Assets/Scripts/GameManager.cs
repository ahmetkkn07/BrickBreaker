using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int Live = 3; //can sayısı
    public static int Level = 1;//mevcut level
    public static bool isDead = false; //öldü mü?

    public GameObject GameUI; //oyun arayüzünü tutan nesne
    public Text LiveText; //can yazısı
    public Text ScoreText; //skor yazısı
    public Text LevelText; //toast yapmak için kullanılan metin


    void Start()
    {
        GameManager.Live = 3; //canı üçe ayarla
        GameManager.Level = 1; //leveli başa al
        GameManager.isDead = false; //ölmedi
    }

    void Update()
    {
        LiveText.text = "Kalan Hak\n" + GameManager.Live.ToString(); //kalan canı göster
        ScoreText.text = "Seviye\n" + GameManager.Level.ToString(); //seviyeyi göster
    }

    public void showToast(string text, int duration)
    {
        StartCoroutine(showToastCOR(text, duration)); //coroutine çağır
    }

    private IEnumerator showToastCOR(string text, int duration)
    {
        Color orginalColor = LevelText.color; //yazının orijinal rengini al

        LevelText.text = text; //metni ayarla
        LevelText.enabled = true; //göster
        
        yield return fadeInAndOut(LevelText, true, 0.5f);//Fade in efekti

        float counter = 0;//süreyi sıfırla
        while (counter < duration) //süre kadar bekle
        {
            counter += Time.deltaTime;
            yield return null;
        }

        yield return fadeInAndOut(LevelText, false, 0.5f);//Fade out efekti

        LevelText.enabled = false; //metni gizle
        LevelText.color = orginalColor; //orijinal rengi geri ayarla
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        
        float a, b;
        if (fadeIn)//fadein ise
        {
            //değerleri ayarla
            a = 0f; 
            b = 1f;
        }
        else //fadeout ise
        {
            //değerleri ayarla
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear; //rengi şeffaf yap
        float counter = 0f; //süreyi sıfırla

        while (counter < duration) //süre kadar
        {
            counter += Time.deltaTime; //zamanı artır
            float alpha = Mathf.Lerp(a, b, counter / duration); //geçiş yap

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha); //rengi ayarla
            yield return null;
        }
    }
}
