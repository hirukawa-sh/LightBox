using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "StageListData", menuName = "GameData/StageListData", order = 1)]
    public class StageListData : ScriptableObject
    {
        /// <summary>
        /// ステージリスト
        /// </summary>
        public List<StageController> ListData
        {
            get
            {
                return _stagePrefabList;
            }
        }
        [SerializeField]
        List<StageController> _stagePrefabList = new List<StageController>();
    }
}