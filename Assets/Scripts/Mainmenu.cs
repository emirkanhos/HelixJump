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

        // Butonlara t�klan�nca ne yapmalar� gerekti�i atanmas� 
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
        PlayerPrefs.SetInt("CurrentLevelIndex", 1);// Level say�m�z� s�f�rlar
        PlayerPrefs.SetInt("DeadCounter", 0);// Yanma say�m�z� s�f�rlar
        audioManager.Play("Explosion");// Patlama sesi �almak i�in
        settingsPanel.SetActive(false);// Paneli kapatmak i�in
    }

    void ExitGame()
    {
        Debug.Log("��k�� Yap�ld�!");// Unity i�erisinde ��k �al��mad��� i�in t�kland���n� anlamam�za yarayan konsol ��kt�s� verir
        audioManager.Play("Click");// Click sesi �almak i�in
        Application.Quit();// Telefondayken uygulamadan ��kmay� sa�lar
    }

    private void OpenSettingsPanel()
    {
        // SettingsPanel'i aktifle�tir
        settingsPanel.SetActive(true);
        audioManager.Play("Click");
    }

    private void CloseSettingsPanel()
    {
        // SettingsPanel'i kapat�r
        settingsPanel.SetActive(false);
        audioManager.Play("Click");
    }
}
