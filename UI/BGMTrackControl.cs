using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Settings;
using ScriptableEvent;

namespace Game.UI
{
    /// <summary>
    /// BGM�g���b�N�̐���UI
    /// </summary>
    public class BGMTrackControl : MonoBehaviour
    {
        /// <summary>
        /// �T�E���h�̐ݒ�
        /// </summary>
        [SerializeField]
        SoundSettingsData _soundSettings;

        /// <summary>
        /// BGM�̃��X�g
        /// </summary>
        [SerializeField]
        AudioListData _bgmList;

        /// <summary>
        /// �O�̊y�ȃ{�^��
        /// </summary>
        [SerializeField]
        Button _leftButton;

        /// <summary>
        /// ���̊y�ȃ{�^��
        /// </summary>
        [SerializeField]
        Button _rightButton;

        /// <summary>
        /// �g���b�N�\��
        /// </summary>
        [SerializeField]
        Text _trackDisplayText;

        /// <summary>
        /// �g���b�N�\���̃t�H�[�}�b�g
        /// </summary>
        [SerializeField]
        string _trackFormat = "{0:000}";

        /// <summary>
        /// BGM�Đ��C�x���g
        /// </summary>
        [SerializeField]
        GameEvent _onPlayBGMEvent;

        // Start is called before the first frame update
        void Start()
        {
            UpdateTrackText();
        }

        private void OnEnable()
        {
            // �{�^���C�x���g�o�^
            _leftButton.onClick.AddListener(OnLeftButtonPushed);
            _rightButton.onClick.AddListener(OnRightButtonPushed);
        }

        private void OnDisable()
        {
            // �{�^���C�x���g����
            _leftButton.onClick.RemoveListener(OnLeftButtonPushed);
            _rightButton.onClick.RemoveListener(OnRightButtonPushed);
        }

        // �g���b�N�ԍ��̕\�����X�V
        public void UpdateTrackText()
        {
            // �g���b�N�ԍ��͎��l + 1�i�[���\�L��h�����߁j
            _trackDisplayText.text = string.Format(_trackFormat, _soundSettings.BGMID.Value + 1);
        }

        // �g���b�N�ԍ��؂�ւ�
        void ChangeTrackID(int value)
        {
            var bgmid = _soundSettings.BGMID.Value;
            bgmid += value;

            // ID�ԍ���0 �` �ő�l�͈͓̔��ɗ}����
            if (bgmid < 0)
            {
                bgmid = _bgmList.ListData.Count - 1;
            }

            if (_bgmList.ListData.Count <= bgmid)
            {
                bgmid = 0;
            }

            _soundSettings.BGMID.Value = bgmid;
        }

        /// <summary>
        /// ���{�^�����쎞
        /// </summary>
        void OnLeftButtonPushed()
        {
            ChangeTrackID(-1);
            UpdateTrackText();
            _onPlayBGMEvent.Raise();
        }

        /// <summary>
        /// �E�{�^�����쎞
        /// </summary>
        void OnRightButtonPushed()
        {
            ChangeTrackID(1);
            UpdateTrackText();
            _onPlayBGMEvent.Raise();
        }
    }
}