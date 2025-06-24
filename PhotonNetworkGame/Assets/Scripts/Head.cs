using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform head;
    [SerializeField] float rotationSpeed;
    float rotationX;

    void Update()
    {
        if(photonView.IsMine)
        {
            RotateX();
        }
        
    }

    public void RotateX()
    {
        // rotationX�� ���콺�� �Է��� �� ����
        rotationX += Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;

        // rotationX�� ���� -65~65 ������ ������ ����
        rotationX = Mathf.Clamp(rotationX, -65, 65);
        // ps. rotation.x�� ���� Ŀ������ �Ʒ������� ȸ���Ѵ�

        // ������ rotationX �� ����
        head.localRotation = Quaternion.Euler(-1*rotationX, 0, 0);
    }

}
