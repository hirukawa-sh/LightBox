using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Web�ւ̃A�N�Z�X���s��
/// </summary>
public class WebRequest : MonoBehaviour
{
    [SerializeField]
    string _url = "http://";

    public void Open()
    {
        Application.OpenURL(_url);
    }
}