using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LoadingUIController : MonoBehaviour
    {
        /// <summary>
        /// �t�F�[�h�R���|�[�l���g
        /// </summary>
        [SerializeField]
        Fade _fade;

        /// <summary>
        /// UI�R���g���[��
        /// </summary>
        [SerializeField]
        UIController _uiController;

        /// <summary>
        /// �t�F�[�h�C�����ԁi�b�j
        /// </summary>
        [SerializeField]
        float _fadeInTime;

        /// <summary>
        /// �t�F�[�h�A�E�g���ԁi�b�j
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