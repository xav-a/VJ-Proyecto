﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float time = 10f;

    private bool timerIsActive = true;

    public void Awake()
    {
        timeText = GetComponent<Text>();
    }

    public void Update()
    {
        if (timerIsActive)
        {
            time -= Time.deltaTime;
            timeText.text = "00 : " + time.ToString("f0");
            if (time <= 0)
            {
                Debug.Log("HERE");
                timerIsActive = false;
                SceneManager.LoadScene("ShootDemo3");
            }
        }

    }
}
