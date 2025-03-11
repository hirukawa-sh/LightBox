using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

/// <summary>
/// 一定時間後に呼ばれるUnityEvent
/// </summary>
public class TimerUnityEvent: MonoBehaviour
{
    /// <summary>
    /// 待ち時間（秒）
    /// </summary>
    [SerializeField]
    float _waitTimer;

    /// <summary>
    /// 呼ばれるイベント
    /// </summary>
    [SerializeField]
    UnityEvent _onTimerEvent;

    public void OnTimerEvent()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(_waitTimer)).Subscribe(_ => _onTimerEvent.Invoke());
    }
}
