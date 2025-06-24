using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    double time;
    double initializeTime;

    int min;
    int sec;
    int mil;

    void Start()
    {
        // 시간측정 시작지점
        initializeTime = PhotonNetwork.Time;
    }

    void Update()
    {
        time = PhotonNetwork.Time - initializeTime;
        min = (int)(time / 60);
        sec = (int)(time % 60);
        mil = (int)(time % 1 * 100);

        string elapsedTime = $"{min:D2}:{sec:D2}:{mil:D2}";
        Debug.Log("경과시간 : " + elapsedTime);
    }



}
