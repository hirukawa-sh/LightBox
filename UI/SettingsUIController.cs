using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Settings;

namespace Game.UI
{
    /// <summary>
    /// 設定UI
    /// UI←→パラメータ の橋渡しを行う
    /// </summary>
    public class SettingsUIController : MonoBehaviour
    {
        // 設定パラメータ
        [Header("Values")]

        [SerializeField]
        SoundSettingsData _soundSettinngs;

        [SerializeField]
        CameraSettingsData _cameraSettings;

        [SerializeField]
        GraphicsSettingsData _graphicsSettings;

        // 対応するUI
        [Header("UI")]

        [SerializeField]
        Slider _BGMVolumeSlider;

        [SerializeField]
        Slider _SEVolumeSlider;

        [SerializeField]
        BGMTrackControl _bgmTrackControl;

        [SerializeField]
        Toggle _inverseCameraXToggle;

        [SerializeField]
        Toggle _inverseCameraYToggle;

        [SerializeField]
        Toggle _enableBloomToggle;

        [SerializeField]
        ColorPicker _bloomColorPicker;

        [SerializeField]
        GrayscalePicker _backgroundColorPicker;

        [SerializeField]
        Toggle _enableDepthOfFieldToggle;

        [SerializeField]
        Slider _focusDistanceSlider;

        [SerializeField]
        Slider _apertureSlider;

        [SerializeField]
        Slider _foculLengthSlider;

        // Start is called before the first frame update
        void Start()
        {
            // 各種UIとパラメータの同期

            // BGM音量
            _BGMVolumeSlider.onValueChanged.AddListener(value =>
                _soundSettinngs.BGMVolume.Value = value
            );

            // SE音量
            _SEVolumeSlider.onValueChanged.AddListener(value =>
                _soundSettinngs.SEVolume.Value = value
            );

            // トラックコントロール
            _bgmTrackControl.UpdateTrackText();

            // カメラ反転X
            _inverseCameraXToggle.onValueChanged.AddListener(isOn =>
                _cameraSettings.InverseCameraX.Value = isOn
            );

            // カメラ反転Y
            _inverseCameraYToggle.onValueChanged.AddListener(isOn =>
                _cameraSettings.InverseCameraY.Value = isOn
            );

            // ブルームOn/Off
            _enableBloomToggle.onValueChanged.AddListener(isOn =>
                _graphicsSettings.EnableBloom.Value = isOn
            );

            // ブルーム色
            _bloomColorPicker.onValueChanged.AddListener(color =>
                _graphicsSettings.BloomColor.Value = color
            );

            // 背景色
            _backgroundColorPicker.onValueChanged.AddListener(color =>
                _graphicsSettings.BackgroundColor.Value = color
            );

            // Depth Of View On/Off
            _enableDepthOfFieldToggle.onValueChanged.AddListener(isOn =>
                _graphicsSettings.EnableDepthOfField.Value = isOn
            );

            // フォーカス距離
            _focusDistanceSlider.onValueChanged.AddListener(value =>
                _graphicsSettings.FocusDistance.Value = value
            );

            // ぼかしの強さ
            _apertureSlider.onValueChanged.AddListener(value =>
                _graphicsSettings.Aperture.Value = value
            );

            // ぼかし範囲
            _foculLengthSlider.onValueChanged.AddListener(value =>
                _graphicsSettings.FocalLength.Value = value
            );

            UpdateUI();
        }

        /// <summary>
        /// UIを実値に更新
        /// </summary>
        void UpdateUI()
        {
            _BGMVolumeSlider.value = _soundSettinngs.BGMVolume.Value;
            _SEVolumeSlider.value = _soundSettinngs.SEVolume.Value;
            _inverseCameraXToggle.isOn = _cameraSettings.InverseCameraX.Value;
            _inverseCameraYToggle.isOn = _cameraSettings.InverseCameraY.Value;
            _enableBloomToggle.isOn = _graphicsSettings.EnableBloom.Value;
            _bloomColorPicker.Value = _graphicsSettings.BloomColor.Value;
            _backgroundColorPicker.Value = _graphicsSettings.BackgroundColor.Value;
            _enableDepthOfFieldToggle.isOn = _graphicsSettings.EnableDepthOfField.Value;
            _focusDistanceSlider.value = _graphicsSettings.FocusDistance.Value;
            _apertureSlider.value = _graphicsSettings.Aperture.Value;
            _foculLengthSlider.value = _graphicsSettings.FocalLength.Value;
        }

        /// <summary>
        /// Defaultボタン押下
        /// </summary>
        public void OnDefaultButtonPushed()
        {
            _soundSettinngs.Default();
            _cameraSettings.Default();
            _graphicsSettings.Default();

            UpdateUI();
        }
    }
}
