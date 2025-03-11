using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ScriptableValue;

namespace Game.UI
{
    /// <summary>
    /// ヒントボタン制御
    /// </summary>
    public class HintButtonController : MonoBehaviour
    {
        /// <summary>
        /// リワード広告閲覧済みフラグ
        /// </summary>
        [SerializeField]
        ScriptableBooleanValue _isRewardsCompleted;

        /// <summary>
        /// ボタンの「Ad」表示
        /// </summary>
        [SerializeField]
        Text _adText;

        /// <summary>
        /// ヒント実行イベント
        /// </summary>
        [SerializeField]
        UnityEvent _onExecuteHintEvent;

        /// <summary>
        /// リワード広告実行イベント
        /// </summary>
        [SerializeField]
        UnityEvent _onShowRewardsEvent;

#if UNITY_WEBGL
#elif UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        // Start is called before the first frame update
        void OnEnable()
        {
            // リワード広告未閲覧なら「Ads」を表示
            if (_isRewardsCompleted.Value == false)
            {
                _adText.gameObject.SetActive(true);
                return;
            }
            _adText.gameObject.SetActive(false);
        }
#endif

        /// <summary>
        /// ボタンが押された時
        /// </summary>
        public void OnButtonClicked()
        {
#if UNITY_WEBGL
#elif UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            // リワード広告閲覧済みならヒント実行
            if (_isRewardsCompleted.Value == true)
            {
                _onExecuteHintEvent.Invoke();
                return;
            }
#endif
            // そうでないなら広告表示
            _onShowRewardsEvent.Invoke();
        }
    }
}