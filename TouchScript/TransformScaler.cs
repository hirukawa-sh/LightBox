using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

namespace TouchScript.Behaviors
{
    [RequireComponent(typeof(ITransformGesture))]
    public class TransformScaler : MonoBehaviour
    {
        [SerializeField]
        float _scaleSpeed = 1.0f;

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
            var deltaScale = 1 - _gesture.DeltaScale;

            transform.localPosition += Vector3.one * deltaScale * _scaleSpeed;
        }
    }
}