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
    /// �J���������̃`�F�b�N
    /// </summary>
    [RequireComponent(typeof(ARCameraManager))]
    public class ARCameraPermissionAuthorization : MonoBehaviour
    {
        [Tooltip("Start()���Ɏ����I�Ƀ��N�G�X�g���s��")]
        [SerializeField]
        bool _autoRequestOnStart = false;

        [Tooltip("�J���������m�F�J�n")]
        [SerializeField]
        UnityEvent _onCameraPermissionAuthrozationStarted;

        [Tooltip("�J���������m�F����")]
        [SerializeField]
        UnityEvent _onCameraPermissionAuthrozationFinished;

        [Tooltip("�J���������̏������ł��Ă���")]
        [SerializeField]
        UnityEvent _onCameraPermissionReady;

        [Tooltip("�J�����������^�����Ă��Ȃ�")]
        [SerializeField]
        UnityEvent _onCameraPermissionError;

        ARCameraManager _ARCameraManager;

        // �������N�G�X�g��̑ҋ@����
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
        /// �Ԃ�l���K�v�Ȃ��ꍇ
        /// </summary>
        public void RequestPermission()
        {
            StartCoroutine(RequestPermissionCoroutine());
        }

        /// <summary>
        /// �����̃`�F�b�N�y�у��N�G�X�g���s
        /// </summary>
        /// <returns></returns>
        public IEnumerator RequestPermissionCoroutine()
        {
            _ARCameraManager.enabled = false;

            _onCameraPermissionAuthrozationStarted.Invoke();

#if UNITY_IOS
            // �������N�G�X�g
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            Debug.Log($"{this} : Camera Permission Requested.");

            // �����`�F�b�N
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
            // �������N�G�X�g
            Permission.RequestUserPermission(Permission.Camera);
            Debug.Log($"{this} : Camera Permission Requested.");

            // ��莞�ԑҋ@
            yield return new WaitForSeconds(WAIT_TIME);

            // �����`�F�b�N
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