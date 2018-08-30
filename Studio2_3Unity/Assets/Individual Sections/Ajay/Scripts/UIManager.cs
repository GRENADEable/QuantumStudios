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
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text scoring;
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
            scoring.text = "Score: " + Mathf.Round(scoreCount);
            scoreCount += pointsPerSec *countInterval* Time.deltaTime;
        }
    }
    #endregion
}
