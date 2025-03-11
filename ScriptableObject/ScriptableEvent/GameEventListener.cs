using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvent
{
    /// <summary>
    /// GameEventを受け取るコンポーネント
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        /// <summary>
        /// 受けたいイベント
        /// </summary>
        public GameEvent Event;

        /// <summary>
        /// 受け取ったイベントを対象に渡すUnityEvent
        /// </summary>
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        /// <summary>
        /// イベント発行時に呼ばれる
        /// </summary>
        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}