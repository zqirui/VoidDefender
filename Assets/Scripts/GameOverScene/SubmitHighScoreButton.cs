using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Debug = UnityEngine.Debug;


public class SubmitHighScoreButton : MonoBehaviour
{
    [SerializeField] private InputField _highScoreNameInputField;
    private void Awake()
    {
        string filePath =  Application.dataPath + "/highscores.csv";
        _highScoreFilePath = filePath;
    }

    private static string _highScoreFilePath;
    
    public void SubmitNewHighScorePlayer()
    {
        string name = _highScoreNameInputField.text;
        int score = FindObjectOfType<UIManager>().GetScore();
        HighScorePlayer newPlayer = new HighScorePlayer(name, score);
        
        //get old high scores
        List<HighScorePlayer> highScores = GetHighScore();

        //removed last player
        if (highScores.Count == 7)
        {
            HighScorePlayer toBeRemovedPlayer = highScores[highScores.Count - 1];
            highScores.Remove(toBeRemovedPlayer);
        }
        
        //insert the player based on score 
        int index = -1;
        if (highScores.Count < 7)
            index = highScores.Count;
        foreach (HighScorePlayer player in highScores)
        {
            //if it´s the lowest of all scores then on that last spot
            if (newPlayer.GetScore() > player.GetScore())
            {
                index = highScores.IndexOf(player);
                break;
            }
        }
        
        highScores.Insert(index, newPlayer);
        
        //write out changes to csv file
        WriteOutNewHighScore(highScores);
        
        //make Inputfield and button invisible
        _highScoreNameInputField.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
    }

    public static List<HighScorePlayer> GetHighScore()
    {
        /*
         * returns a empty list when file is not initialized
         */
        List<HighScorePlayer> highScores = new List<HighScorePlayer>();
        if (File.Exists(_highScoreFilePath))
        {
            StreamReader inputStream = new StreamReader(_highScoreFilePath);

            List<string> lines = new List<string>();
            while (!inputStream.EndOfStream)
            {
                string line = inputStream.ReadLine();
                lines.Add(line);
            }

            inputStream.Close();

            foreach (string line in lines)
            {
                string[] lineContent =  line.Split(',');
                //first one is the name, second the score
                string name = lineContent[0];
                int score = System.Int32.Parse(lineContent[1]);
                highScores.Add(new HighScorePlayer(name, score));
            }
        }
        return highScores;
    }

    public static List<HighScorePlayer> GetHighScore(string filePath)
    {
        _highScoreFilePath = filePath;
        return GetHighScore();
    }
    private void WriteOutNewHighScore(List<HighScorePlayer> newHighScores)
    {
        Debug.Log("High Score File Path: " + _highScoreFilePath);
        StreamWriter writer = new StreamWriter(_highScoreFilePath);

        for (int i = 0; i < newHighScores.Count; i++)
        {
            writer.WriteLine(newHighScores[i].GetName()+ "," + newHighScores[i].GetScore());
        }
        
        writer.Flush();
        writer.Close();
    }
    
}
