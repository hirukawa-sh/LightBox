using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �p�[�e�B�N����~���̃R�[���o�b�N���󂯎��
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

        // �p�[�e�B�N���̐ݒ����������
        var mainModule = _particle.main;
        mainModule.stopAction = ParticleSystemStopAction.Callback;
        mainModule.loop = false;
    }

    /// <summary>
    /// �p�[�e�B�N����~���ɌĂ΂��
    /// </summary>
    void OnParticleSystemStopped()
    {
        _onParticleSystemStopped.Invoke();
    }
}
