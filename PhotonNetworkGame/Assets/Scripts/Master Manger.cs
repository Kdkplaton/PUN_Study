using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;

public class MasterManger : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform initTransform;
    int val = -2;
    WaitForSeconds WFS_5f = new WaitForSeconds(5f);

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        { Execute(); }
    }

    void Execute()
    {
        StartCoroutine(CreateUnit());
    }

    public IEnumerator CreateUnit() 
    {
        Vector3 initPos = initTransform.position;
        Quaternion initRot = initTransform.rotation;

        while(true)
        {
            if(PhotonNetwork.CurrentRoom != null)
            {
                if (!GameObject.Find("Unit(Clone)"))
                {
                    initPos += new Vector3(val, 0, 0);
                    PhotonNetwork.InstantiateRoomObject("Unit", initPos, initRot);
                }
                else {
                    // 실행 내용 없음
                }
            }

            yield return WFS_5f;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("");
        PhotonNetwork.SetMasterClient(newMasterClient);
    }

}
