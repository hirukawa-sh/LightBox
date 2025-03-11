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
    /// UI表示・非表示制御
    /// </summary>
    public class UIController : MonoBehaviour
    {
        /// <summary>
        /// 初期状態で隠すか？
        /// </summary>
        [SerializeField]
        bool _isHideStartup;

        /// <summary>
        /// Canvas
        /// </summary>
        [SerializeField]
        Canvas _canvas;

        /// <summary>
        /// UI用カメラ
        /// </summary>
        [SerializeField]
        Camera _uiCamera;

        /// <summary>
        /// UIアニメーション
        /// </summary>
        [SerializeField]
        UIAnimation _uiAnime;

        /// <summary>
        /// 現在の状態　UI非表示か？
        /// </summary>
        bool _isHide = false;

        /// <summary>
        /// ルートキャンバスの参照
        /// </summary>
        Canvas _rootCanvas;

        /// <summary>
        /// ルートキャンバスのGraphicRaycaster
        /// </summary>
        GraphicRaycaster _rootRaycaster;

        void Start()
        {
            // UI用カメラの取得
            var _uicamera = GameObject.Find(_uiCamera.name)?.GetComponent<Camera>();

            // シーン内に見つからなければ生成
            if (_uicamera == null)
            {
                _uicamera = Instantiate(_uiCamera);
            }

            // UIキャンバスの取得
            if (_canvas == null)
            {
                // 指定が無い場合は自分で探す
                _canvas = GetComponentInParent<Canvas>();
            }

            // ルートキャンバスを取得
            _rootCanvas = _canvas.rootCanvas;
            _rootRaycaster = _rootCanvas.GetComponent<GraphicRaycaster>();

            // カメラの設定
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = _uicamera;

            // 初期状態で隠す
            if (_isHideStartup)
            {
                _canvas.gameObject.SetActive(false);
                _isHide = true;
            }
        }

        /// <summary>
        /// UI表示
        /// </summary>
        public async void Show()
        {
            await ShowTask();
        }

        /// <summary>
        /// UI表示タスク版
        /// </summary>
        /// <returns></returns>
        public async UniTask ShowTask()
        {
            if (_isHide == true)
            {
                // 表示処理
                _canvas.gameObject.SetActive(true);

                // Openアニメ
                if (_uiAnime != null)
                {
                    DisableTouch();
                    await _uiAnime.Play(UIAnimation.UIAnimationType.Open);
                    EnableTouch();
                }

                // フラグ切り替え
                _isHide = false;
            }
        }

        /// <summary>
        /// UI非表示
        /// </summary>
        public async void Hide()
        {
            await HideTask();
        }

        /// <summary>
        /// UI非表示タスク版
        /// </summary>
        public async UniTask HideTask()
        {
            if (_isHide == false)
            {
                // Closeアニメ
                if (_uiAnime != null)
                {
                    DisableTouch();
                    await _uiAnime.Play(UIAnimation.UIAnimationType.Close);
                    EnableTouch();
                }

                // 非表示処理
                _canvas.gameObject.SetActive(false);

                // フラグ切り替え
                _isHide = true;
            }
        }

        /// <summary>
        /// 操作有効化
        /// </summary>
        public void EnableTouch()
        {
            if (_rootRaycaster)
            {
                _rootRaycaster.enabled = true;
            }
        }

        /// <summary>
        /// 操作無効化
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
