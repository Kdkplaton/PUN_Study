using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;
    [SerializeField] InputField input_email;
    [SerializeField] InputField input_password;
    [SerializeField] GameObject failurePanel;

    void Update()
    {
        Input_KeyBoard();       // TabŰ �̵� ����
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
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    void Failure(PlayFabError playFabError)
    {
        input_email.text = "";
        input_password.text = "";

        string msg = playFabError.GenerateErrorReport();
        Debug.Log(msg);

        failurePanel.GetComponent<Failure>().Message(msg);
        failurePanel.SetActive(true);
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
                
                if (inputfield != null)     // ��Ŀ�� �̵��� �Է�â�� ���õ��� �ʴ� ���
                { inputfield.OnPointerClick(new PointerEventData(system)); }
                    
                system.SetSelectedGameObject(next.gameObject);
            }
        }
    }
}
