using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExitGames.Client.Photon.StructWrapping;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;
    [SerializeField] InputField input_email;
    [SerializeField] InputField input_password;

    void Update()
    {
        Input_KeyBoard();       // Tab키 이동 구현
    }

    void Success(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = version;

        PhotonNetwork.ConnectUsingSettings();

    }

    public void Access()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = input_email.text,
            Password = input_password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, Success, Failure);
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }

    void Input_KeyBoard()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            var system = EventSystem.current;
            Selectable next = system.currentSelectedGameObject?.GetComponent<Selectable>();

            if (next != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    next = next.FindSelectableOnUp();
                else
                    next = next.FindSelectableOnDown();
            }

            if (next != null)
            {
                InputField inputfield = next.GetComponent<InputField>();
                
                if (inputfield != null)     // 포커스 이동시 입력창이 선택되지 않는 경우
                { inputfield.OnPointerClick(new PointerEventData(system)); }
                    
                system.SetSelectedGameObject(next.gameObject);
            }
        }
    }
}
