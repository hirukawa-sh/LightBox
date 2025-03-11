using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace Game.AR
{
    /// <summary>
    /// AR�@�\�̐���
    /// ARSession�̃`�F�b�N �� �J���������̃`�F�b�N �� �Q�[���J�n
    /// </summary>
    public class ARController : MonoBehaviour
    {
        [Tooltip("AR�@�\�̃`�F�b�N")]
        [SerializeField]
        UnityEvent _onCheckARSession;

        [Tooltip("�J���������̃`�F�b�N")]
        [SerializeField]
        UnityEvent _onCheckARCameraPermission;

        [Tooltip("����������")]
        [SerializeField]
        UnityEvent _onInitializeCompleted;

        // Start is called before the first frame update
        void Start()
        {
            _onCheckARSession.Invoke();
        }

        /// <summary>
        /// AR Session�̏����������������ɌĂ΂��
        /// </summary>
        public void OnARSessionReady()
        {
            _onCheckARCameraPermission.Invoke();
        }

        /// <summary>
        /// �J���������̏����������������ɌĂ΂��
        /// </summary>
        public void OnCameraPermisionReady()
        {
            _onInitializeCompleted.Invoke();
        }
    }
}