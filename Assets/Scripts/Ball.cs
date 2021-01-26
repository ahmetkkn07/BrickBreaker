using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameObject pedal;
    public GameObject GameOverUI;
    public Text LevelText;
    public AudioSource Crash;
    public AudioSource Break;

    private float speed = 8f;
    private float limit = 3f;
    public bool isRespawned = true;

    private float posx = -9999;
    private float posy = -9999;
    private int colCountx = 0;
    private int colCounty = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameOverUI.SetActive(false);
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRespawned)
        {
            pedal.transform.position = new Vector3(0, -4.5f, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Push();
            }
        }
        if (colCountx >= 2 || colCounty >= 2)
        {
            showToast("Top sıkıştığı için tekrar atış yapmalısınız!\nBoşluk tuşuna basarak atış yapabilirsiniz.", 3);
            transform.position = new Vector3(0, -4.1748f, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            pedal.transform.position = new Vector3(0, -4.5f, 0);
            isRespawned = true;
            colCountx = 0;
            colCounty = 0;
        }

    }

    public void Respawn(bool isLevelUp = false)
    {
        colCountx = 0;
        colCounty = 0;
        if (GameManager.Live > 0)
        {
            transform.position = new Vector3(0, -4.1748f, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            pedal.transform.position = new Vector3(0, -4.5f, 0);
            isRespawned = true;
            if (!isLevelUp)
                GameManager.Live--;
        }
        else
        {
            GameManager.isDead = true;
            GameOverUI.SetActive(true);
        }
    }

    public void Push()
    {
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-speed, speed), speed), ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, speed), ForceMode2D.Impulse);
        float x = Random.Range(-limit, limit);
        float y = Mathf.Sqrt(speed * speed - x * x);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        isRespawned = false;
    }

    public void Restart()
    {
        GameManager.Live = 3;
        GameManager.Level = 1;
        GameManager.isDead = false;
        SceneManager.LoadScene("Game");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Brick"))
            Break.Play();
        else
            Crash.Play();
        if (collision.gameObject.name.Equals("WallUp"))
        {
            if (posx == gameObject.transform.position.x)
            {
                colCountx++;
            }
            else
            {
                colCountx = 0;
            }
            posx = gameObject.transform.position.x;
        }
        else if (collision.gameObject.name.Equals("WallLeft") || collision.gameObject.name.Equals("WallRight"))
        {
            if (posy == gameObject.transform.position.y)
            {
                colCounty++;
            }
            else
            {
                colCounty = 0;
            }
            posy = gameObject.transform.position.y;
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
