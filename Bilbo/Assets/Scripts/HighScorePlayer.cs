using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScorePlayer
{
    private string _name;
    private int _score;

    public HighScorePlayer(string name, int score)
    {
        _name = name;
        _score = score;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetScore()
    {
        return _score;
    }
}

