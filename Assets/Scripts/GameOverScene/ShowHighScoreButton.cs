using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScoreButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToUnshown;
    [SerializeField] private Text _highScoreTitle;
    [SerializeField] private Text _highScoreTable;
    
    public void ShowHighScore()
    {
        //hide some objects to free space
        foreach (var obj in _objectsToUnshown )
        {
            obj.gameObject.SetActive(false);
        }
        
        //show high score table and title
        _highScoreTable.gameObject.SetActive(true);
        _highScoreTitle.gameObject.SetActive(true);
        
        //get high score (top 7) and set text
        string highScoreTable = "";

        List<HighScorePlayer> highScores = SubmitHighScoreButton.GetHighScore();
        
        if(highScores.Count == 0)
            _highScoreTable.text = "No High Scores yet, play to submit!";
        else
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                highScoreTable += i + 1 + ". " + highScores[i].GetName() + "     " + highScores[i].GetScore() + "\n";
            }

            _highScoreTable.text = highScoreTable;
        }
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
    }
}
