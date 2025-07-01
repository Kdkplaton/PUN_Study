using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Failure : MonoBehaviour
{
    [SerializeField] Text failureText;

    public void Message(string error)
    { failureText.text = error; }

    public void Btn_Clicked()
    {
        failureText.text = "";
        gameObject.SetActive(true);
    }
}
