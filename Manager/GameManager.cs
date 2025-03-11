using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    /// <summary>
    /// ゲーム管理クラス
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// ゲーム開始イベント
        /// </summary>
        [SerializeField]
        UnityEvent onGameStartEvent;

        /// <summary>
        /// ポーズイベント
        /// </summary>
        [SerializeField]
        UnityEvent onPauseEvent;

        /// <summary>
        /// タッチ無効化イベント
        /// </summary>
        [SerializeField]
        UnityEvent onDisableTouchEvent;

        /// <summary>
        /// タッチ有効化イベント
        /// </summary>
        [SerializeField]
        UnityEvent onEnableTouchEvent;

        /// <summary>
        /// ポーズ中フラグ
        /// </summary>
        bool _isPause = false;

        /// <summary>
        /// 外部ポーズ有効化フラグ
        /// </summary>
        bool _enableApplicaitonPause = true;

        // Start is called before the first frame update
        void Start()
        {
            // ゲーム開始イベント発行
            onGameStartEvent.Invoke();
        }

        /// <summary>
        /// アプリが外部から停止した場合
        /// </summary>
        /// <param name="pause">ポーズフラグ</param>
        void OnApplicationPause(bool pause)
        {
            // 外部から停止された
            if (pause == true)
            {
                // ポーズ中ではない、かつ外部ポーズ有効の場合
                if (!_isPause && _enableApplicaitonPause)
                {
                    // ポーズイベント発生
                    onPauseEvent.Invoke();
                }
            }
        }

        /// <summary>
        /// ゲーム一時停止
        /// </summary>
        public void GamePause()
        {
            // タッチ無効化
            onDisableTouchEvent.Invoke();

            // ポーズフラグオン
            _isPause = true;

            Debug.Log($"[{gameObject}] Game is Paused.");
        }

        /// <summary>
        /// ゲーム再開
        /// </summary>
        public void GameResume()
        {
            //タッチ有効化
            onEnableTouchEvent.Invoke();

            // ポーズフラグオフ
            _isPause = false;

            Debug.Log($"[{gameObject}] Game is Resumed.");
        }

        /// <summary>
        /// 外部ポーズ有効化
        /// </summary>
        public void EnableApplicationPause()
        {
            _enableApplicaitonPause = true;
        }

        /// <summary>
        /// 一時的に外部ポーズを無効にする
        /// </summary>
        public void DisableApplicationPause()
        {
            _enableApplicaitonPause = false;
        }
    }
}