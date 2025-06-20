using Photon.Pun;
using UnityEngine;

public class CharactorManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 direction;

    void Start()
    {
        direction = InitRandomPosition();
        PhotonNetwork.Instantiate("Character", direction, Quaternion.identity);
    }

    Vector3 InitRandomPosition()
    {
        Vector3 newPos;
        float randX = Random.Range(0f, 6f);
        // y값은 고정
        float randZ = Random.Range(0f, 3f);

        newPos = new Vector3(randX, 4, randZ);
        return newPos;
    }

}
