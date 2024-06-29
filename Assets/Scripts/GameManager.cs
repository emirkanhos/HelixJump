using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPannal;
    public GameObject levelWinPannal;
    public GameObject infoPanel;

    public Button infoButton;
    public Button closeInfoButton;

    public Material ballMaterial;
    public Material splitMaterial;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;
    public int deadCounter;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI deadText;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textGOScore;
    public TextMeshProUGUI textLWScore;

    private int score;
    private int best;

    AudioManager audioManager;

    public Slider ProggressBar;

    public Color randomColor;

    public static GameManager singleton;

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);

        if (singleton == null)
            singleton = this;
        else if (singleton != null)
            Destroy(gameObject);

        best = PlayerPrefs.GetInt("BestScore");
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        deadCounter = PlayerPrefs.GetInt("DeadCounter", 0);
        UpdateDeadText();
        score = 0;

        gameOver = false;
        levelWin = false;

        infoButton.onClick.AddListener(OpenInfoPanel);
        closeInfoButton.onClick.AddListener(CloseInfoPanel);


        SetRandomColors(); // Topun malzemesinin rengini rastgele olarak ayarla
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPannal.SetActive(true);
            textScore.gameObject.SetActive(false);
            textGOScore.text = "Skorunuz: "+score.ToString() + "\n Best: "+ best.ToString();
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(1);
                score = 0;
                textScore.gameObject.SetActive(true);
            }
        }

        currentLevelText.text = CurrentLevelIndex.ToString();
        nextLevelText.text = (CurrentLevelIndex + 1).ToString();

        //slider update
        int proggress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProggressBar.value = proggress;


        if (levelWin)
        {
            levelWinPannal.SetActive(true);
            Time.timeScale = 0;
            textLWScore.text = "Skorunuz: " + score.ToString() + "\n Best: " + best.ToString();
            textScore.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);
                score = 0;
                SceneManager.LoadScene(1);
                textScore.gameObject.SetActive(true);
            }
        }
        textScore.text = "Skor: " + score;
    }

    public void Addscore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    public void IncreaseDeadCounter()
    {
        deadCounter++;
        PlayerPrefs.SetInt("DeadCounter", deadCounter);
        UpdateDeadText();
    }

    private void UpdateDeadText()
    {
        deadText.text = deadCounter.ToString() + " defa yandınız";
    }

    public void SetRandomColors()
    {
        Color randomColor = Random.ColorHSV(); // Rastgele bir renk se�
        ballMaterial.color = randomColor; // Topun malzemesinin rengini se�ilen rastgele renk olarak ayarla
        splitMaterial.color = randomColor;
    }

    private void OpenInfoPanel()
    {
        // ResetPanel'i aktifle�tir
        infoPanel.SetActive(true);
        Time.timeScale = 0;
        UpdateDeadText();
        audioManager.Play("Click");
    }

    private void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        Time.timeScale = 1f;
        audioManager.Play("Click");
    }
}