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
    GameObject failure;

    private void Awake()
    {
        failure = GameObject.Find("Failure Panel");
    }

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
        string msg = playFabError.GenerateErrorReport();
        Debug.Log(msg);

        failure.SetActive(true);
        failure.GetComponent<Failure>().Message(msg);
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
