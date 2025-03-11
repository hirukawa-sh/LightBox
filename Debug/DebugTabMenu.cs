#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Debugger
{
    /// <summary>
    /// �f�o�b�O�p�^�u���j���[
    /// </summary>
    [RequireComponent(typeof(EnhancedToggleGroup))]
    public class DebugTabMenu : MonoBehaviour
    {
        [SerializeField]
        DebugLogViewer _debugLogViewer;

        [SerializeField]
        SystemInfoViewer _systemInfoViewer;

        // �ǂ�UI��\�����邩�H
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
            // �f�t�H���g�\��
            OnToggleCheck();
        }

        /// <summary>
        /// �\���ؑ�
        /// </summary>
        /// <param name="type">�\������^�C�v</param>
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
        /// �g�O���`�F�b�N�@��Ԃɉ�����UI�\��
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