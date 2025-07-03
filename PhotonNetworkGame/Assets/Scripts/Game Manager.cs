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
        SetMouse(false);

        initializeTime = PhotonNetwork.Time;
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
        }
    }

    public void Continue()
    {
        if(photonView.IsMine)
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

    void SetMouse(bool state)
    {
        if (photonView.IsMine)
        {
            Cursor.visible = state;
            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        }
    }
    private void OnDestroy()
    { SetMouse(true); }

}
