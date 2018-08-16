using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerOnline : Photon.PunBehaviour
{
    #region Public Variables
    public float timerCount = 0f;
    public float scoreCount;
    public float pointsPerSec;
    public GameObject player;
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text scoring;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        scoring = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (scoring.text != null)
        {
            scoring.text = "Score: " + Mathf.Round(scoreCount);
            scoreCount += pointsPerSec * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPos = player.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
    }
    #endregion
}
