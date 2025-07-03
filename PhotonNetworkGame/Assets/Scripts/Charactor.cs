using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class Charactor : MonoBehaviourPun
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Camera virtualCamera;
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    [SerializeField] float mouseX;
    [SerializeField] float rotationSpeed;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        DisableCamera();
        DisableMyCap();
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            // UI에 포커스가 있다면 입력을 무시
            if(EventSystem.current.currentSelectedGameObject != null)
            { return; }

            Control();

            Move();

            Rotate();
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

            virtualCamera.GetComponent<AudioListener>().enabled = false;
        }
        
    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        mouseX += Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime;
    }
    public void Move()
    {
        characterController.Move(characterController.transform.TransformDirection(direction) * speed * Time.deltaTime);
    }
    public void Rotate()
    {
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    public void DisableMyCap()
    {
        if (photonView.IsMine)
        {
            MeshRenderer[] capRenderers;
            capRenderers = GameObject.Find("Cap").GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer renderer in capRenderers)
            { renderer.enabled = false; }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Authorized"))
        {
            PhotonView clone = other.GetComponent<PhotonView>();

            if(clone.IsMine)
            { PhotonNetwork.Destroy(other.gameObject); }
        }

    }

}
