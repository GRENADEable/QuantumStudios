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
    private Vector3 inputVec;
    #endregion

    #region Callbacks
    void Start()
    {
        bgImage = GetComponent<Image>();
        joyImage = transform.GetChild(0).GetComponent<Image>();
    }
    #endregion

    #region Pointer Events
    public virtual void OnDrag(PointerEventData data)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, data.position, data.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            inputVec = new Vector3(pos.x * 2f, 0f, pos.y * 2f);
            inputVec = (inputVec.magnitude > 1f) ? inputVec.normalized : inputVec;

            //Movement of Joystick Img;
            joyImage.rectTransform.anchoredPosition = new Vector3(inputVec.x * (bgImage.rectTransform.sizeDelta.x / 3), inputVec.z * (bgImage.rectTransform.sizeDelta.y / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        inputVec = Vector3.zero;
        joyImage.rectTransform.anchoredPosition = Vector3.zero;
    }
    #endregion

    #region My Functions
    public float Horizontal()
    {
        if (inputVec.x != 0)
            return inputVec.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }

    public float Vertical()
    {
        if (inputVec.x != 0)
            return inputVec.z;
        else
            return Input.GetAxisRaw("Vertical");
    }
    #endregion
}
