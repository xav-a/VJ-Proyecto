using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject history;

    public void PlayScene()
    {
        SceneManager.LoadScene("SpaceLevel1");
    }

    public void History(GameObject history)
    {
        history.SetActive(!history.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }





}
