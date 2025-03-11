using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

namespace TouchScript.Behaviors
{
    [RequireComponent(typeof(ITransformGesture))]
    public class TransformRotator : MonoBehaviour
    {
        [SerializeField]
        bool _isRotateX = true;

        [SerializeField]
        bool _isRotateY = true;

        [SerializeField]
        float _rotateSpeed = 15;

        ITransformGesture _gesture;

        // Start is called before the first frame update
        void Awake()
        {
            _gesture = GetComponent<ITransformGesture>();
        }
        void OnEnable()
        {
            _gesture.Transformed += OnTransformed;
        }

        void OnDisable()
        {
            _gesture.Transformed -= OnTransformed;
        }

        void OnTransformed(object obj, System.EventArgs e)
        {
            if (_isRotateX)
            {
                var deltaRotX = _gesture.DeltaPosition.x;
                transform.Rotate(transform.up, deltaRotX * _rotateSpeed);
            }

            if (_isRotateY)
            {
                var deltaRotY = _gesture.DeltaPosition.y;
                transform.Rotate(transform.right, deltaRotY * _rotateSpeed);
            }
        }
    }
}