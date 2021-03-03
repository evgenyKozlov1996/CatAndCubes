using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnGameStart(){
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnGameEnd(){
        Application.Quit();
    }
}
