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
                // �� Ŭ���̾�Ʈ�� �޽��� ���� �۾�
                GameObject newMsg = Instantiate(Msg, chattingField);
                newMsg.GetComponent<Text>().text = inputField.text;
                inputField.text = "";
            }
        }

        // UpdateChattingField();
    }

    public void UpdateChattingField()
    {
        // ��� ä���� �� ä��â�� ����ִ� �۾�?
        // ???
    }

}
