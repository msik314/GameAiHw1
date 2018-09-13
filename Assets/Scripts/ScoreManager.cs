using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private Text highScoreText;
	[SerializeField] private Text gameScoreText;

	int highScore;
	int gameScore = 0;

	// Use this for initialization
	void Start()
	{
		if (!PlayerPrefs.HasKey("HighScore"))
		{
			highScore = 0;
			PlayerPrefs.SetInt("HighScore", 0);
		}
		else
			highScore = PlayerPrefs.GetInt("HighScore");
		WriteScores();
	}

	void WriteScores()
	{
		gameScoreText.text = "" + gameScore;
		highScoreText.text = "" + highScore;
	}

	public void AddPoints(int points)
	{
		gameScore += points;
		if (gameScore > highScore)
			highScore = gameScore;
		WriteScores();
	}

	public void SaveScore()
	{
		PlayerPrefs.SetInt("HighScore", highScore);
	}
}
