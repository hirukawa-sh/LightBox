using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;

namespace Game.Settings
{
    /// <summary>
    /// サウンド設定の構造体
    /// </summary>
    public struct SoundSettings
    {
        public readonly float BGMVolume;
        public readonly float SEVolume;
        public readonly int BGMID;

        public SoundSettings(SoundSettingsData soundSettingsData)
        {
            BGMVolume = soundSettingsData.BGMVolume.Value;
            SEVolume = soundSettingsData.SEVolume.Value;
            BGMID = soundSettingsData.BGMID.Value;
        }
    }

    /// <summary>
    /// サウンド設定
    /// </summary>
    [CreateAssetMenu(fileName = "SoundSettingsData", menuName = "GameData/Settings/SoundSettingsData", order = 1)]
    public class SoundSettingsData : ScriptableObject, ISaveDataAccess<SoundSettings>
    {
        /// <summary>
        /// BGM音量
        /// </summary>
        public ScriptableFloatValue BGMVolume;

        /// <summary>
        /// SE音量
        /// </summary>
        public ScriptableFloatValue SEVolume;

        /// <summary>
        /// 再生中BGMのID
        /// </summary>
        public ScriptableIntValue BGMID;

        public void Set(SoundSettings data)
        {
            BGMVolume.Value = data.BGMVolume;
            SEVolume.Value = data.SEVolume;
            BGMID.Value = data.BGMID;
        }

        public void Default()
        {
            BGMVolume.Reset();
            SEVolume.Reset();
            BGMID.Reset();
        }
    }
}