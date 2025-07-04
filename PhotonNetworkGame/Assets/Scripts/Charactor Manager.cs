using Photon.Pun;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharactorManager : MonoBehaviourPunCallbacks
{
    // [SerializeField] Vector3 direction;
    [SerializeField] List<Transform> transformList;

    void Start()
    {
        // direction = InitRandomPosition();

        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        Vector3 direction = transformList[index].position;
        PhotonNetwork.Instantiate("Character", direction, Quaternion.identity);

    }

    /*
    Vector3 InitRandomPosition()    // ������ġ ��������
    {
        Vector3 newPos;
        float randX = Random.Range(0f, 6f);
        // y���� ����
        float randZ = Random.Range(0f, 3f);

        newPos = new Vector3(randX, 4, randZ);
        return newPos;
    }
    */

}
