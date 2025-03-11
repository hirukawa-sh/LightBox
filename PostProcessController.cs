using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using ScriptableValue;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// �|�X�g�v���Z�X����
    /// </summary>
    public class PostProcessController : MonoBehaviour
    {
        /// <summary>
        /// PostProcess�{��
        /// </summary>
        [SerializeField]
        PostProcessVolume _volume;

        /// <summary>
        /// �O���t�B�b�N�ݒ�
        /// </summary>
        [SerializeField]
        GraphicsSettingsData _graphicsSettings;

        /// <summary>
        /// PostProcess�̃v���t�@�C���f�[�^
        /// </summary>
        PostProcessProfile _profile;

        /// <summary>
        /// �u���[���̐ݒ�
        /// </summary>
        Bloom _bloom;

        /// <summary>
        /// DepthOfField�̐ݒ�
        /// </summary>
        DepthOfField _depth;

        void Start()
        {
            _profile = _volume.profile;

            // Bloom�̐ݒ�Ɠ���
            _bloom = _profile.GetSetting<Bloom>();

            _graphicsSettings.EnableBloom.OnUpdateValue.AddListener(isOn =>
                _bloom.enabled.Override(isOn)
            );

            _graphicsSettings.BloomColor.OnUpdateValue.AddListener(color =>
                _bloom.color.Override(color)
            );

            // �����l�̐ݒ�
            _bloom.enabled.Override(_graphicsSettings.EnableBloom.Value);
            _bloom.color.Override(_graphicsSettings.BloomColor.Value);

            // DepthOfField�̐ݒ�Ɠ���
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

            // �����l�̐ݒ�
            _depth.enabled.Override(_graphicsSettings.EnableDepthOfField.Value);
            _depth.focusDistance.Override(_graphicsSettings.FocusDistance.Value);
            _depth.aperture.Override(_graphicsSettings.Aperture.Value);
            _depth.focalLength.Override(_graphicsSettings.FocalLength.Value);
        }
    }
}
