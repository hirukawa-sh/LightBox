using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// 特定タイミング時に自動的にイベントが発生
    /// </summary>
    public class UnityEventInvoker : MonoBehaviour
    {
        /// <summary>
        /// 発生タイミング
        /// </summary>
        public enum InvokeTiming
        {
            Awake,
            Start,
            Enable,
            Disable,
            Destroy,
        }
        [SerializeField]
        InvokeTiming _invokeTiming;

        /// <summary>
        /// 発生させるイベント
        /// </summary>
        [SerializeField]
        UnityEvent _event;

        void Awake()
        {
            if (_invokeTiming == InvokeTiming.Awake)
            {
                _event.Invoke();
            }
        }

        void Start()
        {
            if (_invokeTiming == InvokeTiming.Start)
            {
                _event.Invoke();
            }
        }

        void OnEnable()
        {
            if (_invokeTiming == InvokeTiming.Enable)
            {
                _event.Invoke();
            }
        }

        void OnDisable()
        {
            if (_invokeTiming == InvokeTiming.Disable)
            {
                _event.Invoke();
            }
        }

        void OnDestroy()
        {
            if (_invokeTiming == InvokeTiming.Destroy)
            {
                _event.Invoke();
            }
        }
    }
}