using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Public Variables
    public float timerCount = 0f;
    public float scoreCount;
    public float pointsPerSec;
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text scoring;
    private Text timerSecond;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        scoring = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        timerSecond = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
    }

    void Update()
    {
        if (scoring.text != null)
        {
            scoring.text = "Score: " + Mathf.Round(scoreCount);
            scoreCount += pointsPerSec * Time.deltaTime;
        }

        if (timerSecond.text != null)
        {
            timerSecond.text = timerCount.ToString("Time: 0");
            timerCount += Time.deltaTime;
        }
    }
    #endregion

    #region  My Functions

    #endregion
}
