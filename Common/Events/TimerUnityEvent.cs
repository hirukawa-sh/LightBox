using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

/// <summary>
/// ��莞�Ԍ�ɌĂ΂��UnityEvent
/// </summary>
public class TimerUnityEvent: MonoBehaviour
{
    /// <summary>
    /// �҂����ԁi�b�j
    /// </summary>
    [SerializeField]
    float _waitTimer;

    /// <summary>
    /// �Ă΂��C�x���g
    /// </summary>
    [SerializeField]
    UnityEvent _onTimerEvent;

    public void OnTimerEvent()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(_waitTimer)).Subscribe(_ => _onTimerEvent.Invoke());
    }
}
