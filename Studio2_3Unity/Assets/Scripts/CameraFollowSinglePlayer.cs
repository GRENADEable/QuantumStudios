using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSinglePlayer : MonoBehaviour
{

    #region Private Variables
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 Offset;
    #endregion
    void Start()
    {
        if (player != null)
            Offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        if (player != null)
            transform.position = player.transform.position + Offset;
    }
}
