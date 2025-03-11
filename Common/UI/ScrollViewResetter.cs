using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scroll View �̃X�N���[���o�[�������ʒu�Ƀ��Z�b�g����
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class ScrollViewResetter : MonoBehaviour
{
    public enum HorizontalNormalizedPosition
    {
        None,
        Left,
        Middle,
        Right,
    }
    [Tooltip("���������̏����ʒu")]
    [SerializeField]
    HorizontalNormalizedPosition _horizontalPosition;

    public enum VerticalNormalizedPosition
    {
        None,
        Top,
        Middle,
        Bottom,
    }
    [Tooltip("���������̏����ʒu")]
    [SerializeField]
    VerticalNormalizedPosition _verticalPosition;

    ScrollRect _scrollRect;

    void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    void OnEnable()
    {
        switch (_horizontalPosition)
        {
            case HorizontalNormalizedPosition.Left:
                _scrollRect.horizontalNormalizedPosition = 0.0f;
                break;

            case HorizontalNormalizedPosition.Middle:
                _scrollRect.horizontalNormalizedPosition = 0.5f;
                break;

            case HorizontalNormalizedPosition.Right:
                _scrollRect.horizontalNormalizedPosition = 1.0f;
                break;
        }

        switch (_verticalPosition)
        {
            case VerticalNormalizedPosition.Top:
                _scrollRect.verticalNormalizedPosition = 1.0f;
                break;

            case VerticalNormalizedPosition.Middle:
                _scrollRect.verticalNormalizedPosition = 0.5f;
                break;

            case VerticalNormalizedPosition.Bottom:
                _scrollRect.verticalNormalizedPosition = 0.0f;
                break;
        }
        Debug.Log($"{this} : Scroll Rect Reset");
    }
}
