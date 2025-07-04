using Photon.Pun;
using Photon.Realtime;
using PlayFab.EconomyModels;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform content;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] InputField inputField;
    GameObject Msg;

    void Start()
    {
        Msg = (GameObject)Resources.Load("Msg");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inputField.ActivateInputField();

            if (inputField.text.Length <= 0)
            { return; }
            else
            {
                string msg = $"{PhotonNetwork.LocalPlayer.NickName} : {inputField.text}";

                // RpcTarget.All : ���� �뿡 �ִ� ��� Ŭ���̾�Ʈ (�ڽ� ����)
                // RpcTarget.Others : �ڽ� ���� ������ Ŭ���̾�Ʈ ���
                // RpcTarget.MasterClient : ������(����) Ŭ���̾�Ʈ ��
                photonView.RPC("Talk", RpcTarget.All, msg);

                inputField.text = "";
            }
        }
    }

    [PunRPC]
    public void Talk(string msg)
    {
        // prefab�� �ϳ� ����
        GameObject newMsg = Instantiate(Msg);
        
        // ������ ������Ʈ�� Text ������Ʈ �����Ͽ� text ����
        newMsg.GetComponent<Text>().text = msg;

        // ��ũ�� �� - content ������Ʈ�� �ڽ����� ���
        newMsg.transform.SetParent(content);

        // Canvas�� �������� ����ȭ
        Canvas.ForceUpdateCanvases();

        // ��ũ���� ��ġ�� �ʱ�ȭ
        scrollRect.verticalNormalizedPosition = 0.0f;

    }

}
