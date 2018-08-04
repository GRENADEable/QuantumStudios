using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Public Variables

    #endregion

    #region Private Variables
    private Image bgImage;
    private Image joyImgage;
    #endregion

    #region Callbacks
    public virtual void OnPointerDown(PointerEventData data)
    {

    }

    public virtual void OnPointerUp(PointerEventData data)
    {

    }

    public virtual void OnDrag(PointerEventData data)
    {

    }
    #endregion

    #region My Functions

    #endregion
}
