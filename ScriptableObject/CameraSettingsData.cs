using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;

namespace Game.Settings
{
    /// <summary>
    /// カメラ設定の構造体（セーブ/ロード時に利用）
    /// </summary>
    public struct CameraSettings
    {
        public readonly bool InverseCameraX;
        public readonly bool InverseCameraY;

        public CameraSettings(CameraSettingsData cameraSettingsData)
        {
            InverseCameraX = cameraSettingsData.InverseCameraX.Value;
            InverseCameraY = cameraSettingsData.InverseCameraY.Value;
        }
    }

    /// <summary>
    /// カメラ設定
    /// </summary>
    [CreateAssetMenu(fileName = "CameraSettingsData", menuName = "GameData/Settings/CameraSettingsData", order = 1)]
    public class CameraSettingsData : ScriptableObject, ISaveDataAccess<CameraSettings>
    {
        /// <summary>
        /// カメラ反転X
        /// </summary>
        public ScriptableBooleanValue InverseCameraX;

        /// <summary>
        /// カメラ反転Y
        /// </summary>
        public ScriptableBooleanValue InverseCameraY;

        public void Set(CameraSettings data)
        {
            InverseCameraX.Value = data.InverseCameraX;
            InverseCameraY.Value = data.InverseCameraY;
        }

        public void Default()
        {
            InverseCameraX.Reset();
            InverseCameraY.Reset();
        }
    }
}