using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Leaderboardの各行データUI
    /// </summary>
    public class LeaderboardRowUIController : MonoBehaviour
    {
        // 各Textコンポーネント
        [SerializeField]
        Image _noHintClearIcon;

        [SerializeField]
        Text _stageNumberText;

        [SerializeField]
        Text _clearTimeText;

        [SerializeField]
        Text _touchCountText;

        [SerializeField]
        Text _hintCountText;

        /// <summary>
        /// ノーヒントクリアアイコン
        /// </summary>
        public Image NoHintClearIcon
        {
            get
            {
                return _noHintClearIcon;
            }
            set
            {
                _noHintClearIcon = value;
            }
        }

        /// <summary>
        /// ステージ番号
        /// </summary>
        public string StageNumber
        {
            get
            {
                return _stageNumberText.text;
            }
            set
            {
                _stageNumberText.text = value;
            }
        }

        /// <summary>
        /// クリア時間
        /// </summary>
        public string ClearTime
        {
            get
            {
                return _clearTimeText.text;
            }
            set
            {
                _clearTimeText.text = value;
            }
        }

        /// <summary>
        /// タッチ回数
        /// </summary>
        public string TouchCount
        {
            get
            {
                return _touchCountText.text;
            }
            set
            {
                _touchCountText.text = value;
            }
        }

        /// <summary>
        /// ヒント回数
        /// </summary>
        public string HintCount
        {
            get
            {
                return _hintCountText.text;
            }
            set
            {
                _hintCountText.text = value;
            }
        }
    }
}
