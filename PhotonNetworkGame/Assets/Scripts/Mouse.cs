using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Mouse : MonoBehaviourPun
{

    void Start()
    { SetMouse(false); }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (Cursor.visible) { SetMouse(false); }
                else { SetMouse(true); }
            }
        }
            
    }

    void SetMouse(bool state)
    {
        if(photonView.IsMine)
        {
            Cursor.visible = state;
            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        }
    }

    private void OnDestroy()
    { SetMouse(true); }


}
