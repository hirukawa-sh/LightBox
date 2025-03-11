using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scroll View のスクロールバーを初期位置にリセットする
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
    [Tooltip("水平方向の初期位置")]
    [SerializeField]
    HorizontalNormalizedPosition _horizontalPosition;

    public enum VerticalNormalizedPosition
    {
        None,
        Top,
        Middle,
        Bottom,
    }
    [Tooltip("垂直方向の初期位置")]
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
