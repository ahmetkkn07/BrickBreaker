using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public float spacing;
    public GameObject brickPrefab;
    public GameObject GameOverUI;
    public Text LeftBrictText;
    public Text BrokenBrickText;
    public List<GameObject> bricks = new List<GameObject>();
    public Text LevelText;

    void Start()
    {
        showToast("Boşluk tuşuna basarak oyuna başlayabilirsiniz.\nSağ ve sol ok tuşları ile pedalı hareket ettirebilirsiniz.", 3);
        ResetLevel();
    }

    void Update()
    {
        if (bricks.Count == 0)
        {
            LevelUp();
        }
        GameManager.Level = rows - 1;
        LeftBrictText.text = "Kalan Tuğla\n" + bricks.Count;
        BrokenBrickText.text = "Kırılan Tuğla\n" + ((rows - 1) * columns - bricks.Count);

    }

    public void ResetLevel()
    {

        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }
        bricks.Clear();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(
                    x * (brickPrefab.transform.localScale.x + spacing),
                    -y * (brickPrefab.transform.localScale.y + spacing));
                GameObject brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
                bricks.Add(brick);
            }
        }
    }

    public void LevelUp()
    {
        if (rows <= 25)
        {
            if (rows > 1)
                showToast("Tebrikler, yeni bölüme geçtiniz!\nBoşluk tuşuna basarak oyuna başlayabilirsiniz.", 3);
            rows++;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().Respawn(isLevelUp: true);
            ResetLevel();
        }
        else
        {
            showToast("Tebrikler, oyunu bitirdiniz.", 3);
            GameOverUI.SetActive(true);
        }

    }


    void showToast(string text,
    int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = LevelText.color;

        LevelText.text = text;
        LevelText.enabled = true;

        //Fade in efekti
        yield return fadeInAndOut(LevelText, true, 0.5f);

        //süre kadar bekle
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out efekti
        yield return fadeInAndOut(LevelText, false, 0.5f);

        LevelText.enabled = false;
        LevelText.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //fadein mi değil mi?
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }
}
