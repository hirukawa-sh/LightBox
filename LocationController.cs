using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// ����ݒ�𐧌䂷��
    /// </summary>
    public class LocationController : MonoBehaviour
    {
        [Tooltip("����ݒ�f�[�^")]
        [SerializeField]
        LanguageSettingsData _languageSettings;

        [Tooltip("�����������C�x���g")]
        [SerializeField]
        UnityEvent _initializeCompleted;
        
        IEnumerator Start()
        {
            // LocalizationSettings�̏����� yield�Ȃ̂ŁA���s���郁�\�b�h�͗vIenumerator
            yield return LocalizationSettings.InitializationOperation;

            // �����������C�x���g���s
            if (_initializeCompleted != null)
            {
                _initializeCompleted.Invoke();
            }

            ChangeLocation();
        }

        /// <summary>
        /// ���݂� LocationIndex �� Locale �ɐ؂�ւ���
        /// </summary>
        public void ChangeLocation()
        {
            var currentIndex = _languageSettings.LocationIndex.Value;
            var locale = LocalizationSettings.AvailableLocales.Locales[currentIndex];
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}