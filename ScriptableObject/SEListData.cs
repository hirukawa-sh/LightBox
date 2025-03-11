using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// SE���X�g
    /// </summary>
    [CreateAssetMenu(fileName = "SEListData", menuName = "GameData/SEListData", order = 1)]
    public class SEListData : ScriptableObject
    {
        /// <summary>
        /// ����{�^��������SE
        /// </summary>
        public AudioClip DecideSEClip;

        /// <summary>
        /// �L�����Z���{�^��������SE
        /// </summary>
        public AudioClip CancelSEClip;
    }
}