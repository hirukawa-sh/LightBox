using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;

namespace Game.UI
{
    /// <summary>
    /// �`���[�g���A����p
    /// </summary>
    public class TutorialUIController : MonoBehaviour
    {
        /// <summary>
        /// AR���[�h����t���O
        /// </summary>
        [SerializeField]
        ScriptableBooleanValue _isARMode;

        /// <summary>
        /// �N���A�ς݂̃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        ScriptableIntValue _clearedStageNumber;

        /// <summary>
        /// ���݂̃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        int _currentStageNumber;

        void Start()
        {
            // AR���[�h�œ��쒆�̓`���[�g���A����\�����Ȃ�
            if (_isARMode.Value == true)
            {
                gameObject.SetActive(false);
            }
            else
            {
                // ���݂̃X�e�[�W���N���A�ς݂Ȃ�`���[�g���A����\�����Ȃ��悤�ɂ���
                if (_clearedStageNumber.Value < _currentStageNumber)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}