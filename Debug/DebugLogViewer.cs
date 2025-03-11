#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text;

namespace Game.Debugger
{
    /// <summary>
    /// Debug.log を表示する
    /// </summary>
    public class DebugLogViewer : MonoBehaviour
    {
        /// <summary>
        /// ログ最大行数
        /// </summary>
        [SerializeField]
        int maxLineCount = 10;

        /// <summary>
        /// ログをイベントで送信
        /// </summary>
        [SerializeField]
        UnityEvent<string> _onUpdateLog;

        Queue<string> _queue;

        void Awake()
        {
            _queue = new Queue<string>();
        }

        void Start()
        {
            Application.logMessageReceived += UpdateLog;
        }

        void OnDestroy()
        {
            Application.logMessageReceived -= UpdateLog;
        }

        void UpdateLog(string logstr, string stacktrace, LogType type)
        {
            // ログの成形
            var message = "";
            switch (type)
            {
                case LogType.Warning:
                    message = "<color=#ffff00>" + logstr;
                    break;

                case LogType.Error:
                    message = "<color=#ff0000>" + logstr;
                    break;

                case LogType.Exception:
                    message = "<color=#0000ff>" + logstr;
                    break;
                default:
                    message = logstr;
                    break;
            }

            // キューにログを格納
            _queue.Enqueue(message);

            // 最大行数に到達していたら古い行を削除
            if (maxLineCount < _queue.Count)
            {
                _queue.Dequeue();
            }

            // キューの内容をテキストに変換
            var str = new StringBuilder();
            foreach (var log in _queue.ToArray())
            {
                str.AppendLine(log);
            }

            // ログを送信
            _onUpdateLog.Invoke(str.ToString());
        }
    }
}
#endif