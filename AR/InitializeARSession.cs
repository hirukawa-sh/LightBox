using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

namespace Game.AR
{
    /// <summary>
    /// AR Session�̏�����
    /// </summary>
    [RequireComponent(typeof(ARSession))]
    public class InitializeARSession : MonoBehaviour
    {
        [Tooltip("Start()���Ɏ����I�Ƀ`�F�b�N���J�n����")]
        [SerializeField]
        bool _autoCheckOnStart = false;

        [Tooltip("AR��������")]
        [SerializeField]
        UnityEvent _onARSessionReady;

        [Tooltip("AR�G���[")]
        [SerializeField]
        UnityEvent _onARSessionError;

        [Tooltip("AR��Ή�")]
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
        /// �Ԃ�l���K�v�Ȃ��ꍇ
        /// </summary>
        public void CheckARSupport()
        {
            StartCoroutine(CheckARSupportCoroutin());
        }

        /// <summary>
        /// AR�T�|�[�g�̃`�F�b�N
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

            // �Ή��f�o�C�X�̏ꍇ
            if (ARSession.state >= ARSessionState.Ready)
            {
                Debug.Log($"{this} : AR Session is Ready.");
                _session.enabled = true;
                _onARSessionReady.Invoke();
            }

            // �T�|�[�g�ΏۊO����
            else if (ARSession.state == ARSessionState.Unsupported)
            {
                Debug.LogWarning($"{this} : AR Session is UnSupported.");
                _onARUnsupported.Invoke();
            }

            // �G���[
            else if (ARSession.state == ARSessionState.None)
            {
                Debug.LogError($"{this} : AR Session Initialized Error.");
                _onARSessionError.Invoke();
            }
        }
    }
}