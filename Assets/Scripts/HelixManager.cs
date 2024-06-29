using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelixManager : MonoBehaviour
{
   public GameObject[] rings;
    public TextMeshProUGUI ringtext;

    public int noOfRings;
   public float ringDistance = 5f;
   float yPos;

    int minRings = 5;
    int maxRings = 15;
    int levelOffset = 30;

   private void Start () {

        noOfRings = Mathf.Clamp(GameManager.CurrentLevelIndex + minRings, minRings, maxRings);
        if (GameManager.CurrentLevelIndex >= levelOffset)
        {
            int levelMultiplier = Mathf.FloorToInt((GameManager.CurrentLevelIndex - levelOffset) / 10) + 1;
            noOfRings += levelMultiplier;
        }

        for (int i=0;i< noOfRings;i++){
            if(i==0){
                //ilk halka spawnı için
                SpawnRings (0);
            }
            else {
                // orta halkalar spawnı için 0 ve son dahil değil
                SpawnRings(Random.Range(1, rings.Length - 1));
            }
        }
    // son halka spawnı
    SpawnRings (rings.Length - 1);
    ringtext.text = "Helix sayısı: "+noOfRings.ToString();
    }

    private void Update()
    {
        
    }

    void SpawnRings(int index) {
    GameObject newRing = Instantiate (rings[index], new Vector3 (transform.position.x, yPos, transform.position.z), Quaternion.identity);
    yPos -= ringDistance;
        newRing.transform.parent = transform;
   }
}
