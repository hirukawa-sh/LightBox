using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// 言語設定を制御する
    /// </summary>
    public class LocationController : MonoBehaviour
    {
        [Tooltip("言語設定データ")]
        [SerializeField]
        LanguageSettingsData _languageSettings;

        [Tooltip("初期化完了イベント")]
        [SerializeField]
        UnityEvent _initializeCompleted;
        
        IEnumerator Start()
        {
            // LocalizationSettingsの初期化 yieldなので、実行するメソッドは要Ienumerator
            yield return LocalizationSettings.InitializationOperation;

            // 初期化完了イベント発行
            if (_initializeCompleted != null)
            {
                _initializeCompleted.Invoke();
            }

            ChangeLocation();
        }

        /// <summary>
        /// 現在の LocationIndex の Locale に切り替える
        /// </summary>
        public void ChangeLocation()
        {
            var currentIndex = _languageSettings.LocationIndex.Value;
            var locale = LocalizationSettings.AvailableLocales.Locales[currentIndex];
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}