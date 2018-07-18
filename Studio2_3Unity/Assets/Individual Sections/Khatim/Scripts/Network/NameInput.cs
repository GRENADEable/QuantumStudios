using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(InputField))]
public class NameInput : MonoBehaviour
{
    #region Private Variables
    static string playerName = "PlayerName";
    #endregion

    #region Callbacks
    void Start()
    {
        string defaultName = "";
        InputField field = this.GetComponent<InputField>();
        if (field != null)
        {
            if (PlayerPrefs.HasKey(playerName))
            {
                defaultName = PlayerPrefs.GetString(playerName);
                field.text = defaultName;
            }
        }

        PhotonNetwork.playerName = defaultName;
    }
    #endregion

    #region Functions
    public void SetName(string value)
    {
        PhotonNetwork.playerName = value + " ";
        PlayerPrefs.SetString(playerName, value);
    }
    #endregion
}
