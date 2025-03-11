using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// パーティクル停止時のコールバックを受け取る
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemStoppedCallback : MonoBehaviour
{
    [SerializeField]
    UnityEvent _onParticleSystemStopped;

    ParticleSystem _particle;

    void Awake()
    {
        _particle = GetComponent<ParticleSystem>();

        // パーティクルの設定を書き換え
        var mainModule = _particle.main;
        mainModule.stopAction = ParticleSystemStopAction.Callback;
        mainModule.loop = false;
    }

    /// <summary>
    /// パーティクル停止時に呼ばれる
    /// </summary>
    void OnParticleSystemStopped()
    {
        _onParticleSystemStopped.Invoke();
    }
}
