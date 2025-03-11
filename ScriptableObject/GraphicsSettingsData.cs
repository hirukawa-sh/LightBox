using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;
using BayatGames.SaveGameFree.Types;

namespace Game.Settings
{
    /// <summary>
    /// �J�����ݒ�̍\���́i�Z�[�u/���[�h���ɗ��p�j
    /// </summary>
    public struct GraphicsSettings
    {
        public readonly bool EnableBloom;
        public readonly Color BloomColor;
        public readonly Color BackgroundColor;
        //public readonly ColorSave BloomColor;
        public readonly bool EnableDepthOfField;
        public readonly float FocusDistance;
        public readonly float Aperture;
        public readonly float FocalLength;

        public GraphicsSettings(GraphicsSettingsData graphicsSettingsData)
        {
            EnableBloom = graphicsSettingsData.EnableBloom.Value;
            BloomColor = graphicsSettingsData.BloomColor.Value;
            BackgroundColor = graphicsSettingsData.BackgroundColor.Value;
            EnableDepthOfField = graphicsSettingsData.EnableDepthOfField.Value;
            FocusDistance = graphicsSettingsData.FocusDistance.Value;
            Aperture = graphicsSettingsData.Aperture.Value;
            FocalLength = graphicsSettingsData.FocalLength.Value;
        }
    }

    /// <summary>
    /// �O���t�B�b�N�ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "GraphicSettingsData", menuName = "GameData/Settings/GraphicSettingsData", order = 1)]
    public class GraphicsSettingsData : ScriptableObject, ISaveDataAccess<GraphicsSettings>
    {
        /// <summary>
        /// �u���[���L��On/Off
        /// </summary>
        public ScriptableBooleanValue EnableBloom;

        /// <summary>
        /// �u���[���F
        /// </summary>
        public ScriptableColorValue BloomColor;

        /// <summary>
        /// �w�i�F
        /// </summary>
        public ScriptableColorValue BackgroundColor;

        /// <summary>
        /// DepthOfField�L��On/Off
        /// </summary>
        public ScriptableBooleanValue EnableDepthOfField;

        /// <summary>
        /// �ڂ�������
        /// </summary>
        public ScriptableFloatValue FocusDistance;

        /// <summary>
        /// �ڂ�������
        /// </summary>
        public ScriptableFloatValue Aperture;

        /// <summary>
        /// �ڂ����͈�
        /// </summary>
        public ScriptableFloatValue FocalLength;

        public void Set(GraphicsSettings data)
        {
            EnableBloom.Value = data.EnableBloom;
            BloomColor.Value = data.BloomColor;
            BackgroundColor.Value = data.BackgroundColor;
            EnableDepthOfField.Value = data.EnableDepthOfField;
            FocusDistance.Value = data.FocusDistance;
            Aperture.Value = data.Aperture;
            FocalLength.Value = data.FocalLength;
        }

        public void Default()
        {
            EnableBloom.Reset();
            BloomColor.Reset();
            BackgroundColor.Reset();
            EnableDepthOfField.Reset();
            FocusDistance.Reset();
            Aperture.Reset();
            FocalLength.Reset();
        }
    }
}
