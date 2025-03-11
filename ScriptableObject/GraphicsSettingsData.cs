using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;
using BayatGames.SaveGameFree.Types;

namespace Game.Settings
{
    /// <summary>
    /// カメラ設定の構造体（セーブ/ロード時に利用）
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
    /// グラフィック設定
    /// </summary>
    [CreateAssetMenu(fileName = "GraphicSettingsData", menuName = "GameData/Settings/GraphicSettingsData", order = 1)]
    public class GraphicsSettingsData : ScriptableObject, ISaveDataAccess<GraphicsSettings>
    {
        /// <summary>
        /// ブルーム有効On/Off
        /// </summary>
        public ScriptableBooleanValue EnableBloom;

        /// <summary>
        /// ブルーム色
        /// </summary>
        public ScriptableColorValue BloomColor;

        /// <summary>
        /// 背景色
        /// </summary>
        public ScriptableColorValue BackgroundColor;

        /// <summary>
        /// DepthOfField有効On/Off
        /// </summary>
        public ScriptableBooleanValue EnableDepthOfField;

        /// <summary>
        /// ぼかし距離
        /// </summary>
        public ScriptableFloatValue FocusDistance;

        /// <summary>
        /// ぼかし強さ
        /// </summary>
        public ScriptableFloatValue Aperture;

        /// <summary>
        /// ぼかし範囲
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
