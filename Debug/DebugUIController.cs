#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Debugger
{
    /// <summary>
    /// デバッグUI制御
    /// </summary>
    public class DebugUIController : MonoBehaviour
    {
        /// <summary>
        /// ゲームデータ
        /// </summary>
        [SerializeField]
        Game.Settings.GameData _gameData;

        /// <summary>
        /// デバッグ表示用Canvas
        /// </summary>
        [SerializeField]
        Canvas _canvas;

        /// <summary>
        /// 初期状態でデバッグ表示を隠す？
        /// </summary>
        [SerializeField]
        bool _isHideStartup = true;

        void Start()
        {
            if (_isHideStartup)
            {
                _canvas.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// デバッグ用UI表示
        /// </summary>
        public void OpenDebugUI()
        {
            _canvas.gameObject.SetActive(true);
        }
    }
}
#endif