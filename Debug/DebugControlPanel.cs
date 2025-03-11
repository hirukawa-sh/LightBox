#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableValue;

namespace Game.Debugger
{
    /// <summary>
    /// �f�o�b�O�p���̓t�H�[��
    /// </summary>
    public class DebugControlPanel : MonoBehaviour
    {
        [SerializeField]
        InputField _cleardStageNumberInput;

        [SerializeField]
        InputField _hintCountInput;

        [SerializeField]
        InputField _stageClearTimeInput;

        [SerializeField]
        InputField _touchCountInput;

        [SerializeField]
        ScriptableIntValue _cleardStageNumberValue;

        [SerializeField]
        ScriptableIntValue _hintCountValue;

        [SerializeField]
        ScriptableFloatValue _stageClearTimeValue;

        [SerializeField]
        ScriptableIntValue _touchCountValue;

        void OnEnable()
        {
            // �l -> InputField
            _cleardStageNumberInput.text = $"{_cleardStageNumberValue.Value}";
            _hintCountInput.text = $"{_hintCountValue.Value}";
            _stageClearTimeInput.text = $"{_stageClearTimeValue.Value}";
            _touchCountInput.text = $"{_touchCountValue.Value}";
        }

        public void OnApply()
        {
            // InputField -> �l
            _cleardStageNumberValue.Value = int.Parse(_cleardStageNumberInput.text);
            _hintCountValue.Value = int.Parse(_hintCountInput.text);
            _stageClearTimeValue.Value = float.Parse(_stageClearTimeInput.text);
            _touchCountValue.Value = int.Parse(_touchCountInput.text);
        }
    }
}
#endif