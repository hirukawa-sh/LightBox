using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Unityクラス内で使用可能なストップウォッチ
    /// モバイル環境では、System.Diagnostics.Stopwatch が使用できないので
    /// 代わりに使用する
    /// </summary>
    public class UnityStopwatch
    {
        // 計測開始時間記録用
        float _startValue = 0.0f;

        // 計測停止時間記録用
        float _stopValue = 0.0f;

        // 合計時間
        float _totalTime = 0.0f;

        /// <summary>
        /// 値を取得（ミリ秒、long型）
        /// </summary>
        public long ElapsedMilliseconds
        {
            get
            {
                // ミリ秒に直してからlong型に変換
                return (long)((_totalTime) * 1000);
            }
        }

        /// <summary>
        /// 計測開始
        /// </summary>
        public void Start()
        {
            _startValue = Time.time;
        }

        /// <summary>
        /// 計測停止
        /// </summary>
        public void Stop()
        {
            _stopValue = Time.time;

            // 経過時間を求める
            _totalTime += _stopValue - _startValue;
        }

        /// <summary>
        /// 値のクリア
        /// </summary>
        public void Reset()
        {
            // 値をゼロにする
            _startValue = _stopValue = _totalTime = 0.0f;
        }
    }
}