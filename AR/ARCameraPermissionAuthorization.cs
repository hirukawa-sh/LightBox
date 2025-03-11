using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UniRx;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Game.AR
{
    /// <summary>
    /// カメラ権限のチェック
    /// </summary>
    [RequireComponent(typeof(ARCameraManager))]
    public class ARCameraPermissionAuthorization : MonoBehaviour
    {
        [Tooltip("Start()時に自動的にリクエストを行う")]
        [SerializeField]
        bool _autoRequestOnStart = false;

        [Tooltip("カメラ権限確認開始")]
        [SerializeField]
        UnityEvent _onCameraPermissionAuthrozationStarted;

        [Tooltip("カメラ権限確認完了")]
        [SerializeField]
        UnityEvent _onCameraPermissionAuthrozationFinished;

        [Tooltip("カメラ権限の準備ができている")]
        [SerializeField]
        UnityEvent _onCameraPermissionReady;

        [Tooltip("カメラ権限が与えられていない")]
        [SerializeField]
        UnityEvent _onCameraPermissionError;

        ARCameraManager _ARCameraManager;

        // 権限リクエスト後の待機時間
        const float WAIT_TIME = 0.5f;

        void Awake()
        {
            _ARCameraManager = GetComponent<ARCameraManager>();
        }

        void Start()
        {
            if (_autoRequestOnStart == true)
            {
                StartCoroutine(RequestPermissionCoroutine());
            }
        }

        /// <summary>
        /// 返り値が必要ない場合
        /// </summary>
        public void RequestPermission()
        {
            StartCoroutine(RequestPermissionCoroutine());
        }

        /// <summary>
        /// 権限のチェック及びリクエスト実行
        /// </summary>
        /// <returns></returns>
        public IEnumerator RequestPermissionCoroutine()
        {
            _ARCameraManager.enabled = false;

            _onCameraPermissionAuthrozationStarted.Invoke();

#if UNITY_IOS
            // 権限リクエスト
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            Debug.Log($"{this} : Camera Permission Requested.");

            // 権限チェック
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.Log($"{this} : Camera Permission is Authorized.");
                _onCameraPermissionReady.Invoke();
                _ARCameraManager.enabled = true;
            }
            else
            {
                Debug.Log($"{this} : Camera Permission is Not Authorized.");
                _onCameraPermissionError.Invoke();
            }
#elif UNITY_ANDROID
            // 権限リクエスト
            Permission.RequestUserPermission(Permission.Camera);
            Debug.Log($"{this} : Camera Permission Requested.");

            // 一定時間待機
            yield return new WaitForSeconds(WAIT_TIME);

            // 権限チェック
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Debug.Log($"{this} : Camera Permission is Authorized.");
                _onCameraPermissionReady.Invoke();
                _ARCameraManager.enabled = true;
            }
            else
            {
                Debug.LogWarning($"{this} : Camera Permission is Not Authorized.");
                _onCameraPermissionError.Invoke();
            }
#endif
            _onCameraPermissionAuthrozationFinished.Invoke();

            yield break;
        }
    }
}