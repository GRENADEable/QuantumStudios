using UnityEngine;
using UnityEngine.UI;

public class ListOfRoom : MonoBehaviour
{
    #region Public Variables
    public bool isUpdated
    {
        get;
        set;
    }
    public string roomName
    {
        get;
        private set;
    }
    #endregion

    #region  Private Variables
    [SerializeField]
    private Text _roomNameText;
    private Text roomNameText
    {
        get
        {
            return _roomNameText;
        }
    }
    #endregion

    #region Unity Callbacks
    void Start()
    {
        GameObject canvasLobbyObj = CanvasManager.instance.canvasLobby.gameObject;
        if (canvasLobbyObj == null)
        {
            Debug.LogWarning("Lobby is Empty");
            return;
        }

        CanvasLobby canvasLobby = canvasLobbyObj.GetComponent<CanvasLobby>();
        Debug.LogWarning("Accessed Canvas Lobby Object");
        Button butt = GetComponent<Button>();
        Debug.LogWarning("Got the Button Commponent");
        butt.onClick.AddListener(() => canvasLobby.OnClickJoinRoom(roomNameText.text));
        Debug.LogWarning("Waiting for Click");

    }

    void OnDestroy()
    {
        Button butt = GetComponent<Button>();
        butt.onClick.RemoveAllListeners();
        Debug.LogWarning("Removed Listeners");
    }
    #endregion

    #region My Functions
    public void SetRoom(string setRoomText)
    {
        roomName = setRoomText;
        roomNameText.text = roomName;
        Debug.LogWarning("Room Set");
    }
    #endregion
}
