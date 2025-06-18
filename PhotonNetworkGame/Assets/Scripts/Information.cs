using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Information : MonoBehaviourPunCallbacks
{
    [SerializeField] string roomName;
    [SerializeField] Text titleText;

    public void OnConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void UpdateDetails(RoomInfo roominfo)
    {
        titleText.text = string.Format("{0} ({1}/{2})", roominfo.Name, roominfo.PlayerCount, roominfo.MaxPlayers);
    }


}
