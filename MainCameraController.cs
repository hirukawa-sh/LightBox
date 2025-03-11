using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// ���C���J��������@��ɐݒ荀�ڂ���̔��f���s��
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

            // �w�i�F�̓����Ə�����
            _graphicsSettings.BackgroundColor.OnUpdateValue.AddListener(color =>
                _camera.backgroundColor = color
            );

            _camera.backgroundColor = _graphicsSettings.BackgroundColor.Value;
        }
    }
}