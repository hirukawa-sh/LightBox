#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Debugger
{
    /// <summary>
    /// ï\é¶êÿÇËë÷Ç¶
    /// </summary>
    public class ToggleView : MonoBehaviour
    {
        [SerializeField]
        bool _toggle = true;

        void Start()
        {
            OnToggleView();
        }

        public void OnToggleView()
        {
            // èÛë‘îΩì]
            if (_toggle = !_toggle)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
#endif
