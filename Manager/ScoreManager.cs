using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ScriptableValue;
using Common;
using UnityEngine.Events;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// �X�R�A�Ǘ�
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>
        /// LeaderBoard
        /// </summary>
        [SerializeField]
        LeaderBoardData _leaderBoardData;

        /// <summary>
        /// ���݂̃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        ScriptableIntValue _currentStageNumberValue;

        /// <summary>
        /// �^�C�}�[
        /// </summary>
        [SerializeField]
        ScriptableFloatValue _timerValue;

        /// <summary>
        /// �^�b�`��
        /// </summary>
        [SerializeField]
        ScriptableIntValue _touchCountValue;

        /// <summary>
        /// �q���g��
        /// </summary>
        [SerializeField]
        ScriptableIntValue _hintCountValue;

        /// <summary>
        /// �X�R�A�X�V�����������ꍇ�ɌĂ΂��C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent _onNewRecordEvent;

        /// <summary>
        /// �X�g�b�v�E�H�b�`
        /// </summary>
        UnityStopwatch _stopWatch = new UnityStopwatch();

        void Start()
        {
            // �l�̏�����
            _touchCountValue.Reset();
            _hintCountValue.Reset();
            _timerValue.Reset();
        }

        /// <summary>
        /// �^�C�}�[�v���J�n
        /// </summary>
        public void StartTimer()
        {
            _stopWatch.Start();
        }

        /// <summary>
        /// �^�C�}�[�v����~
        /// </summary>
        public void StopTimer()
        {
            _stopWatch.Stop();

            // �v�����ʂ� ScriptableValue �ɏ�������
            _timerValue.Value = _stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// �^�b�`�񐔌v��
        /// </summary>
        public void TouchCountUp()
        {
            _touchCountValue.Value++;
        }

        /// <summary>
        /// �q���g�񐔌v��
        /// </summary>
        public void HintCountUp()
        {
            _hintCountValue.Value++;
        }

        /// <summary>
        /// �L�^��LeaderBoard�ɏ�������
        /// </summary>
        public void UpdateLeaderboard()
        {
            var result =_leaderBoardData.UpdateHighScore(_currentStageNumberValue.Value,
                            _timerValue.Value,
                            _touchCountValue.Value,
                            _hintCountValue.Value
                            );

            // �X�R�A�X�V������������C�x���g����
            if (result == true)
            {
                _onNewRecordEvent.Invoke();
            }
        }
    }
}