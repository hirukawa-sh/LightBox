using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;

namespace Game.Settings
{
    /// <summary>
    /// 言語設定の構造体（セーブ/ロード時に利用）
    /// </summary>
    public struct LanguageSettings
    {
        public readonly int LocationIndex;

        public LanguageSettings(LanguageSettingsData languageSettingsData)
        {
            LocationIndex = languageSettingsData.LocationIndex.Value;
        }
    }
    /// <summary>
    /// 言語設定
    /// </summary>
    [CreateAssetMenu(fileName = "LanguageSettingsData", menuName = "GameData/Settings/LanguageSettingsData", order = 1)]
    public class LanguageSettingsData : ScriptableObject, ISaveDataAccess<LanguageSettings>
    {
        /// <summary>
        /// Localization.Locale の Index
        /// </summary>
        public ScriptableIntValue LocationIndex;

        public void Default()
        {
            LocationIndex.Reset();
        }

        public void Set(LanguageSettings data)
        {
            LocationIndex.Value = data.LocationIndex;
        }
    }
}
