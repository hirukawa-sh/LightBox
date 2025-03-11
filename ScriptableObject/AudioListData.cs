using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// AudioClip�̃��X�g
    /// </summary>
    [CreateAssetMenu(fileName = "AudioListData", menuName = "GameData/AudioListData", order = 1)]
    public class AudioListData : ScriptableObject
    {
        /// <summary>
        /// AudioClip���X�g
        /// </summary>
        public List<AudioClip> ListData
        {
            get
            {
                return _AudioList;
            }
        }
        [SerializeField]
        List<AudioClip> _AudioList;
    }
}