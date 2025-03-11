using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Game.AR
{
    /// <summary>
    /// 自身をARRaycastMangerを用いて再配置する　画面中央にレイを飛ばし交点位置に配置
    /// </summary>
    public class ARTargetReallocator : MonoBehaviour
    {
        [Tooltip("AR Raycast Manager コンポーネント")]
        [SerializeField]
        ARRaycastManager _raycast;

        [Tooltip("レイに反応するトラックタイプ（Plane or Point）")]
        [SerializeField]
        TrackableType _hitType;

        /// <summary>
        /// 配置座標のオフセット
        /// </summary>
        Vector3 _offset = Vector3.zero;

        /// <summary>
        /// 画面中央の座標
        /// </summary>
        Vector2 _screenCenterPoint;

        /// <summary>
        /// レイの結果
        /// </summary>
        List<ARRaycastHit> _hits = new List<ARRaycastHit>();

        // Start is called before the first frame update
        void Awake()
        {
            // PlaneまたはPointの場合、必要なコンポーネントが無ければ自動的に追加する
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

            // 画面中央の座標を計算
            _screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            // 現在の位置をオフセットとして保存
            _offset = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            // ARSessionが動作中のみ位置更新を行う
            if (ARSession.state == ARSessionState.SessionTracking)
            {
                if (_raycast.Raycast(_screenCenterPoint, _hits, _hitType))
                {
                    var point = _hits[0].pose.position;
                    transform.position = point + _offset;
                }
            }

            // 初期化中は非表示に
            else if (ARSession.state == ARSessionState.SessionInitializing)
            {
            }
        }
    }
}