using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableValue;

namespace Game
{
    /// <summary>
    /// ステージ管理クラス、ステージに対してなんやかんや処理する
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        /// <summary>
        /// 生成後の親となるオブジェクト
        /// </summary>
        [SerializeField]
        Transform _parent;

        /// <summary>
        /// ステージリスト
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
        /// ゲームシーン読み込みイベント
        /// </summary>
        [SerializeField]
        UnityEvent onGameSceneLoadEvent;

        /// <summary>
        /// タイトルシーン読み込みイベント
        /// </summary>
        [SerializeField]
        UnityEvent onTitleSceneLoadEvent;

        /// <summary>
        /// タッチ無効化イベント
        /// </summary>
        [SerializeField]
        UnityEvent onDisableTouchEvent;

        /// <summary>
        /// タッチ有効化イベント
        /// </summary>
        [SerializeField]
        UnityEvent onEnableTouchEvent;

        /// <summary>
        /// 現在のステージ
        /// </summary>
        StageController _currentStage;

        /// <summary>
        /// ステージ生成
        /// </summary>
        public void CreateStage()
        {
            var stageData = _stageList.ListData[_currentStageNumber.Value];
            _currentStage = Instantiate(stageData, _parent);

            Debug.Log($"[{gameObject}] Stage Creation is Finished.");
        }

        /// <summary>
        /// Retryボタン
        /// </summary>
        public void RetryStage()
        {
            // ゲームシーン読み込み
            onGameSceneLoadEvent.Invoke();
        }

        /// <summary>
        /// Nextボタン
        /// </summary>
        public void NextStage()
        {
            // 最終ステージでなければ
            if (_currentStageNumber.Value < _stageList.ListData.Count - 1)
            {
                // ステージを進める
                _currentStageNumber.Value++;

                // ゲームシーン読み込み
                onGameSceneLoadEvent.Invoke();
            }
            // 最終ステージをクリアした場合
            else
            {
                // タイトルシーン読み込み（暫定）
                onTitleSceneLoadEvent.Invoke();
            }
        }

        /// <summary>
        /// ステージクリア
        /// </summary>
        public void StageClear()
        {
            // タッチ無効化
            onDisableTouchEvent.Invoke();

            // そのステージが初クリアなら、ステージ番号を記録する
            if (_currentStageNumber.Value == _cleardStageNumber.Value)
            {
                _cleardStageNumber.Value = _currentStageNumber.Value + 1;
            }
        }

        /// <summary>
        /// ヒント機能実行
        /// </summary>
        public async void Hint()
        {
            // タッチ無効化
            onDisableTouchEvent.Invoke();

            // ヒント実行（アニメーション終了まで待機）
            await _currentStage.HintAction();

            // まだステージクリアになっていないなら
            if (_currentStage.IsCleared == false)
            {
                // タッチ再有効化
                onEnableTouchEvent.Invoke();
            }
        }
    }
}