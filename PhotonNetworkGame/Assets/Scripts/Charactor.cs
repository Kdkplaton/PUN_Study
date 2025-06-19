using Photon.Pun;
using UnityEngine;

public class Charactor : MonoBehaviourPun
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Camera virtualCamera;
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        DisableCamera();
        // direction = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            Control();

            Move();
        }
    }

    public void DisableCamera()
    {
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
        
    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }
    public void Move()
    {
        characterController.Move(direction);
    }

}
