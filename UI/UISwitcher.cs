using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UIÇÃêÿÇËë÷Ç¶ÇçsÇ§
    /// </summary>
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField]
        UIController _hideUI;

        [SerializeField]
        UIController _showUI;

        public async void Switch()
        {
            if (_hideUI != null)
            {
                await _hideUI.HideTask();
            }

            if (_showUI != null)
            {
                await _showUI.ShowTask();
            }
        }
    }
}
