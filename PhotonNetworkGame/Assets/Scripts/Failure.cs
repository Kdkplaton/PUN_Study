using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Failure : MonoBehaviour
{
    [SerializeField] Text failureText;

    public void Message(string error)
    {
        string msg = error.Split(" ", 2)[1];
        failureText.text = msg;
    }

    public void Confirm()
    {
        failureText.text = "";
        gameObject.SetActive(false);
    }
}
