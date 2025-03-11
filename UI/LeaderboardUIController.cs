using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// LeaderboardのUI制御
    /// </summary>
    public class LeaderboardUIController : MonoBehaviour
    {
        /// <summary>
        /// Leaderboard の ScriptableObject
        /// </summary>
        [SerializeField]
        Game.Settings.LeaderBoardData _leaderboardData;
        
        /// <summary>
        /// LeaderboardContentUI
        /// </summary>
        [SerializeField]
        RectTransform _contents;

        /// <summary>
        /// NoDataUI
        /// </summary>
        [SerializeField]
        RectTransform _noData;

        /// <summary>
        /// インスタンス化するRowUI
        /// </summary>
        [SerializeField]
        LeaderboardRowUIController _leaderboardRowUIPrefabs;

        /// <summary>
        /// ステージ番号フォーマット
        /// </summary>
        [SerializeField]
        string _stageNumberFormat = "{0:000}";

        /// <summary>
        /// クリアタイムの表示フォーマット
        /// </summary>
        [SerializeField]
        string _clearTimeFormat = "{0:mm\\:ss\\.fff}";

        /// <summary>
        /// タッチ回数フォーマット
        /// </summary>
        [SerializeField]
        string _touchCountFormat = "{0:000}";

        /// <summary>
        /// ヒント回数フォーマット
        /// </summary>
        [SerializeField]
        string _hintCountFormat = "{0:000}";

        // Start is called before the first frame update
        void Start()
        {
            // データが存在するならリストを作成
            if (0 < _leaderboardData.Count)
            {
                // No Data 表示を消す
                _noData.gameObject.SetActive(false);

                // リスト表示
                CreateRows();
            }

            // 無いなら No Data を表示
            else
            {
                _noData.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// データリストを作成
        /// </summary>
        void CreateRows()
        {
            for (int i = 0; i < _leaderboardData.Count; i++)
            {
                var row = Instantiate(_leaderboardRowUIPrefabs, _contents);

                // ヒント使用回数がゼロならノーヒントアイコンを表示
                if (_leaderboardData.ScoreDatas[i].HintCount == 0)
                {
                    row.NoHintClearIcon.enabled = true;
                }
                else
                {
                    row.NoHintClearIcon.enabled = false;
                }
                row.StageNumber = string.Format(_stageNumberFormat, i + 1);
                row.ClearTime = string.Format(_clearTimeFormat, System.TimeSpan.FromMilliseconds(_leaderboardData.ScoreDatas[i].ClearTime));
                row.TouchCount = string.Format(_touchCountFormat, _leaderboardData.ScoreDatas[i].TouchCount);
                row.HintCount = string.Format(_hintCountFormat, _leaderboardData.ScoreDatas[i].HintCount);
            }
        }
    }
}
