using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Settings;
using ScriptableEvent;

namespace Game.UI
{
    /// <summary>
    /// BGMトラックの制御UI
    /// </summary>
    public class BGMTrackControl : MonoBehaviour
    {
        /// <summary>
        /// サウンドの設定
        /// </summary>
        [SerializeField]
        SoundSettingsData _soundSettings;

        /// <summary>
        /// BGMのリスト
        /// </summary>
        [SerializeField]
        AudioListData _bgmList;

        /// <summary>
        /// 前の楽曲ボタン
        /// </summary>
        [SerializeField]
        Button _leftButton;

        /// <summary>
        /// 次の楽曲ボタン
        /// </summary>
        [SerializeField]
        Button _rightButton;

        /// <summary>
        /// トラック表示
        /// </summary>
        [SerializeField]
        Text _trackDisplayText;

        /// <summary>
        /// トラック表示のフォーマット
        /// </summary>
        [SerializeField]
        string _trackFormat = "{0:000}";

        /// <summary>
        /// BGM再生イベント
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
            // ボタンイベント登録
            _leftButton.onClick.AddListener(OnLeftButtonPushed);
            _rightButton.onClick.AddListener(OnRightButtonPushed);
        }

        private void OnDisable()
        {
            // ボタンイベント解除
            _leftButton.onClick.RemoveListener(OnLeftButtonPushed);
            _rightButton.onClick.RemoveListener(OnRightButtonPushed);
        }

        // トラック番号の表示を更新
        public void UpdateTrackText()
        {
            // トラック番号は実値 + 1（ゼロ表記を防ぐため）
            _trackDisplayText.text = string.Format(_trackFormat, _soundSettings.BGMID.Value + 1);
        }

        // トラック番号切り替え
        void ChangeTrackID(int value)
        {
            var bgmid = _soundSettings.BGMID.Value;
            bgmid += value;

            // ID番号を0 ～ 最大値の範囲内に抑える
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
        /// 左ボタン操作時
        /// </summary>
        void OnLeftButtonPushed()
        {
            ChangeTrackID(-1);
            UpdateTrackText();
            _onPlayBGMEvent.Raise();
        }

        /// <summary>
        /// 右ボタン操作時
        /// </summary>
        void OnRightButtonPushed()
        {
            ChangeTrackID(1);
            UpdateTrackText();
            _onPlayBGMEvent.Raise();
        }
    }
}