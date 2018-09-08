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
    [SerializeField]
    private bool scoreUploaded;
    #endregion

    #region Private Variables
    //private string addScoreURL = "http://localhost/highscore_unity/addScore.php"; //Local Database
    private string addScoreURL = "https://kahtimdar.000webhostapp.com/addScore.php"; //External Database
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
            //ScoreStats(showUserField.text, showScoreField.text);
            StartCoroutine(ScoreStats(showUserField.text, showScoreField.text));
            scoreUploaded = true;
        }
    }

    /*public void ScoreStats(string playerName, string playerScore)
    {
        WWWForm scoreForm = new WWWForm();
        //Debug.LogWarning("WWWForm Created"); //Testing

        scoreForm.AddField("playerUsername", playerName);
        scoreForm.AddField("userScore", playerScore);
        //Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        Debug.LogWarning("Score Uploaded to Database");
    }*/

    private IEnumerator ScoreStats(string playerName, string playerScore)
    {
        WWWForm scoreForm = new WWWForm();
        Debug.LogWarning("WWWForm Created"); //Testing

        scoreForm.AddField("playerUsername", playerName);
        scoreForm.AddField("userScore", playerScore);
        Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        yield return dbLink;
        /*if (dbLink.text == "Working")
            Debug.LogWarning("Score Uploaded to Database");
        else if (dbLink.text == "Error")
            Debug.LogWarning("Score Upload Failed");*/
    }
    #endregion

}
