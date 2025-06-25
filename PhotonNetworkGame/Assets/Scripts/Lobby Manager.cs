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
    [SerializeField] Text inputRoomName;

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
        string roomName = inputRoomName.text;
        OnCreateRoom(roomName);
        inputRoomName.text = null;
        roomCreation.SetActive(false);
    }
    public void CloseRoomCreation()
    {
        inputRoomName.text = null;
        roomCreation.SetActive(false);
    }

    public void OnCreateRoom(string newRoomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.CreateRoom(newRoomName, roomOptions);
    }

    public void CheckDuplication(List<RoomInfo> roomList, string roomName)
    {
        foreach (RoomInfo room in roomList)
        {
            if(room.Name == roomName)
            {
                Debug.Log("�ߺ� �̸� ����!");
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
            // Room ���� Ȯ��&�ݿ�
            if(roominfo.RemovedFromList == true)
            {
                dictionary.TryGetValue(roominfo.Name, out prefab);
                Destroy(prefab);
            }
            else    // Room�� ������ ����Ǵ� ��� (����, ����)
            {
                if(roominfo.IsVisible == true)
                {
                    for(int i=0; i<dictionary.Count; i++)
                    {
                        if (dictionary.ContainsKey(roominfo.Name) == false)        // Room�� ���� �Ǿ��� ���
                        {
                            string newRoomName = roominfo.Name;
                            GameObject newRoom = Instantiate(roomInit, parentTransform);
                            
                            newRoom.GetComponent<Information>().UpdateDetails(roominfo);
                            
                            dictionary.Add(newRoom.name, newRoom);
                            Debug.Log("Room " + newRoom.name + " Created!");
                        }
                        else        // Room�� ���� �Ǿ��� ���
                        {
                            dictionary.TryGetValue(roominfo.Name, out prefab);

                            prefab.GetComponent<Information>().UpdateDetails(roominfo);
                        }
                    }
                }
            }

        }


    }


    public void RemoveRoom()
    {



    }

}