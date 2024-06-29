using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    public Button backButton;

    AudioManager audioManager;

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        backButton.onClick.AddListener(StartTheGame);
    }
    public void StartTheGame()
    {
        SceneManager.LoadScene("MainMenu");
        audioManager.Play("Click");
    }
}
