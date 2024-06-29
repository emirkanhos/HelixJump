using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    public Button settingsButton;
    public Button exitButton;
    public Button backButton;
    public Button evetButton;
    public Button hayirButton;

    public GameObject settingsPanel;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        // Butonlara týklanýnca ne yapmalarý gerektiði atanmasý 
        exitButton.onClick.AddListener(ExitGame);
        settingsButton.onClick.AddListener(OpenSettingsPanel);
        evetButton.onClick.AddListener(ResetProgress);
        hayirButton.onClick.AddListener(CloseSettingsPanel);
        backButton.onClick.AddListener(CloseSettingsPanel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", 1);// Level sayýmýzý sýfýrlar
        PlayerPrefs.SetInt("DeadCounter", 0);// Yanma sayýmýzý sýfýrlar
        audioManager.Play("Explosion");// Patlama sesi çalmak için
        settingsPanel.SetActive(false);// Paneli kapatmak için
    }

    void ExitGame()
    {
        Debug.Log("Çýkýþ Yapýldý!");// Unity içerisinde çýk çalýþmadýðý için týklandýðýný anlamamýza yarayan konsol çýktýsý verir
        audioManager.Play("Click");// Click sesi çalmak için
        Application.Quit();// Telefondayken uygulamadan çýkmayý saðlar
    }

    private void OpenSettingsPanel()
    {
        // SettingsPanel'i aktifleþtir
        settingsPanel.SetActive(true);
        audioManager.Play("Click");
    }

    private void CloseSettingsPanel()
    {
        // SettingsPanel'i kapatýr
        settingsPanel.SetActive(false);
        audioManager.Play("Click");
    }
}
