using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Events;

namespace UnityEngine.Localization.Components
{
    /// <summary>
    /// FontデータをLocalizeするカスタムイベント
    /// </summary>
    [AddComponentMenu("Localization/Asset/Localize Font Event")]
    public class LocalizeFontEvent : LocalizedAssetEvent<Font, LocalizedFont, UnityEventFont>
    {
        // 中身は空
    }
}
