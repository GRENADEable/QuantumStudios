using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSinglePlayer : MonoBehaviour
{

    #region Private Variables
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Vector3 Offset;
    #endregion

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        Offset = transform.position - Player.transform.position;
    }
    void LateUpdate()
    {
        if (Player != null)
            transform.position = Player.transform.position + Offset;
    }
}
