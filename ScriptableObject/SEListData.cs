using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// SEリスト
    /// </summary>
    [CreateAssetMenu(fileName = "SEListData", menuName = "GameData/SEListData", order = 1)]
    public class SEListData : ScriptableObject
    {
        /// <summary>
        /// 決定ボタン押下時SE
        /// </summary>
        public AudioClip DecideSEClip;

        /// <summary>
        /// キャンセルボタン押下時SE
        /// </summary>
        public AudioClip CancelSEClip;
    }
}