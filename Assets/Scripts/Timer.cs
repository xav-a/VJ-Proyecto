using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject message;

    public GameObject gameOver;

    public GameObject level;

    private GameObject player;

    public Text timeText;

    public Text livesText;
   

    public float time = 10f;

    private bool timerIsActive = true;

    public void Awake()
    {
        timeText = GetComponent<Text>();

        livesText = GameObject.Find("Lives").GetComponent<Text>();
    }

    public void Update()
    {
        if (timerIsActive)
        {
            time -= Time.deltaTime;
            //then drag and drop the Username_field
            timeText.text = "00 : " + time.ToString("f0");
            if (time <= 0 )
            {
                timerIsActive = false;
                //SceneManager.LoadScene("ShootDemo3");
                message.SetActive(!message.activeSelf);
                player = GameObject.Find("Ship");
                Destroy(player);
            }

            if (livesText.text == "X0")
            {
                timerIsActive = false;
                gameOver.SetActive(!gameOver.activeSelf);
            }

            if (level.activeSelf)
            {
                timerIsActive = false;
            }
        }

    }

}
