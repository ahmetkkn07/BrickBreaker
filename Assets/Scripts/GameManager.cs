using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int Live = 3;
    public static int Level = 1;
    public static bool isDead = false;

    public GameObject GameUI;
    public Text LiveText;
    public Text ScoreText;

    void Start()
    {
        GameManager.Live = 3;
        GameManager.Level = 1;
        GameManager.isDead = false;
    }

    void Update()
    {
        LiveText.text = "Kalan Hak\n" + GameManager.Live.ToString();
        ScoreText.text = "Seviye\n" + GameManager.Level.ToString();
    }

    
}
