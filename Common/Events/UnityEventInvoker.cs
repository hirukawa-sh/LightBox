using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// ����^�C�~���O���Ɏ����I�ɃC�x���g������
    /// </summary>
    public class UnityEventInvoker : MonoBehaviour
    {
        /// <summary>
        /// �����^�C�~���O
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
        /// ����������C�x���g
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