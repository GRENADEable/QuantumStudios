using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    #region Public Variables
    public static AddScore instance = null;
    #endregion

    #region  Private Variables
    //private string addScoreURL = "http://localhost/highscore_unity/addScore.php"; //Local Databsae
    private string addScoreURL = "https://kahtimdar.000webhostapp.com/addScore.php"; //External Database
    #endregion

    #region Unity Callbacks
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region IEnums
    public void UploadScore(string score)
    {
        WWWForm scoreForm = new WWWForm();
        Debug.LogWarning("WWWForm Created"); //Testing

        //scoreForm.AddField("playerUsername", playerName);
        scoreForm.AddField("userScore", score);
        Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        Debug.LogWarning("Databse Accessed"); //Testing
    }
    #endregion
}
