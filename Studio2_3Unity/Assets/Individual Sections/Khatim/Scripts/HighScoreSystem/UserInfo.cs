using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    #region Public Variables
    public InputField showUserField;
    public InputField showScoreField;
    public HighScoreObj high;
    private bool scoreUploaded;
    #endregion

    #region Private Variables
    //private string addScoreURL = "http://localhost/highscore_unity/addScore.php"; //Local Database
    private string addScoreURL = "kahtimdar.000webhostapp.com/addScore.php"; //External Database
    #endregion

    #region Unity Callbacks
    void Start()
    {
        showUserField.text = high.playerName;
        showScoreField.text = high.playerScore;
        scoreUploaded = false;
    }
    #endregion

    #region My Functions
    public void UploadScore()
    {
        if (!scoreUploaded)
        {
            ScoreStats(high.playerName, high.playerScore);
            scoreUploaded = true;
        }
    }

    public void ScoreStats(string playerName, string playerScore)
    {
        WWWForm scoreForm = new WWWForm();
        //Debug.LogWarning("WWWForm Created"); //Testing

        scoreForm.AddField("playerUsername", playerName);
        scoreForm.AddField("userScore", playerScore);
        //Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        Debug.LogWarning("Score Uploaded to Database");
    }
    #endregion

}
