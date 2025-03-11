#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Debugger
{
    /// <summary>
    /// デバッグ用タブメニュー
    /// </summary>
    [RequireComponent(typeof(EnhancedToggleGroup))]
    public class DebugTabMenu : MonoBehaviour
    {
        [SerializeField]
        DebugLogViewer _debugLogViewer;

        [SerializeField]
        SystemInfoViewer _systemInfoViewer;

        // どのUIを表示するか？
        enum ViewType
        {
            DebugLogViewer,
            SystemInfoViewer,
            Control,
        }
        ViewType _type;

        EnhancedToggleGroup _group;

        // Start is called before the first frame update
        void Awake()
        {
            _group = GetComponent<EnhancedToggleGroup>();
            _debugLogViewer.enabled = false;
            _systemInfoViewer.enabled = false;
        }

        void Start()
        {
            // デフォルト表示
            OnToggleCheck();
        }

        /// <summary>
        /// 表示切替
        /// </summary>
        /// <param name="type">表示するタイプ</param>
        void ChangeView(ViewType type)
        {
            switch (type)
            {
                case ViewType.DebugLogViewer:
                    _debugLogViewer.enabled = true;
                    _systemInfoViewer.enabled = false;
                    break;

                case ViewType.SystemInfoViewer:
                    _debugLogViewer.enabled = false;
                    _systemInfoViewer.enabled = true;
                    break;

                case ViewType.Control:
                    break;
            }
        }

        /// <summary>
        /// トグルチェック　状態に応じてUI表示
        /// </summary>
        public void OnToggleCheck()
        {
            var index = _group.GetActiveToggleIndex();
            if (index == 1)
            {
                ChangeView(ViewType.DebugLogViewer);
            }
            else if (index == 2)
            {
                ChangeView(ViewType.SystemInfoViewer);
            }
        }
    }
}
#endif