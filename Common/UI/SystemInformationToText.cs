using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// システム情報を取得し、UI Textに流し込む
/// </summary>
public class SystemInformationToText : MonoBehaviour
{
    enum InformationType
    {
        ApplicationVersion,
        UnityVersion,
        ProductName,
        Platform,
        CompanyName,
    }
    [Tooltip("情報の種類")]
    [SerializeField]
    InformationType _infoType;

    [Tooltip("表示フォーマット")]
    [SerializeField]
    string _format = "{0}";

    [Tooltip("表示するString")]
    [SerializeField]
    UnityEvent<string> _updateString;

    // Start is called before the first frame update
    void Start()
    {
        var message = "";

        switch (_infoType)
        {
            case InformationType.ApplicationVersion:
                message = string.Format(_format, Application.version);
                break;

            case InformationType.UnityVersion:
                message = string.Format(_format, Application.unityVersion);
                break;

            case InformationType.ProductName:
                message = string.Format(_format, Application.productName);
                break;

            case InformationType.Platform:
                message = string.Format(_format, Application.platform);
                break;

            case InformationType.CompanyName:
                message = string.Format(_format, Application.companyName);
                break;
        }

        // 文字列の更新
        _updateString.Invoke(message);
    }
}
