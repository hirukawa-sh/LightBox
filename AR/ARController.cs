using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace Game.AR
{
    /// <summary>
    /// AR機能の制御
    /// ARSessionのチェック → カメラ権限のチェック → ゲーム開始
    /// </summary>
    public class ARController : MonoBehaviour
    {
        [Tooltip("AR機能のチェック")]
        [SerializeField]
        UnityEvent _onCheckARSession;

        [Tooltip("カメラ権限のチェック")]
        [SerializeField]
        UnityEvent _onCheckARCameraPermission;

        [Tooltip("初期化完了")]
        [SerializeField]
        UnityEvent _onInitializeCompleted;

        // Start is called before the first frame update
        void Start()
        {
            _onCheckARSession.Invoke();
        }

        /// <summary>
        /// AR Sessionの準備が完了した時に呼ばれる
        /// </summary>
        public void OnARSessionReady()
        {
            _onCheckARCameraPermission.Invoke();
        }

        /// <summary>
        /// カメラ権限の準備が完了した時に呼ばれる
        /// </summary>
        public void OnCameraPermisionReady()
        {
            _onInitializeCompleted.Invoke();
        }
    }
}