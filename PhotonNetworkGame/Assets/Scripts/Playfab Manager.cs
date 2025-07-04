using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public void Success(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = version;

        StartCoroutine(Connect());

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

    IEnumerator Connect()
    {
        // Master Server로 연결
        PhotonNetwork.ConnectUsingSettings();

        // 서버 연결이 완료되거나 시간 초과될 때 까지 대기
        while (PhotonNetwork.IsConnectedAndReady == false)
        {   
            yield return null;
        }

        // 특정 로비를 생성하여 진입하는 함수
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
