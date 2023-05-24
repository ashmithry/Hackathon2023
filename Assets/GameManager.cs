using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header ("UI References")]
    public GameObject UI;
    public GameObject GameOverUI;
    public GameObject GameWinUI;
    public GameObject pauseMenu;
    public int playerCount = 32;

    public void GameOver()
    {  
        UI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void PlayAgain()
    {
        playerCount = 32;
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
        if (player != null && playerCount == 1)
        {
            Debug.Log("Game Won!");
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("plunderRoyale");
            UI.SetActive(false);
            GameWinUI.SetActive(true);
        }

        GameObject textManager = GameObject.Find("PlayersLeftText");

        if(textManager != null)
        {
            textManager.GetComponent<TextMeshProUGUI>().text = "" + playerCount;
        }
    }

    public void OnKilled(GameObject entity)
    {
        playerCount--;
    }
}