using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _finalScoreText;
    [SerializeField] private Text _newHighScoreText;
    [SerializeField] private InputField _newHighScoreInputField;
    [SerializeField] private Button _submitHighScoreButton;
    
    // Start is called before the first frame update
    void Start()
    {
        int score = FindObjectOfType<UIManager>().GetScore();
        if (FindObjectOfType<UIManager>()._bossBeaten)
        {
            _gameOverText.text = "Victory! You bet the Game!";
            //clear remaining hearts in victory screen
            FindObjectOfType<HealthBar>().ClearBar();
        }
        bool newHighScore = IsNewHighScore(score);
        if (newHighScore)
        {
            _newHighScoreText.gameObject.SetActive(true);
            _newHighScoreInputField.gameObject.SetActive(true);
            _submitHighScoreButton.gameObject.SetActive(true);
        }
        string finalText = "Final Score: " + score;
        _finalScoreText.text = finalText;
    }

    private bool IsNewHighScore(int newScore)
    {
        bool isNewHighScore = false;
        List<HighScorePlayer> highScores = SubmitHighScoreButton.GetHighScore();
        // new high scores if it´s the first ones
        if (highScores.Count < 7)
            isNewHighScore = true;
        foreach (HighScorePlayer player in highScores)
        {
            if (newScore > player.GetScore())
                isNewHighScore = true;
        }
        return isNewHighScore;
    }
}
