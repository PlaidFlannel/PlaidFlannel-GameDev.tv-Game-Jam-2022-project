using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalLevelBeaconCounter : MonoBehaviour
{
    //detect when final beacon is lit and light the street light above
    //activate a platform to access the final goal
    //transition to next level will be with a sensor as usual
    public TextMeshProUGUI scoreText;
    private float nextStageDelay;
    private int score;
    [SerializeField] GameObject[] beacons;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (score == beacons.Length)
        {
            Invoke("RaisePlatform", nextStageDelay);
        }

    }
    public void UpdateScore(int scoreToAdd)
    {
        int beaconsCount = beacons.Length;
        score += scoreToAdd;
        Debug.Log(beaconsCount);
        scoreText.text = "Beacons lit: " + score + "/" + beaconsCount;
    }
}
