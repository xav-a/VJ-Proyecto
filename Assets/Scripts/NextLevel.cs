using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject message;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var otherObject = collider.gameObject;
        if (otherObject.tag == "Player")
        {
            GameObject.Destroy(otherObject);
            message.SetActive(!message.activeSelf);
        }
    }

    public void restartLevel1()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //message.SetActive(!message.activeSelf);
    }

    public void Level2()
    {
        SceneManager.LoadScene("SpaceLevel2");
    }

    public void ReturnLevel1()
    {
        SceneManager.LoadScene("SpaceLevel1");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }


}
