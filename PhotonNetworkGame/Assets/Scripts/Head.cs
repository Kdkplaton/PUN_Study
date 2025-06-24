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
        // rotationX에 마우스로 입력한 값 저장
        rotationX += Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;

        // rotationX에 값을 -65~65 사이의 값으로 제한
        rotationX = Mathf.Clamp(rotationX, -65, 65);
        // ps. rotation.x는 값이 커질수록 아래쪽으로 회전한다

        // 보정된 rotationX 를 적용
        head.localRotation = Quaternion.Euler(-1*rotationX, 0, 0);
    }

}
