using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableValue
{
    /// <summary>
    /// �O���[�o���ϐ��̂悤�Ɏg���� ScriptableValue
    /// </summary>
    // ���A�Z�b�g�𐶐�����ꍇ�̓R�����g�A�E�g����������
    [CreateAssetMenu(fileName = "BooleanValue", menuName = "ScriptableValue/ScriptableBooleanValue", order = 1)]
    public class ScriptableBooleanValue : BaseScriptableValue<bool>
    {
    }
}