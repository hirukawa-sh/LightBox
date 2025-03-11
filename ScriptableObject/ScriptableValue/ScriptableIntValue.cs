using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableValue
{
    /// <summary>
    /// グローバル変数のように使える ScriptableValue
    /// </summary>
    // ↓アセットを生成する場合はコメントアウトを解除する
    [CreateAssetMenu(fileName = "IntValue", menuName = "ScriptableValue/ScriptableIntValue", order = 1)]
    public class ScriptableIntValue : BaseScriptableValue<int>
    {
    }
}