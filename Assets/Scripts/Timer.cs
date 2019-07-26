using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float time = 0.0f;

    float finishTime = 0.0f;

    public void Update()
    {
        time -= Time.deltaTime;
        timeText.text = "00 : 0" + time.ToString("f0");

        if (time <= 0)
        {
            SceneManager.LoadScene("SpaceLevel1");
        }
    }
}
