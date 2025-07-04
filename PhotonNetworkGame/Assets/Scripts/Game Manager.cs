using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    double time;
    double initializeTime;
    int min, sec, mil;
    [SerializeField] Text timeText;
    [SerializeField] GameObject pausePanel;

    void Start()
    {
        if (photonView.IsMine)
        { SetMouse(false); }

        if(PhotonNetwork.IsMasterClient)
        {
            initializeTime = PhotonNetwork.Time;
            photonView.RPC("InitializeTime", RpcTarget.AllBuffered, initializeTime);
        }
    }

    void Update()
    {
        time = PhotonNetwork.Time - initializeTime;
        min = (int)(time / 60);
        sec = (int)(time % 60);
        mil = (int)(time % 1 * 100);

        string elapsedTime = $"{min:D2}:{sec:D2}:{mil:D2}";
        timeText.text = elapsedTime;
        //Debug.Log("경과시간 : " + elapsedTime);

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetMouse(true);
                pausePanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (Cursor.visible == false)
                { SetMouse(true); }
                else
                { SetMouse(false); }
            }
        }
    }

    public void Continue()
    {
        if (photonView.IsMine)
        {
            SetMouse(false);
            pausePanel.SetActive(false);
        }
    }

    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }  

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if(PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

    }

    [PunRPC]
    void InitializeTime(double time)
    { initializeTime = time; }

    void SetMouse(bool state)
    {
        if (photonView.IsMine)
        {
            Cursor.visible = state;
            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        }
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        { SetMouse(true); }
    }

}
