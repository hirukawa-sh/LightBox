using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// メインカメラ制御　主に設定項目からの反映を行う
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField]
        GraphicsSettingsData _graphicsSettings;

        Camera _camera;

        void Start()
        {
            _camera = GetComponent<Camera>();

            // 背景色の同期と初期化
            _graphicsSettings.BackgroundColor.OnUpdateValue.AddListener(color =>
                _camera.backgroundColor = color
            );

            _camera.backgroundColor = _graphicsSettings.BackgroundColor.Value;
        }
    }
}