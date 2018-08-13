using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerOffline : MonoBehaviour
{
    #region Public Variables
    public static GameManagerOffline instance = null;
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
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        scoring = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        timerSecond = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
    }

    void Update()
    {
        scoring.text = "Score: " + Mathf.Round(scoreCount);
        scoreCount += pointsPerSec * Time.deltaTime;

        timerSecond.text = timerCount.ToString("Time: 0");
        timerCount += Time.deltaTime;
    }
    #endregion

    #region  My Functions

    #endregion
}
