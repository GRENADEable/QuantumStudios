using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScore", menuName = "Username and Highscore")]
public class HighScoreObj : ScriptableObject
{
    public string playerName;
    public string playerScore;
}
