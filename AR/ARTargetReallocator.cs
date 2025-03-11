using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Game.AR
{
    /// <summary>
    /// ���g��ARRaycastManger��p���čĔz�u����@��ʒ����Ƀ��C���΂���_�ʒu�ɔz�u
    /// </summary>
    public class ARTargetReallocator : MonoBehaviour
    {
        [Tooltip("AR Raycast Manager �R���|�[�l���g")]
        [SerializeField]
        ARRaycastManager _raycast;

        [Tooltip("���C�ɔ�������g���b�N�^�C�v�iPlane or Point�j")]
        [SerializeField]
        TrackableType _hitType;

        /// <summary>
        /// �z�u���W�̃I�t�Z�b�g
        /// </summary>
        Vector3 _offset = Vector3.zero;

        /// <summary>
        /// ��ʒ����̍��W
        /// </summary>
        Vector2 _screenCenterPoint;

        /// <summary>
        /// ���C�̌���
        /// </summary>
        List<ARRaycastHit> _hits = new List<ARRaycastHit>();

        // Start is called before the first frame update
        void Awake()
        {
            // Plane�܂���Point�̏ꍇ�A�K�v�ȃR���|�[�l���g��������Ύ����I�ɒǉ�����
            switch (_hitType)
            {
                case TrackableType.Planes:
                case TrackableType.PlaneEstimated:
                case TrackableType.PlaneWithinBounds:
                case TrackableType.PlaneWithinInfinity:
                case TrackableType.PlaneWithinPolygon:
                    if (_raycast.GetComponent<ARPlaneManager>() == null)
                    {
                        _raycast.gameObject.AddComponent<ARPlaneManager>();
                    }
                    break;

                case TrackableType.FeaturePoint:
                    if (_raycast.GetComponent<ARPointCloudManager>() == null)
                    {
                        _raycast.gameObject.AddComponent<ARPointCloudManager>();
                    }
                    break;
            }

            // ��ʒ����̍��W���v�Z
            _screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            // ���݂̈ʒu���I�t�Z�b�g�Ƃ��ĕۑ�
            _offset = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            // ARSession�����쒆�݈̂ʒu�X�V���s��
            if (ARSession.state == ARSessionState.SessionTracking)
            {
                if (_raycast.Raycast(_screenCenterPoint, _hits, _hitType))
                {
                    var point = _hits[0].pose.position;
                    transform.position = point + _offset;
                }
            }

            // ���������͔�\����
            else if (ARSession.state == ARSessionState.SessionInitializing)
            {
            }
        }
    }
}