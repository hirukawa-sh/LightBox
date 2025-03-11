using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Settings;

namespace Game.UI
{
    /// <summary>
    /// �ݒ�UI
    /// UI�����p�����[�^ �̋��n�����s��
    /// </summary>
    public class SettingsUIController : MonoBehaviour
    {
        // �ݒ�p�����[�^
        [Header("Values")]

        [SerializeField]
        SoundSettingsData _soundSettinngs;

        [SerializeField]
        CameraSettingsData _cameraSettings;

        [SerializeField]
        GraphicsSettingsData _graphicsSettings;

        // �Ή�����UI
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
            // �e��UI�ƃp�����[�^�̓���

            // BGM����
            _BGMVolumeSlider.onValueChanged.AddListener(value =>
                _soundSettinngs.BGMVolume.Value = value
            );

            // SE����
            _SEVolumeSlider.onValueChanged.AddListener(value =>
                _soundSettinngs.SEVolume.Value = value
            );

            // �g���b�N�R���g���[��
            _bgmTrackControl.UpdateTrackText();

            // �J�������]X
            _inverseCameraXToggle.onValueChanged.AddListener(isOn =>
                _cameraSettings.InverseCameraX.Value = isOn
            );

            // �J�������]Y
            _inverseCameraYToggle.onValueChanged.AddListener(isOn =>
                _cameraSettings.InverseCameraY.Value = isOn
            );

            // �u���[��On/Off
            _enableBloomToggle.onValueChanged.AddListener(isOn =>
                _graphicsSettings.EnableBloom.Value = isOn
            );

            // �u���[���F
            _bloomColorPicker.onValueChanged.AddListener(color =>
                _graphicsSettings.BloomColor.Value = color
            );

            // �w�i�F
            _backgroundColorPicker.onValueChanged.AddListener(color =>
                _graphicsSettings.BackgroundColor.Value = color
            );

            // Depth Of View On/Off
            _enableDepthOfFieldToggle.onValueChanged.AddListener(isOn =>
                _graphicsSettings.EnableDepthOfField.Value = isOn
            );

            // �t�H�[�J�X����
            _focusDistanceSlider.onValueChanged.AddListener(value =>
                _graphicsSettings.FocusDistance.Value = value
            );

            // �ڂ����̋���
            _apertureSlider.onValueChanged.AddListener(value =>
                _graphicsSettings.Aperture.Value = value
            );

            // �ڂ����͈�
            _foculLengthSlider.onValueChanged.AddListener(value =>
                _graphicsSettings.FocalLength.Value = value
            );

            UpdateUI();
        }

        /// <summary>
        /// UI�����l�ɍX�V
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
        /// Default�{�^������
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
