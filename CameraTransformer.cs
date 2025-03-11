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
        // �Ώۂ̃J����
        [SerializeField]
        Camera _targetCamera;

        // �J�����̉�]���x
        [SerializeField]
        float _rotateSpeed = 1.0f;

        // �J�����̃Y�[�����x
        [SerializeField]
        float _zoomSpeed = 1.0f;

        // �Y�[���Œ዗��
        [SerializeField]
        float _zoomMin = -20f;

        // �Y�[���ő勗��
        [SerializeField]
        float _zoomMax = -1f;

        // �J�����ݒ�f�[�^
        [SerializeField]
        CameraSettingsData _cameraSettings;

        // Gesture�R���|�[�l���g
        ScreenTransformGesture _gesture;

        // �Y�[�������l�ۑ��p
        float _defaultZoom;

        // Start is called before the first frame update
        private void Awake()
        {
            _gesture = GetComponent<ScreenTransformGesture>();

            // �f�t�H���g�̃Y�[���l�iZ�l�j��ۑ����Ă���
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
        /// �J������]�W�F�X�`���[
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenTransformGestureHandler(object sender, System.EventArgs e)
        {
            var rotx = _gesture.DeltaPosition.x * _rotateSpeed;
            var roty = _gesture.DeltaPosition.y * _rotateSpeed;
            var zoom = (_gesture.DeltaScale -1f) * _zoomSpeed;

            // �J�����̉�]

            // ��]�������t�ɂ���ꍇ
            if (_cameraSettings.InverseCameraX.Value)
            {
                rotx = -rotx;
            }
            if (_cameraSettings.InverseCameraY.Value)
            {
                roty = -roty;
            }

            // ��]��X���AY���ɑ΂����]�Ȃ̂�
            // X��Y�͓��͂Ƌt�ɂȂ�
            transform.rotation *= Quaternion.Euler(roty, rotx, 0);

            // �J�����̃Y�[��
            _targetCamera.transform.localPosition += Vector3.forward * zoom;

            // �Y�[�������̐���
            var cameraZ = Mathf.Clamp(_targetCamera.transform.localPosition.z, _zoomMin, _zoomMax);
            _targetCamera.transform.localPosition =
                new Vector3(_targetCamera.transform.localPosition.x, _targetCamera.transform.localPosition.y, cameraZ);
        }

        /// <summary>
        /// �J���������̏�����
        /// </summary>
        public void ResetCamera()
        {
            // ��]�̏�����
            transform.rotation = Quaternion.identity;

            // �J�����̃Y�[����������
            _targetCamera.transform.localPosition =
                new Vector3(_targetCamera.transform.localPosition.x, _targetCamera.transform.localPosition.y, _defaultZoom);
        }
    }
}