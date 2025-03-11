using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// �V�X�e�������擾���AUI Text�ɗ�������
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
    [Tooltip("���̎��")]
    [SerializeField]
    InformationType _infoType;

    [Tooltip("�\���t�H�[�}�b�g")]
    [SerializeField]
    string _format = "{0}";

    [Tooltip("�\������String")]
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

        // ������̍X�V
        _updateString.Invoke(message);
    }
}
