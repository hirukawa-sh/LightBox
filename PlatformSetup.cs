#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Platform���ɕK�v�Ȃ��̂��Z�b�g�A�b�v����
    /// </summary>
    public class PlatformSetup : MonoBehaviour
    {
#if UNITY_IOS || UNITY_ANDROID
        [Tooltip("�L����L���ɂ��邩�H")]
        [SerializeField]
        bool _enableAds = true;

        [Tooltip("�L��")]
        [SerializeField]
        GameObject _ads;
#endif

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        [Tooltip("�f�o�b�O�pUI��L���ɂ��邩�H")]
        [SerializeField]
        bool _enableDebugUI = true;

        [Tooltip("�f�o�b�O�pUI")]
        [SerializeField]
        GameObject _debugUI;
#endif

        // Start is called before the first frame update
        void Start()
        {
#if UNITY_IOS || UNITY_ANDROID
            if (_enableAds)
            {
                Instantiate(_ads, transform);
            }
#endif
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if (_enableDebugUI)
            {
                Instantiate(_debugUI, transform);
            }
#endif
        }
    }
}
#endif