using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using ScriptableValue;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// ポストプロセス制御
    /// </summary>
    public class PostProcessController : MonoBehaviour
    {
        /// <summary>
        /// PostProcess本体
        /// </summary>
        [SerializeField]
        PostProcessVolume _volume;

        /// <summary>
        /// グラフィック設定
        /// </summary>
        [SerializeField]
        GraphicsSettingsData _graphicsSettings;

        /// <summary>
        /// PostProcessのプロファイルデータ
        /// </summary>
        PostProcessProfile _profile;

        /// <summary>
        /// ブルームの設定
        /// </summary>
        Bloom _bloom;

        /// <summary>
        /// DepthOfFieldの設定
        /// </summary>
        DepthOfField _depth;

        void Start()
        {
            _profile = _volume.profile;

            // Bloomの設定と同期
            _bloom = _profile.GetSetting<Bloom>();

            _graphicsSettings.EnableBloom.OnUpdateValue.AddListener(isOn =>
                _bloom.enabled.Override(isOn)
            );

            _graphicsSettings.BloomColor.OnUpdateValue.AddListener(color =>
                _bloom.color.Override(color)
            );

            // 初期値の設定
            _bloom.enabled.Override(_graphicsSettings.EnableBloom.Value);
            _bloom.color.Override(_graphicsSettings.BloomColor.Value);

            // DepthOfFieldの設定と同期
            _depth = _profile.GetSetting<DepthOfField>();

            _graphicsSettings.EnableDepthOfField.OnUpdateValue.AddListener(isOn =>
                _depth.enabled.Override(isOn)
            );

            _graphicsSettings.FocusDistance.OnUpdateValue.AddListener(value =>
                _depth.focusDistance.Override(value)
            );

            _graphicsSettings.Aperture.OnUpdateValue.AddListener(value =>
                _depth.aperture.Override(value)
            );

            _graphicsSettings.FocalLength.OnUpdateValue.AddListener(value =>
                _depth.focalLength.Override(value)
            );

            // 初期値の設定
            _depth.enabled.Override(_graphicsSettings.EnableDepthOfField.Value);
            _depth.focusDistance.Override(_graphicsSettings.FocusDistance.Value);
            _depth.aperture.Override(_graphicsSettings.Aperture.Value);
            _depth.focalLength.Override(_graphicsSettings.FocalLength.Value);
        }
    }
}
