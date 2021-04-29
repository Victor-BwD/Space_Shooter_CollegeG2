using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

     Text scoreText;
     int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreText();
        }

    }

	// Use this for initialization
	void Start ()
    {

        scoreText = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void UpdateScoreText () {

        string scoreStr = string.Format("{0:0000000}", score);
        scoreText.text = scoreStr;
		
	}
}
