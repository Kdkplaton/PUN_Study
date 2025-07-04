using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] GameObject profilePanel;
    [SerializeField] Text usernameText;

    void Awake()
    {
        PlayFabClientAPI.GetAccountInfo
        (
            new GetAccountInfoRequest(),
            Success,
            Failure
        );
        
    }
    
    public void Submit()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputField.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName
        (
            request,
            Success,
            Failure
        );

    }

    void Success(UpdateUserTitleDisplayNameResult updateUserTitleDisplayNameResult)
    {
        string name = updateUserTitleDisplayNameResult.DisplayName;
        Debug.Log("name : " + name);
        
        if (string.IsNullOrEmpty(name)) { return; }
        else {
            profilePanel.SetActive(false);
            usernameText.text = $"¹Ý°©½À´Ï´Ù, {name}!";
        }
    }
    void Success(GetAccountInfoResult getAccountInfoResult)
    {
        string name = getAccountInfoResult.AccountInfo.TitleInfo.DisplayName;
        Debug.Log("name : " + name);
        usernameText.text = $"¹Ý°©½À´Ï´Ù, {name}!";

        if (string.IsNullOrEmpty(name)) { profilePanel.SetActive(true); }
    }
    void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }

}
