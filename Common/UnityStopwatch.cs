using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Unity�N���X���Ŏg�p�\�ȃX�g�b�v�E�H�b�`
    /// ���o�C�����ł́ASystem.Diagnostics.Stopwatch ���g�p�ł��Ȃ��̂�
    /// ����Ɏg�p����
    /// </summary>
    public class UnityStopwatch
    {
        // �v���J�n���ԋL�^�p
        float _startValue = 0.0f;

        // �v����~���ԋL�^�p
        float _stopValue = 0.0f;

        // ���v����
        float _totalTime = 0.0f;

        /// <summary>
        /// �l���擾�i�~���b�Along�^�j
        /// </summary>
        public long ElapsedMilliseconds
        {
            get
            {
                // �~���b�ɒ����Ă���long�^�ɕϊ�
                return (long)((_totalTime) * 1000);
            }
        }

        /// <summary>
        /// �v���J�n
        /// </summary>
        public void Start()
        {
            _startValue = Time.time;
        }

        /// <summary>
        /// �v����~
        /// </summary>
        public void Stop()
        {
            _stopValue = Time.time;

            // �o�ߎ��Ԃ����߂�
            _totalTime += _stopValue - _startValue;
        }

        /// <summary>
        /// �l�̃N���A
        /// </summary>
        public void Reset()
        {
            // �l���[���ɂ���
            _startValue = _stopValue = _totalTime = 0.0f;
        }
    }
}