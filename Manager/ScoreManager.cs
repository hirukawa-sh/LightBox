using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ScriptableValue;
using Common;
using UnityEngine.Events;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// スコア管理
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>
        /// LeaderBoard
        /// </summary>
        [SerializeField]
        LeaderBoardData _leaderBoardData;

        /// <summary>
        /// 現在のステージ番号
        /// </summary>
        [SerializeField]
        ScriptableIntValue _currentStageNumberValue;

        /// <summary>
        /// タイマー
        /// </summary>
        [SerializeField]
        ScriptableFloatValue _timerValue;

        /// <summary>
        /// タッチ回数
        /// </summary>
        [SerializeField]
        ScriptableIntValue _touchCountValue;

        /// <summary>
        /// ヒント回数
        /// </summary>
        [SerializeField]
        ScriptableIntValue _hintCountValue;

        /// <summary>
        /// スコア更新が発生した場合に呼ばれるイベント
        /// </summary>
        [SerializeField]
        UnityEvent _onNewRecordEvent;

        /// <summary>
        /// ストップウォッチ
        /// </summary>
        UnityStopwatch _stopWatch = new UnityStopwatch();

        void Start()
        {
            // 値の初期化
            _touchCountValue.Reset();
            _hintCountValue.Reset();
            _timerValue.Reset();
        }

        /// <summary>
        /// タイマー計測開始
        /// </summary>
        public void StartTimer()
        {
            _stopWatch.Start();
        }

        /// <summary>
        /// タイマー計測停止
        /// </summary>
        public void StopTimer()
        {
            _stopWatch.Stop();

            // 計測結果を ScriptableValue に書き込む
            _timerValue.Value = _stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// タッチ回数計測
        /// </summary>
        public void TouchCountUp()
        {
            _touchCountValue.Value++;
        }

        /// <summary>
        /// ヒント回数計測
        /// </summary>
        public void HintCountUp()
        {
            _hintCountValue.Value++;
        }

        /// <summary>
        /// 記録をLeaderBoardに書き込み
        /// </summary>
        public void UpdateLeaderboard()
        {
            var result =_leaderBoardData.UpdateHighScore(_currentStageNumberValue.Value,
                            _timerValue.Value,
                            _touchCountValue.Value,
                            _hintCountValue.Value
                            );

            // スコア更新が発生したらイベント発生
            if (result == true)
            {
                _onNewRecordEvent.Invoke();
            }
        }
    }
}