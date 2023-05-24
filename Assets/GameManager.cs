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
    public GameObject pauseMenu;

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

    public void togglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null && ShipData.playersAlive == 0)
        {
            Debug.Log("Game Won!");
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("plunderRoyale");
            UI.SetActive(false);
            GameWinUI.SetActive(true);
        }
    }
}