using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class Identifier : MonoBehaviourPunCallbacks
{
    [SerializeField] Text identifier;

    void Awake()
    {
        Load();
    }

    void Load()
    {
        PlayFabClientAPI.GetAccountInfo
        (
            new GetAccountInfoRequest(),
            Success,
            Failure
        );

    }

    void Success(GetAccountInfoResult getAccountInfoResult)
    {
        string DisplayName = getAccountInfoResult.AccountInfo.TitleInfo.DisplayName;
        Debug.Log("NickName : " + DisplayName);

        PhotonNetwork.LocalPlayer.NickName = DisplayName;
        
        if (photonView.IsMine)
        { identifier.text = PhotonNetwork.LocalPlayer.NickName; }
        else
        { identifier.text = photonView.Owner.NickName; }
          
    }
    void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }

}
