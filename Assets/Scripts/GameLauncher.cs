using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameLauncher : MonoBehaviour
{
    public Text usernameInput;
    public GameObject menuTutorial;

    public void OnStartClick()
    {
        PlayerPrefs.SetString("username", usernameInput.text);

        SceneManager.LoadScene(1);
    }

    public void Start()
    {
        string username = "Foo";
        
        if(PlayerPrefs.HasKey("username"))
        {
            username = PlayerPrefs.GetString("username");
        }

        usernameInput.text = username;
    }

    public void ToggleTutorial()
    {
        menuTutorial.SetActive(!menuTutorial.activeSelf);
    }
}
