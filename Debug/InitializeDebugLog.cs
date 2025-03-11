using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Development Build の時だけ Debug.Logを出力するようにする
/// </summary>
public class InitializeDebugLog
{
    // ランタイム実行時に動作する（最速）
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
    }
}