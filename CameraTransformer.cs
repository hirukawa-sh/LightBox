using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;
using Game.Settings;

namespace Game
{
    [RequireComponent(typeof(ScreenTransformGesture))]
    public class CameraTransformer : MonoBehaviour
    {
        // 対象のカメラ
        [SerializeField]
        Camera _targetCamera;

        // カメラの回転速度
        [SerializeField]
        float _rotateSpeed = 1.0f;

        // カメラのズーム速度
        [SerializeField]
        float _zoomSpeed = 1.0f;

        // ズーム最低距離
        [SerializeField]
        float _zoomMin = -20f;

        // ズーム最大距離
        [SerializeField]
        float _zoomMax = -1f;

        // カメラ設定データ
        [SerializeField]
        CameraSettingsData _cameraSettings;

        // Gestureコンポーネント
        ScreenTransformGesture _gesture;

        // ズーム初期値保存用
        float _defaultZoom;

        // Start is called before the first frame update
        private void Awake()
        {
            _gesture = GetComponent<ScreenTransformGesture>();

            // デフォルトのズーム値（Z値）を保存しておく
            _defaultZoom = _targetCamera.transform.localPosition.z;
        }

        private void OnEnable()
        {
            _gesture.Transformed += ScreenTransformGestureHandler;
        }

        private void OnDisable()
        {
            _gesture.Transformed -= ScreenTransformGestureHandler;
        }

        /// <summary>
        /// カメラ回転ジェスチャー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenTransformGestureHandler(object sender, System.EventArgs e)
        {
            var rotx = _gesture.DeltaPosition.x * _rotateSpeed;
            var roty = _gesture.DeltaPosition.y * _rotateSpeed;
            var zoom = (_gesture.DeltaScale -1f) * _zoomSpeed;

            // カメラの回転

            // 回転方向を逆にする場合
            if (_cameraSettings.InverseCameraX.Value)
            {
                rotx = -rotx;
            }
            if (_cameraSettings.InverseCameraY.Value)
            {
                roty = -roty;
            }

            // 回転はX軸、Y軸に対する回転なので
            // XとYは入力と逆になる
            transform.rotation *= Quaternion.Euler(roty, rotx, 0);

            // カメラのズーム
            _targetCamera.transform.localPosition += Vector3.forward * zoom;

            // ズーム距離の制限
            var cameraZ = Mathf.Clamp(_targetCamera.transform.localPosition.z, _zoomMin, _zoomMax);
            _targetCamera.transform.localPosition =
                new Vector3(_targetCamera.transform.localPosition.x, _targetCamera.transform.localPosition.y, cameraZ);
        }

        /// <summary>
        /// カメラ向きの初期化
        /// </summary>
        public void ResetCamera()
        {
            // 回転の初期化
            transform.rotation = Quaternion.identity;

            // カメラのズームを初期化
            _targetCamera.transform.localPosition =
                new Vector3(_targetCamera.transform.localPosition.x, _targetCamera.transform.localPosition.y, _defaultZoom);
        }
    }
}