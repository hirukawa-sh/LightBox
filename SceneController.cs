using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using ScriptableValue;

namespace Game
{
    /// <summary>
    /// シーン制御
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// シーンの種類
        /// </summary>
        public enum SceneType
        {
            Title,
            GamePlay,
        }

        /// <summary>
        /// Fadeコンポーネント
        /// </summary>
        [SerializeField]
        Fade _fade;

        /// <summary>
        /// フェードアニメーション時間（秒）
        /// </summary>
        [SerializeField]
        float _fadeTime = 0.5f;

        /// <summary>
        /// 起動時に自動的にシーンを読み込む
        /// </summary>
        [SerializeField]
        bool _loadOnAwake = false;

        /// <summary>
        /// ARモードで実行するか？
        /// </summary>
        [SerializeField]
        ScriptableBooleanValue _isAR;

        /// <summary>
        /// ロードしたシーンをアクティブにするか？
        /// </summary>
        [SerializeField]
        bool _activateLoadScene;

        /// <summary>
        /// 初期シーン
        /// </summary>
        [SerializeField]
        SceneType _defaultScene;

        /// <summary>
        /// ゲームシーン
        /// </summary>
        [SerializeField]
        SceneReference _gameScene;

        /// <summary>
        /// ARゲームシーン
        /// </summary>
        [SerializeField]
        SceneReference _AR_gameScene;

        /// <summary>
        /// タイトルシーン
        /// </summary>
        [SerializeField]
        SceneReference _titleScene;

        /// <summary>
        /// ロード開始イベント
        /// </summary>
        public UnityEvent LoadStartEvent;

        /// <summary>
        /// ロード完了イベント
        /// </summary>
        public UnityEvent LoadCompleteEvent;

        /// <summary>
        /// 現在のシーン
        /// </summary>
        SceneReference _currentScene;

        void Start()
        {
            if (_loadOnAwake == true)
            {
                // 初期シーン読み込み
                switch (_defaultScene)
                {
                    case SceneType.Title:
                        LoadTitleScene();
                        break;
                    case SceneType.GamePlay:
                        LoadGameScene();
                        break;
                }
            }
        }

        /// <summary>
        /// 現在のシーンを削除
        /// </summary>
        async void UnloadCurrentScene()
        {
            if (_currentScene != null)
            {
                await SceneManager.UnloadSceneAsync(_currentScene);
            }
        }

        /// <summary>
        /// シーン読み込み
        /// </summary>
        /// <param name="scene"></param>
        void LoadScene(SceneReference scene)
        {
            // フェードイン実行
            _fade.FadeIn(_fadeTime, async () => {

                // ↓フェードイン完了後の処理

                    // シーン読み込み開始イベント発行
                    LoadStartEvent.Invoke();

                    // 既存シーンを削除
                    UnloadCurrentScene();

                    // シーン読み込み実行
                    await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

                    // 現在のシーンを設定
                    _currentScene = scene;

                    // ロードしたシーンをアクティブに
                    if (_activateLoadScene)
                    {
                        var loadedScene = SceneManager.GetSceneByPath(scene.ScenePath);
                        SceneManager.SetActiveScene(loadedScene);
                    }

                    // 起動直後の初回ロード時の読み込みで処理落ちが発生し、
                    // うまくフェードアウトできないので、対策として
                    // 暫定的に２秒待機する
                    await UniTask.Delay(2);

                    // フェードアウト処理
                    _fade.FadeOut(_fadeTime);

                    // シーン読み込み完了イベント発行
                    LoadCompleteEvent.Invoke();

                    Debug.Log($"[{gameObject}] {_currentScene.ScenePath} is loaded.");
            });
        }

        /// <summary>
        /// ゲームシーン読み込み
        /// </summary>
        public void LoadGameScene()
        {
            // ARモードかチェック
            if (_isAR.Value)
            {
                LoadScene(_AR_gameScene);
            }
            else
            {
                LoadScene(_gameScene);
            }
        }

        /// <summary>
        /// タイトル画面読み込み
        /// </summary>
        public void LoadTitleScene()
        {
            LoadScene(_titleScene);
        }
    }
}