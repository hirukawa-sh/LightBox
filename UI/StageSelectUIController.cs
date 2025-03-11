using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableValue;

namespace Game.UI
{
    /// <summary>
    /// ステージセレクトUI
    /// </summary>
    public class StageSelectUIController : MonoBehaviour
    {
        /// <summary>
        /// ステージ一覧
        /// </summary>
        [SerializeField]
        StageListData _stageList;

        /// <summary>
        /// 現在のステージ番号
        /// </summary>
        [SerializeField]
        ScriptableIntValue _currentStageNumber;

        /// <summary>
        /// クリア済みステージ番号
        /// </summary>
        [SerializeField]
        ScriptableIntValue _cleardStageNumber;

        /// <summary>
        /// GridLayout
        /// </summary>
        [SerializeField]
        GridLayoutGroup _gridLayout;

        /// <summary>
        /// ボタンのプレハブ
        /// </summary>
        [SerializeField]
        Button _stageButtonPrefab;

        /// <summary>
        /// ロック状態のボタンプレハブ
        /// </summary>
        [SerializeField]
        Button _stageLockButtonPrefab;

        // Start is called before the first frame update
        void Start()
        {
            CreateButton();
        }

        /// <summary>
        /// ボタンの作成
        /// </summary>
        void CreateButton()
        {
            for (int i = 0; i < _stageList.ListData.Count; i++)
            {
                // ボタンの生成
                Button button;

                // 到達済みのステージだけボタンの有効化,
                // 未クリア状態のステージはロックする
                if (i < _cleardStageNumber.Value + 1)
                {
                    // 到達済みステージ
                    button = Instantiate(_stageButtonPrefab, _gridLayout.transform);

                    // クリックしたボタンのインデックスを設定
                    // 引数は一時変数を利用する（引数がポインタを参照する？らしく一時変数でないと反映されない）
                    var index = i;  // ←一時変数
                    button.onClick.AddListener(() => OnButtonClickedHandler(index));
                }
                else
                {
                    // 未クリアステージ（ロック状態）
                    button = Instantiate(_stageLockButtonPrefab, _gridLayout.transform);
                }

                // ラベルの設定
                var label = button.GetComponentInChildren<Text>();
                label.text = $"{i+1:000}";

            }
        }

        /// <summary>
        /// ボタンが押された時のイベント
        /// </summary>
        void OnButtonClickedHandler(int index)
        {
            // ステージ番号を書き込み
            _currentStageNumber.Value = index;
        }
    }
}