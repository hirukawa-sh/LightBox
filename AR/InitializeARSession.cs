using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

namespace Game.AR
{
    /// <summary>
    /// AR Sessionの初期化
    /// </summary>
    [RequireComponent(typeof(ARSession))]
    public class InitializeARSession : MonoBehaviour
    {
        [Tooltip("Start()時に自動的にチェックを開始する")]
        [SerializeField]
        bool _autoCheckOnStart = false;

        [Tooltip("AR準備完了")]
        [SerializeField]
        UnityEvent _onARSessionReady;

        [Tooltip("ARエラー")]
        [SerializeField]
        UnityEvent _onARSessionError;

        [Tooltip("AR非対応")]
        [SerializeField]
        UnityEvent _onARUnsupported;
        
        ARSession _session;

        void Awake()
        {
            _session = GetComponent<ARSession>();
        }

        void Start()
        {
            if (_autoCheckOnStart == true)
            {
                StartCoroutine(CheckARSupportCoroutin());
            }
        }

        /// <summary>
        /// 返り値が必要ない場合
        /// </summary>
        public void CheckARSupport()
        {
            StartCoroutine(CheckARSupportCoroutin());
        }

        /// <summary>
        /// ARサポートのチェック
        /// </summary>
        /// <returns></returns>
        public IEnumerator CheckARSupportCoroutin()
        {
            _session.enabled = false;

            if ((ARSession.state == ARSessionState.None) || (ARSession.state == ARSessionState.CheckingAvailability))
            {
                Debug.Log($"{this} : AR Session CheckAvailability()...");
                yield return ARSession.CheckAvailability();
            }

            // 対応デバイスの場合
            if (ARSession.state >= ARSessionState.Ready)
            {
                Debug.Log($"{this} : AR Session is Ready.");
                _session.enabled = true;
                _onARSessionReady.Invoke();
            }

            // サポート対象外処理
            else if (ARSession.state == ARSessionState.Unsupported)
            {
                Debug.LogWarning($"{this} : AR Session is UnSupported.");
                _onARUnsupported.Invoke();
            }

            // エラー
            else if (ARSession.state == ARSessionState.None)
            {
                Debug.LogError($"{this} : AR Session Initialized Error.");
                _onARSessionError.Invoke();
            }
        }
    }
}