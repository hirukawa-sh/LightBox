using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LoadingUIController : MonoBehaviour
    {
        /// <summary>
        /// フェードコンポーネント
        /// </summary>
        [SerializeField]
        Fade _fade;

        /// <summary>
        /// UIコントローラ
        /// </summary>
        [SerializeField]
        UIController _uiController;

        /// <summary>
        /// フェードイン時間（秒）
        /// </summary>
        [SerializeField]
        float _fadeInTime;

        /// <summary>
        /// フェードアウト時間（秒）
        /// </summary>
        [SerializeField]
        float _fadeOutTime;

        public void FadeIn()
        {
            _uiController.Show();
            _fade.FadeIn(_fadeInTime);
        }

        public void FadeOut()
        {
            _fade.FadeOut(_fadeOutTime);
            _uiController.Hide();
        }
    }
}