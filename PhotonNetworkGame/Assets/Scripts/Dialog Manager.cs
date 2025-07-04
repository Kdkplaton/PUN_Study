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

                // RpcTarget.All : 현재 룸에 있는 모든 클라이언트 (자신 포함)
                // RpcTarget.Others : 자신 제외 나머지 클라이언트 모두
                // RpcTarget.MasterClient : 마스터(방장) 클라이언트 만
                photonView.RPC("Talk", RpcTarget.All, msg);

                inputField.text = "";
            }
        }
    }

    [PunRPC]
    public void Talk(string msg)
    {
        // prefab을 하나 생성
        GameObject newMsg = Instantiate(Msg);
        
        // 생성한 오브젝트의 Text 컴포넌트 접근하여 text 설정
        newMsg.GetComponent<Text>().text = msg;

        // 스크롤 뷰 - content 오브젝트의 자식으로 등록
        newMsg.transform.SetParent(content);

        // Canvas를 수동으로 동기화
        Canvas.ForceUpdateCanvases();

        // 스크롤의 위치를 초기화
        scrollRect.verticalNormalizedPosition = 0.0f;

    }

}
