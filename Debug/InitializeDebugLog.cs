using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Development Build �̎����� Debug.Log���o�͂���悤�ɂ���
/// </summary>
public class InitializeDebugLog
{
    // �����^�C�����s���ɓ��삷��i�ő��j
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
    }
}