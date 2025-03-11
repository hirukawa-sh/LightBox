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
    /// �V�[������
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// �V�[���̎��
        /// </summary>
        public enum SceneType
        {
            Title,
            GamePlay,
        }

        /// <summary>
        /// Fade�R���|�[�l���g
        /// </summary>
        [SerializeField]
        Fade _fade;

        /// <summary>
        /// �t�F�[�h�A�j���[�V�������ԁi�b�j
        /// </summary>
        [SerializeField]
        float _fadeTime = 0.5f;

        /// <summary>
        /// �N�����Ɏ����I�ɃV�[����ǂݍ���
        /// </summary>
        [SerializeField]
        bool _loadOnAwake = false;

        /// <summary>
        /// AR���[�h�Ŏ��s���邩�H
        /// </summary>
        [SerializeField]
        ScriptableBooleanValue _isAR;

        /// <summary>
        /// ���[�h�����V�[�����A�N�e�B�u�ɂ��邩�H
        /// </summary>
        [SerializeField]
        bool _activateLoadScene;

        /// <summary>
        /// �����V�[��
        /// </summary>
        [SerializeField]
        SceneType _defaultScene;

        /// <summary>
        /// �Q�[���V�[��
        /// </summary>
        [SerializeField]
        SceneReference _gameScene;

        /// <summary>
        /// AR�Q�[���V�[��
        /// </summary>
        [SerializeField]
        SceneReference _AR_gameScene;

        /// <summary>
        /// �^�C�g���V�[��
        /// </summary>
        [SerializeField]
        SceneReference _titleScene;

        /// <summary>
        /// ���[�h�J�n�C�x���g
        /// </summary>
        public UnityEvent LoadStartEvent;

        /// <summary>
        /// ���[�h�����C�x���g
        /// </summary>
        public UnityEvent LoadCompleteEvent;

        /// <summary>
        /// ���݂̃V�[��
        /// </summary>
        SceneReference _currentScene;

        void Start()
        {
            if (_loadOnAwake == true)
            {
                // �����V�[���ǂݍ���
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
        /// ���݂̃V�[�����폜
        /// </summary>
        async void UnloadCurrentScene()
        {
            if (_currentScene != null)
            {
                await SceneManager.UnloadSceneAsync(_currentScene);
            }
        }

        /// <summary>
        /// �V�[���ǂݍ���
        /// </summary>
        /// <param name="scene"></param>
        void LoadScene(SceneReference scene)
        {
            // �t�F�[�h�C�����s
            _fade.FadeIn(_fadeTime, async () => {

                // ���t�F�[�h�C��������̏���

                    // �V�[���ǂݍ��݊J�n�C�x���g���s
                    LoadStartEvent.Invoke();

                    // �����V�[�����폜
                    UnloadCurrentScene();

                    // �V�[���ǂݍ��ݎ��s
                    await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

                    // ���݂̃V�[����ݒ�
                    _currentScene = scene;

                    // ���[�h�����V�[�����A�N�e�B�u��
                    if (_activateLoadScene)
                    {
                        var loadedScene = SceneManager.GetSceneByPath(scene.ScenePath);
                        SceneManager.SetActiveScene(loadedScene);
                    }

                    // �N������̏��񃍁[�h���̓ǂݍ��݂ŏ����������������A
                    // ���܂��t�F�[�h�A�E�g�ł��Ȃ��̂ŁA�΍�Ƃ���
                    // �b��I�ɂQ�b�ҋ@����
                    await UniTask.Delay(2);

                    // �t�F�[�h�A�E�g����
                    _fade.FadeOut(_fadeTime);

                    // �V�[���ǂݍ��݊����C�x���g���s
                    LoadCompleteEvent.Invoke();

                    Debug.Log($"[{gameObject}] {_currentScene.ScenePath} is loaded.");
            });
        }

        /// <summary>
        /// �Q�[���V�[���ǂݍ���
        /// </summary>
        public void LoadGameScene()
        {
            // AR���[�h���`�F�b�N
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
        /// �^�C�g����ʓǂݍ���
        /// </summary>
        public void LoadTitleScene()
        {
            LoadScene(_titleScene);
        }
    }
}