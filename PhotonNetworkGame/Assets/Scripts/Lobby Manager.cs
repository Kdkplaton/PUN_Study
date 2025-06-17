using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
    [SerializeField] Transform parentTransform;
    GameObject roomInit;
    string input_roomName;
    [SerializeField] GameObject roomCreation;

    private void Start()
    {
        roomInit = (GameObject)Resources.Load("Room");
        roomCreation.SetActive(false);
    }

    public void StartRoomCreation()
    {
        roomCreation.SetActive(true);
    }
    public void ConfirmRoomCreation()
    {
        string roomName = GameObject.Find("Text_RoomName").GetComponent<Text>().text;
        OnCreateRoom(roomName);
        GetComponentInChildren<Text>().text = null;
        roomCreation.SetActive(false);
    }
    public void CloseRoomCreation()
    {
        GameObject.Find("Text_RoomName").GetComponent<Text>().text = null;
        roomCreation.SetActive(false);
    }

    public void OnCreateRoom(string newRoomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.CreateRoom(newRoomName, roomOptions);

        /*
        GameObject newRoom = Instantiate(roomInit, parentTransform);
        newRoom.name = newRoomName;
        newRoom.GetComponentInChildren<Text>().text = string.Format("{0} (1/4)", newRoomName);
        dictionary.Add(newRoom.name, newRoom);
        Debug.Log("Room " + newRoom.name + " Created!");
        */
    }

    public void CheckDuplication(List<RoomInfo> roomList, string roomName)
    {
        foreach (RoomInfo room in roomList)
        {
            if(room.Name == roomName)
            {
                Debug.Log("중복 이름 감지!");
                return;
            }
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach (RoomInfo roominfo in roomList)
        {
            // Room 제거 확인&반영
            if(roominfo.RemovedFromList == true)
            {
                dictionary.TryGetValue(roominfo.Name, out prefab);
                Destroy(prefab);
            }
            else    // Room의 정보가 변경되는 경우
            {

            }

        }


    }


    public void DeleteRoom()
    {



    }

}