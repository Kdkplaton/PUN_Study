using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class CharactorManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 direction;

    void Start()
    {
        PhotonNetwork.Instantiate("Charactor", direction, Quaternion.identity);
        GameObject.Find("Camera").SetActive(false);
    }


}
