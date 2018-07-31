using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBScript : MonoBehaviour
{
    #region Public Variables
    public GameObject FBLoginPanel;
    public GameObject FBLogoutPanel;
    public GameObject FBUsername;
    public GameObject FBProfilePicture;
    //public GameObject FBEmail;
    #endregion

    #region Private Variables

    #endregion

    #region Callbacks
    void Awake()
    {
        if (!FB.IsInitialized)
            FB.Init(InitializeCallback, OnHideUnity);
        else
            FB.ActivateApp();
    }
    #endregion

    #region My Functions
    void InitializeCallback()
    {
        if (FB.IsInitialized)
            FB.ActivateApp();
        else
            Debug.LogWarning("Initialize Failed");

        FBMenu(FB.IsLoggedIn);
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    #region Facebook Login & Logout
    public void FBLogin()
    {
        var permission = new List<string>()
        {
            "public_profile",//"email"
        };
        FB.LogInWithReadPermissions(permission, AuthCallback);
    }
    public void FBLogout()
    {
        FB.LogOut();
        Debug.LogWarning("Logged Out from Facebook");
        FBLoginPanel.SetActive(false);
        FBLogoutPanel.SetActive(true);
    }
    #endregion

    private void AuthCallback(ILoginResult result)
    {
        if (result.Error != null)
            Debug.LogWarning(result.Error);

        else if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.LogWarning(aToken.UserId);
            foreach (string perm in aToken.Permissions)
            {
                Debug.LogWarning(perm);
                Debug.LogWarning("User Logged In");
            }
        }
        else
            Debug.Log("User cancelled login");

        FBMenu(FB.IsLoggedIn);
    }

    #region Facebook Share
    public void FBShare()
    {
        FB.ShareLink(new System.Uri("https://developers.facebook.com/"), callback: ShareCallback);
    }

    void ShareCallback(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
            Debug.LogWarning("ShareLink Error: " + result.Error);
        else if (!string.IsNullOrEmpty(result.PostId))
            //Print post identifier of the shared content
            Debug.LogWarning(result.PostId);
        else
            //Share succeeded without postID
            Debug.LogWarning("ShareLink success!");
    }
    #endregion

    #region Facebook Invite
    //Need to Publish the App on the Play Store
    /*public void FBGameRequest()
    {
        FB.AppRequest("Yo, Join our Game", title: "Sharkbait");
    }

    public void FBInvite()
    {
        //Need to Publish the App on the Play Store
        FB.Mobile.AppInvite()
    }*/
    #endregion

    void FBMenu(bool IsLoggedIn)
    {
        if (IsLoggedIn)
        {
            FBLoginPanel.SetActive(true);
            FBLogoutPanel.SetActive(false);

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUser);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayImage);
            //FB.API("/me?fields=email", HttpMethod.GET, DisplayEmail);
        }
        else
        {
            FBLoginPanel.SetActive(false);
            FBLogoutPanel.SetActive(true);
        }

    }

    void DisplayUser(IResult result)
    {
        Text user = FBUsername.GetComponent<Text>();

        if (result.Error == null)
            user.text = "Welcome: " + result.ResultDictionary["first_name"];
        else
            Debug.LogWarning(result.Error);
    }

    void DisplayImage(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Image pic = FBProfilePicture.GetComponent<Image>();

            pic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.LogWarning("Profile Picture not Found");
        }
    }

    /*void DisplayEmail(IResult result)
    {
        Text useremail = FBEmail.GetComponent<Text>();

        if (result.Error == null)
            useremail.text = "Email: " + result.ResultDictionary["email"];
        else
            Debug.LogWarning(result.Error);
    }*/
    #endregion
}
