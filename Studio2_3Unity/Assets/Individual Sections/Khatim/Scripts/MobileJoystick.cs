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
    private Image joyImage;
    #endregion

    #region Callbacks
    void Start()
    {
        bgImage = GetComponent<Image>();
        joyImage = transform.GetChild(0).GetComponent<Image>();
    }
    #endregion

    #region My Functions
    public virtual void OnDrag(PointerEventData data)
    {
        /*Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle())*/
    }
    public virtual void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
    }

    public virtual void OnPointerUp(PointerEventData data)
    {

    }
    #endregion
}
