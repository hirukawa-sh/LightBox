using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableValue;

namespace Game.UI
{
    /// <summary>
    /// チュートリアル専用
    /// </summary>
    public class TutorialUIController : MonoBehaviour
    {
        /// <summary>
        /// ARモード動作フラグ
        /// </summary>
        [SerializeField]
        ScriptableBooleanValue _isARMode;

        /// <summary>
        /// クリア済みのステージ番号
        /// </summary>
        [SerializeField]
        ScriptableIntValue _clearedStageNumber;

        /// <summary>
        /// 現在のステージ番号
        /// </summary>
        [SerializeField]
        int _currentStageNumber;

        void Start()
        {
            // ARモードで動作中はチュートリアルを表示しない
            if (_isARMode.Value == true)
            {
                gameObject.SetActive(false);
            }
            else
            {
                // 現在のステージがクリア済みならチュートリアルを表示しないようにする
                if (_clearedStageNumber.Value < _currentStageNumber)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}