using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region Private Variables
    public GameObject Player;
    [SerializeField]
    private Vector3 Offset;
    //private RectTransform playerCanvas;
    [SerializeField]
    private Vector3 textOffset;
    #endregion

    void Awake()
    {
        //playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<RectTransform>();
    }
    void Start()
    {
        if (Player != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            Offset = transform.position - Player.transform.position;
        }

        //textOffset = playerCanvas.transform.position - Player.transform.position;

        //playerCanvas.transform.position = playerCanvas.transform.position + textOffset;
    }
    void LateUpdate()
    {
        if (Player != null)
        {
            transform.position = Player.transform.position + Offset;
        }
    }
}
