using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableValue;
using Game.Settings;

namespace Game.UI
{
    /// <summary>
    /// �X�e�[�W�N���AUI
    /// </summary>
    public class StageClearUIController : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField]
        ScriptableIntValue _currentStageNumberValue;

        [SerializeField]
        ScriptableFloatValue _clearTimeValue;

        [SerializeField]
        ScriptableIntValue _touchCountValue;

        [SerializeField]
        ScriptableIntValue _hintCountValue;

        [Header("Clear Time")]
        [SerializeField]
        Text _clearTimeText;

        [SerializeField]
        string _clearTimeFormat = "{0:mm\\:ss\\.fff}";

        [Header("Touch Count")]
        [SerializeField]
        Text _touchCountText;

        [SerializeField]
        string _touchCountFormat = "x {0:000}";

        [Header("Hint Count")]
        [SerializeField]
        Text _hintCountText;

        [SerializeField]
        string _hintCountFormat = "x {0:000}";

        [SerializeField]
        GameObject _noHintClearUI;

        [Header("New Record")]
        [SerializeField]
        GameObject _newRecordUI;

        void Start()
        {
            // �X�^�[�g����NewRecord�\�����\��
            _newRecordUI.SetActive(false);

            // NoHintClear��\��
            _noHintClearUI.SetActive(false);
        }

        /// <summary>
        /// UI�X�V
        /// </summary>
        public void UpdateUI()
        {
            // Text�R���|�[�l���g�̍X�V
            _clearTimeText.text = string.Format(_clearTimeFormat, System.TimeSpan.FromMilliseconds(_clearTimeValue.Value));
            _touchCountText.text = string.Format(_touchCountFormat, _touchCountValue.Value);
            _hintCountText.text = string.Format(_hintCountFormat, _hintCountValue.Value);

            // �m�[�q���g�Ȃ�NoHintClear�\��
            if (_hintCountValue.Value == 0)
            {
                _noHintClearUI.SetActive(true);
            }
        }

        /// <summary>
        /// �V�L�^�C�x���g�����������ꍇ
        /// </summary>
        public void OnNewRecord()
        {
            // NewRecord�\��
            _newRecordUI.SetActive(true);
        }
    }
}