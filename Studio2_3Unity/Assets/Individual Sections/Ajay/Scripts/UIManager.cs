using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Public Variables
    public float scoreCount;
    public float pointsPerSec;
    public float countInterval;
    public HighScoreObj high;
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text scoring;
    private string addScoreURL = "http://localhost/highscore_unity/addScore.php";
    #endregion

    #region Unity Callbacks
    void Start()
    {
        scoring = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    void Update()
    {
        if (scoring.text != null)
        {
            scoring.text = "" + Mathf.Round(scoreCount);
            scoreCount += pointsPerSec * countInterval * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        //UploadScore(scoring.text);
        //UserInfo.instance.score = scoring.text;
        high.playerScore = scoring.text;
        Debug.LogWarning("Score Uploaded to Scriptable Objects");
    }

    /*public void UploadScore(string score)
    {
        WWWForm scoreForm = new WWWForm();
        //Debug.LogWarning("WWWForm Created"); //Testing

        //scoreForm.AddField("playerUsername", playerName);
        scoreForm.AddField("userScore", score);
        //Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        //Debug.LogWarning("Databse Accessed"); //Testing
    }*/
    #endregion
}
