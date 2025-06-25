using Photon.Pun;
using Photon.Realtime;
using PlayFab.EconomyModels;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform chattingField;
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
                // 내 클라이언트에 메시지 띄우는 작업
                GameObject newMsg = Instantiate(Msg, chattingField);
                newMsg.GetComponent<Text>().text = inputField.text;
                inputField.text = "";
            }
        }

        // UpdateChattingField();
    }

    public void UpdateChattingField()
    {
        // 상대 채팅을 내 채팅창에 띄워주는 작업?
        // ???
    }

}
