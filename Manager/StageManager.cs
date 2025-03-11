using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableValue;

namespace Game
{
    /// <summary>
    /// �X�e�[�W�Ǘ��N���X�A�X�e�[�W�ɑ΂��ĂȂ�₩��⏈������
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        /// <summary>
        /// ������̐e�ƂȂ�I�u�W�F�N�g
        /// </summary>
        [SerializeField]
        Transform _parent;

        /// <summary>
        /// �X�e�[�W���X�g
        /// </summary>
        [SerializeField]
        StageListData _stageList;

        /// <summary>
        /// ���݂̃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        ScriptableIntValue _currentStageNumber;

        /// <summary>
        /// �N���A�ς݃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        ScriptableIntValue _cleardStageNumber;

        /// <summary>
        /// �Q�[���V�[���ǂݍ��݃C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onGameSceneLoadEvent;

        /// <summary>
        /// �^�C�g���V�[���ǂݍ��݃C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onTitleSceneLoadEvent;

        /// <summary>
        /// �^�b�`�������C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onDisableTouchEvent;

        /// <summary>
        /// �^�b�`�L�����C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onEnableTouchEvent;

        /// <summary>
        /// ���݂̃X�e�[�W
        /// </summary>
        StageController _currentStage;

        /// <summary>
        /// �X�e�[�W����
        /// </summary>
        public void CreateStage()
        {
            var stageData = _stageList.ListData[_currentStageNumber.Value];
            _currentStage = Instantiate(stageData, _parent);

            Debug.Log($"[{gameObject}] Stage Creation is Finished.");
        }

        /// <summary>
        /// Retry�{�^��
        /// </summary>
        public void RetryStage()
        {
            // �Q�[���V�[���ǂݍ���
            onGameSceneLoadEvent.Invoke();
        }

        /// <summary>
        /// Next�{�^��
        /// </summary>
        public void NextStage()
        {
            // �ŏI�X�e�[�W�łȂ����
            if (_currentStageNumber.Value < _stageList.ListData.Count - 1)
            {
                // �X�e�[�W��i�߂�
                _currentStageNumber.Value++;

                // �Q�[���V�[���ǂݍ���
                onGameSceneLoadEvent.Invoke();
            }
            // �ŏI�X�e�[�W���N���A�����ꍇ
            else
            {
                // �^�C�g���V�[���ǂݍ��݁i�b��j
                onTitleSceneLoadEvent.Invoke();
            }
        }

        /// <summary>
        /// �X�e�[�W�N���A
        /// </summary>
        public void StageClear()
        {
            // �^�b�`������
            onDisableTouchEvent.Invoke();

            // ���̃X�e�[�W�����N���A�Ȃ�A�X�e�[�W�ԍ����L�^����
            if (_currentStageNumber.Value == _cleardStageNumber.Value)
            {
                _cleardStageNumber.Value = _currentStageNumber.Value + 1;
            }
        }

        /// <summary>
        /// �q���g�@�\���s
        /// </summary>
        public async void Hint()
        {
            // �^�b�`������
            onDisableTouchEvent.Invoke();

            // �q���g���s�i�A�j���[�V�����I���܂őҋ@�j
            await _currentStage.HintAction();

            // �܂��X�e�[�W�N���A�ɂȂ��Ă��Ȃ��Ȃ�
            if (_currentStage.IsCleared == false)
            {
                // �^�b�`�ėL����
                onEnableTouchEvent.Invoke();
            }
        }
    }
}