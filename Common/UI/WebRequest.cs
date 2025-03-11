using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Webへのアクセスを行う
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