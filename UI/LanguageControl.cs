using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using Game.Settings;

namespace Game.UI
{
    /// <summary>
    /// ����I���R���g���[��
    /// LanguageSettingsData.LocationIndex��؂�ւ��邾���A���ۂ�Location�ύX��LocationController�ɂčs��
    /// </summary>
    public class LanguageControl : MonoBehaviour
    {
        /// <summary>
        /// ����ݒ�
        /// </summary>
        [SerializeField]
        LanguageSettingsData _languageSettings;

        /// <summary>
        /// ���{�^��
        /// </summary>
        [SerializeField]
        Button _leftButton;

        /// <summary>
        /// �E�{�^��
        /// </summary>
        [SerializeField]
        Button _rightButton;

        /// <summary>
        /// �\�������e�L�X�g
        /// </summary>
        [SerializeField]
        Text _displayLanguageText;

        /// <summary>
        /// Locale��ύX�����ۂɌĂ΂��
        /// </summary>
        [SerializeField]
        UnityEvent _onLocationChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            // �����\��
            UpdateText();
        }

        void OnEnable()
        {
            // �{�^���C�x���g�o�^
            _leftButton.onClick.AddListener(OnLeftButtonPushed);
            _rightButton.onClick.AddListener(OnRightButtonPushed);
        }

        void OnDisable()
        {
            // �{�^���C�x���g�o�^
            _leftButton.onClick.RemoveListener(OnLeftButtonPushed);
            _rightButton.onClick.RemoveListener(OnRightButtonPushed);
        }

        /// <summary>
        /// ���{�^���������ꂽ
        /// </summary>
        void OnLeftButtonPushed()
        {
            ChangeIndex(-1);
            UpdateText();
        }

        /// <summary>
        /// �E�{�^���������ꂽ
        /// </summary>
        void OnRightButtonPushed()
        {
            ChangeIndex(1);
            UpdateText();
        }

        /// <summary>
        /// Index�̐؂�ւ��@min �� max �Ń��[�v����
        /// </summary>
        /// <param name="addIndex"></param>
        void ChangeIndex(int addIndex)
        {
            var min = 0;
            var max = LocalizationSettings.AvailableLocales.Locales.Count - 1;
            var currentIndex = _languageSettings.LocationIndex.Value;

            currentIndex += addIndex;
            if (max < currentIndex)
                currentIndex = min;
            if (currentIndex < min)
                currentIndex = max;

            _languageSettings.LocationIndex.Value = currentIndex;

            _onLocationChangeEvent.Invoke();
        }

        /// <summary>
        /// Text�̍X�V
        /// </summary>
        void UpdateText()
        {
            _displayLanguageText.text = LocalizationSettings.SelectedLocale.LocaleName;
        }
    }
}