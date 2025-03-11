#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Platform事に必要なものをセットアップする
    /// </summary>
    public class PlatformSetup : MonoBehaviour
    {
#if UNITY_IOS || UNITY_ANDROID
        [Tooltip("広告を有効にするか？")]
        [SerializeField]
        bool _enableAds = true;

        [Tooltip("広告")]
        [SerializeField]
        GameObject _ads;
#endif

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        [Tooltip("デバッグ用UIを有効にするか？")]
        [SerializeField]
        bool _enableDebugUI = true;

        [Tooltip("デバッグ用UI")]
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