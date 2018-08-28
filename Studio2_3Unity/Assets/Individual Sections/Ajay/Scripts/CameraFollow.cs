using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region Private Variables
    public GameObject player;
    [SerializeField]
    private Vector3 Offset;
    //private RectTransform playerCanvas;
    [SerializeField]
    //private Vector3 textOffset;
    #endregion

    void Awake()
    {
        //playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<RectTransform>();
    }
    void Start()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Offset = transform.position - player.transform.position;
        }

        //textOffset = playerCanvas.transform.position - Player.transform.position;

        //playerCanvas.transform.position = playerCanvas.transform.position + textOffset;
    }
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + Offset;
        }
    }
}
