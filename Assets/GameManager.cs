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

    public void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null && ShipData.playersAlive == 1)
        {
            Debug.Log("Game Won!");
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("plunderRoyale");
            UI.SetActive(false);
            GameWinUI.SetActive(true);
        }
    }
}