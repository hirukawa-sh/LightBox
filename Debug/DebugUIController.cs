#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Debugger
{
    /// <summary>
    /// �f�o�b�OUI����
    /// </summary>
    public class DebugUIController : MonoBehaviour
    {
        /// <summary>
        /// �Q�[���f�[�^
        /// </summary>
        [SerializeField]
        Game.Settings.GameData _gameData;

        /// <summary>
        /// �f�o�b�O�\���pCanvas
        /// </summary>
        [SerializeField]
        Canvas _canvas;

        /// <summary>
        /// ������ԂŃf�o�b�O�\�����B���H
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
        /// �f�o�b�O�pUI�\��
        /// </summary>
        public void OpenDebugUI()
        {
            _canvas.gameObject.SetActive(true);
        }
    }
}
#endif