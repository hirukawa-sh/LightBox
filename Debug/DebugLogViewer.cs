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
    /// Debug.log ��\������
    /// </summary>
    public class DebugLogViewer : MonoBehaviour
    {
        /// <summary>
        /// ���O�ő�s��
        /// </summary>
        [SerializeField]
        int maxLineCount = 10;

        /// <summary>
        /// ���O���C�x���g�ő��M
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
            // ���O�̐��`
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

            // �L���[�Ƀ��O���i�[
            _queue.Enqueue(message);

            // �ő�s���ɓ��B���Ă�����Â��s���폜
            if (maxLineCount < _queue.Count)
            {
                _queue.Dequeue();
            }

            // �L���[�̓��e���e�L�X�g�ɕϊ�
            var str = new StringBuilder();
            foreach (var log in _queue.ToArray())
            {
                str.AppendLine(log);
            }

            // ���O�𑗐M
            _onUpdateLog.Invoke(str.ToString());
        }
    }
}
#endif