using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region Private Variables
    public GameObject player1;
    public GameObject player2;
    [SerializeField]
    private Vector3 Offset;
    //private RectTransform playerCanvas;
    //private Vector3 textOffset;
    #endregion

    void Awake()
    {
        //playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<RectTransform>();
    }
    void Start()
    {
        if (player1 != null)
        {
            player1 = GameObject.FindGameObjectWithTag("Player1");
            Offset = transform.position - player1.transform.position;
        }

        if (player2 != null)
        {
            player2 = GameObject.FindGameObjectWithTag("Player2");
            Offset = transform.position - player2.transform.position;
        }

        //textOffset = playerCanvas.transform.position - Player.transform.position;

        //playerCanvas.transform.position = playerCanvas.transform.position + textOffset;
    }
    void LateUpdate()
    {
        if (player1 != null)
        {
            transform.position = player1.transform.position + Offset;
        }

        if (player2 != null)
        {
            transform.position = player2.transform.position + Offset;
        }
    }
}
