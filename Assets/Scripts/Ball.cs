using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    public float bounceForce = 400f;

    AudioManager audioManager;

    public GameObject splitPrefab;

    private GameManager gameManager;
    public GameObject[] heartImages;

    public int minLives = 2;
    public int maxLives = 4;
    
    public int levelOffset = 10;
    private int currentLives;

    private void Start()
    {
        currentLives = CalculateStartingLives();
        UpdateHeartImages();


        audioManager = FindObjectOfType<AudioManager>(); // Seslerin kontrolü için atama
        rb = GetComponent<Rigidbody>(); // Fizik kontrolü için atama
        gameManager = FindObjectOfType<GameManager>();
    }

    private int CalculateStartingLives()
    {
        int startingLives = maxLives;

        if (GameManager.CurrentLevelIndex <= 5)
        {
            startingLives = minLives;
        }
        else if (GameManager.CurrentLevelIndex <= levelOffset)
        {
            startingLives = 3;
        }

        return startingLives;
    }

    private void UpdateHeartImages()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentLives)
                heartImages[i].gameObject.SetActive(true);
            else
                heartImages[i].gameObject.SetActive(false);
        }
    }

    public void DecreaseLives()
    {
        currentLives--;
        UpdateHeartImages();

        if (currentLives <= 0)
        {
            // Oyunu kaybetme iþlemleri
            GameOver();
        }
    }

    private void GameOver()
    {
        GameManager.gameOver = true;
        audioManager.Play("GameOver");
    }



    private void OnCollisionEnter(Collision other)
    {
        // Topun zýplamasý
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        audioManager.Play("Land");

        // split oluþturma
        GameObject newsplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.19f, transform.position.z), transform.rotation);
        newsplit.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        newsplit.transform.parent = other.transform;

        // Hangi materiale dokunduysak ona göre iþlem uygula
        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        if (materialName == "Safe (Instance)")
        {
            
        }
        if (materialName == "UnSafe (Instance)")
        {
            DecreaseLives();
            gameManager.IncreaseDeadCounter();
            audioManager.Play("Hurt");
        }
        if (materialName == "LastRing (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
            audioManager.Play("LevelUp");
        }
    }
}
