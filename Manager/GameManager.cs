using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    /// <summary>
    /// �Q�[���Ǘ��N���X
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// �Q�[���J�n�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onGameStartEvent;

        /// <summary>
        /// �|�[�Y�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onPauseEvent;

        /// <summary>
        /// �^�b�`�������C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onDisableTouchEvent;

        /// <summary>
        /// �^�b�`�L�����C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onEnableTouchEvent;

        /// <summary>
        /// �|�[�Y���t���O
        /// </summary>
        bool _isPause = false;

        /// <summary>
        /// �O���|�[�Y�L�����t���O
        /// </summary>
        bool _enableApplicaitonPause = true;

        // Start is called before the first frame update
        void Start()
        {
            // �Q�[���J�n�C�x���g���s
            onGameStartEvent.Invoke();
        }

        /// <summary>
        /// �A�v�����O�������~�����ꍇ
        /// </summary>
        /// <param name="pause">�|�[�Y�t���O</param>
        void OnApplicationPause(bool pause)
        {
            // �O�������~���ꂽ
            if (pause == true)
            {
                // �|�[�Y���ł͂Ȃ��A���O���|�[�Y�L���̏ꍇ
                if (!_isPause && _enableApplicaitonPause)
                {
                    // �|�[�Y�C�x���g����
                    onPauseEvent.Invoke();
                }
            }
        }

        /// <summary>
        /// �Q�[���ꎞ��~
        /// </summary>
        public void GamePause()
        {
            // �^�b�`������
            onDisableTouchEvent.Invoke();

            // �|�[�Y�t���O�I��
            _isPause = true;

            Debug.Log($"[{gameObject}] Game is Paused.");
        }

        /// <summary>
        /// �Q�[���ĊJ
        /// </summary>
        public void GameResume()
        {
            //�^�b�`�L����
            onEnableTouchEvent.Invoke();

            // �|�[�Y�t���O�I�t
            _isPause = false;

            Debug.Log($"[{gameObject}] Game is Resumed.");
        }

        /// <summary>
        /// �O���|�[�Y�L����
        /// </summary>
        public void EnableApplicationPause()
        {
            _enableApplicaitonPause = true;
        }

        /// <summary>
        /// �ꎞ�I�ɊO���|�[�Y�𖳌��ɂ���
        /// </summary>
        public void DisableApplicationPause()
        {
            _enableApplicaitonPause = false;
        }
    }
}