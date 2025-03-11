using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ScriptableValue;

namespace Game.UI
{
    /// <summary>
    /// �q���g�{�^������
    /// </summary>
    public class HintButtonController : MonoBehaviour
    {
        /// <summary>
        /// �����[�h�L���{���ς݃t���O
        /// </summary>
        [SerializeField]
        ScriptableBooleanValue _isRewardsCompleted;

        /// <summary>
        /// �{�^���́uAd�v�\��
        /// </summary>
        [SerializeField]
        Text _adText;

        /// <summary>
        /// �q���g���s�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent _onExecuteHintEvent;

        /// <summary>
        /// �����[�h�L�����s�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent _onShowRewardsEvent;

#if UNITY_WEBGL
#elif UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        // Start is called before the first frame update
        void OnEnable()
        {
            // �����[�h�L�����{���Ȃ�uAds�v��\��
            if (_isRewardsCompleted.Value == false)
            {
                _adText.gameObject.SetActive(true);
                return;
            }
            _adText.gameObject.SetActive(false);
        }
#endif

        /// <summary>
        /// �{�^���������ꂽ��
        /// </summary>
        public void OnButtonClicked()
        {
#if UNITY_WEBGL
#elif UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            // �����[�h�L���{���ς݂Ȃ�q���g���s
            if (_isRewardsCompleted.Value == true)
            {
                _onExecuteHintEvent.Invoke();
                return;
            }
#endif
            // �����łȂ��Ȃ�L���\��
            _onShowRewardsEvent.Invoke();
        }
    }
}