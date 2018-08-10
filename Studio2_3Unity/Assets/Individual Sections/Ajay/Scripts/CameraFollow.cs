using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region Private Variables
    public GameObject Player;
    [SerializeField]
    private Vector3 Offset;
    #endregion

    void Awake()
    {
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Offset = transform.position - Player.transform.position;
    }
    void LateUpdate()
    {
        if (Player != null)
            transform.position = Player.transform.position + Offset;
    }
}
