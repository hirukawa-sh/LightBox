using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;

namespace Game.Settings
{
    /// <summary>
    /// ����ݒ�̍\���́i�Z�[�u/���[�h���ɗ��p�j
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
    /// ����ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = "LanguageSettingsData", menuName = "GameData/Settings/LanguageSettingsData", order = 1)]
    public class LanguageSettingsData : ScriptableObject, ISaveDataAccess<LanguageSettings>
    {
        /// <summary>
        /// Localization.Locale �� Index
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
