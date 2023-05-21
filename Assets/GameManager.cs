using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("UI References")]
    public GameObject UI;
    public GameObject GameOverUI;
    public GameObject GameWinUI;

    public void GameOver()
    {
        UI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}