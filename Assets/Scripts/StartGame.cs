using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startButton;

    AudioManager audioManager;

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        startButton.onClick.AddListener(StartTheGame);
    }
    public void StartTheGame()
    {
        SceneManager.LoadScene("Game");
        audioManager.Play("Click");
    }
}
