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
    /// 言語選択コントローラ
    /// LanguageSettingsData.LocationIndexを切り替えるだけ、実際のLocation変更はLocationControllerにて行う
    /// </summary>
    public class LanguageControl : MonoBehaviour
    {
        /// <summary>
        /// 言語設定
        /// </summary>
        [SerializeField]
        LanguageSettingsData _languageSettings;

        /// <summary>
        /// 左ボタン
        /// </summary>
        [SerializeField]
        Button _leftButton;

        /// <summary>
        /// 右ボタン
        /// </summary>
        [SerializeField]
        Button _rightButton;

        /// <summary>
        /// 表示されるテキスト
        /// </summary>
        [SerializeField]
        Text _displayLanguageText;

        /// <summary>
        /// Localeを変更した際に呼ばれる
        /// </summary>
        [SerializeField]
        UnityEvent _onLocationChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            // 初期表示
            UpdateText();
        }

        void OnEnable()
        {
            // ボタンイベント登録
            _leftButton.onClick.AddListener(OnLeftButtonPushed);
            _rightButton.onClick.AddListener(OnRightButtonPushed);
        }

        void OnDisable()
        {
            // ボタンイベント登録
            _leftButton.onClick.RemoveListener(OnLeftButtonPushed);
            _rightButton.onClick.RemoveListener(OnRightButtonPushed);
        }

        /// <summary>
        /// 左ボタンが押された
        /// </summary>
        void OnLeftButtonPushed()
        {
            ChangeIndex(-1);
            UpdateText();
        }

        /// <summary>
        /// 右ボタンが押された
        /// </summary>
        void OnRightButtonPushed()
        {
            ChangeIndex(1);
            UpdateText();
        }

        /// <summary>
        /// Indexの切り替え　min ⇔ max でループする
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
        /// Textの更新
        /// </summary>
        void UpdateText()
        {
            _displayLanguageText.text = LocalizationSettings.SelectedLocale.LocaleName;
        }
    }
}