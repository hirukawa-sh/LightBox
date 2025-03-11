using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    /// <summary>
    /// UI�\���E��\������
    /// </summary>
    public class UIController : MonoBehaviour
    {
        /// <summary>
        /// ������ԂŉB�����H
        /// </summary>
        [SerializeField]
        bool _isHideStartup;

        /// <summary>
        /// Canvas
        /// </summary>
        [SerializeField]
        Canvas _canvas;

        /// <summary>
        /// UI�p�J����
        /// </summary>
        [SerializeField]
        Camera _uiCamera;

        /// <summary>
        /// UI�A�j���[�V����
        /// </summary>
        [SerializeField]
        UIAnimation _uiAnime;

        /// <summary>
        /// ���݂̏�ԁ@UI��\�����H
        /// </summary>
        bool _isHide = false;

        /// <summary>
        /// ���[�g�L�����o�X�̎Q��
        /// </summary>
        Canvas _rootCanvas;

        /// <summary>
        /// ���[�g�L�����o�X��GraphicRaycaster
        /// </summary>
        GraphicRaycaster _rootRaycaster;

        void Start()
        {
            // UI�p�J�����̎擾
            var _uicamera = GameObject.Find(_uiCamera.name)?.GetComponent<Camera>();

            // �V�[�����Ɍ�����Ȃ���ΐ���
            if (_uicamera == null)
            {
                _uicamera = Instantiate(_uiCamera);
            }

            // UI�L�����o�X�̎擾
            if (_canvas == null)
            {
                // �w�肪�����ꍇ�͎����ŒT��
                _canvas = GetComponentInParent<Canvas>();
            }

            // ���[�g�L�����o�X���擾
            _rootCanvas = _canvas.rootCanvas;
            _rootRaycaster = _rootCanvas.GetComponent<GraphicRaycaster>();

            // �J�����̐ݒ�
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = _uicamera;

            // ������ԂŉB��
            if (_isHideStartup)
            {
                _canvas.gameObject.SetActive(false);
                _isHide = true;
            }
        }

        /// <summary>
        /// UI�\��
        /// </summary>
        public async void Show()
        {
            await ShowTask();
        }

        /// <summary>
        /// UI�\���^�X�N��
        /// </summary>
        /// <returns></returns>
        public async UniTask ShowTask()
        {
            if (_isHide == true)
            {
                // �\������
                _canvas.gameObject.SetActive(true);

                // Open�A�j��
                if (_uiAnime != null)
                {
                    DisableTouch();
                    await _uiAnime.Play(UIAnimation.UIAnimationType.Open);
                    EnableTouch();
                }

                // �t���O�؂�ւ�
                _isHide = false;
            }
        }

        /// <summary>
        /// UI��\��
        /// </summary>
        public async void Hide()
        {
            await HideTask();
        }

        /// <summary>
        /// UI��\���^�X�N��
        /// </summary>
        public async UniTask HideTask()
        {
            if (_isHide == false)
            {
                // Close�A�j��
                if (_uiAnime != null)
                {
                    DisableTouch();
                    await _uiAnime.Play(UIAnimation.UIAnimationType.Close);
                    EnableTouch();
                }

                // ��\������
                _canvas.gameObject.SetActive(false);

                // �t���O�؂�ւ�
                _isHide = true;
            }
        }

        /// <summary>
        /// ����L����
        /// </summary>
        public void EnableTouch()
        {
            if (_rootRaycaster)
            {
                _rootRaycaster.enabled = true;
            }
        }

        /// <summary>
        /// ���얳����
        /// </summary>
        public void DisableTouch()
        {
            if (_rootRaycaster)
            {
                _rootRaycaster.enabled = false;
            }
        }
    }
}
