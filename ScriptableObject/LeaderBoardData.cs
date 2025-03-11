using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    /// <summary>
    /// �ʃf�[�^�̍\����
    /// </summary>
    [System.Serializable]
    public struct ScoreData
    {
        public float ClearTime;
        public int TouchCount;
        public int HintCount;

        public ScoreData(float clearTime, int touchCount, int hintCount)
        {
            ClearTime = clearTime;
            TouchCount = touchCount;
            HintCount = hintCount;
        }

        public override string ToString()
        {
            return base.ToString() + $"({ClearTime}, {TouchCount}, {HintCount})";
        }
    }

    /// <summary>
    /// �Z�[�u/���[�h�p�̍\����
    /// </summary>
    [System.Serializable]
    public struct LeaderBoard
    {
        public readonly ScoreData[] ScoreDatas;

        public LeaderBoard(LeaderBoardData leaderBoardData)
        {
            ScoreDatas = leaderBoardData.ScoreDatas;
        }
    }

    /// <summary>
    /// LeaderBoard�̃f�[�^
    /// </summary>
    [CreateAssetMenu(fileName = "LeaderBoardData", menuName = "GameData/LeaderBoardData", order = 1)]
    public class LeaderBoardData : ScriptableObject, ISaveDataAccess<LeaderBoard>
    {
        /// <summary>
        /// �f�[�^�̔z��
        /// </summary>
        [SerializeField]
        List<ScoreData> _scoreDatas = new List<ScoreData>();

        /// <summary>
        /// �f�[�^�̎擾
        /// </summary>
        public ScoreData[] ScoreDatas
        {
            get
            {
                return _scoreDatas.ToArray();
            }
        }

        /// <summary>
        /// �f�[�^���擾
        /// </summary>
        public int Count
        {
            get
            {
                return _scoreDatas.Count;            
            }
        }

        /// <summary>
        /// �����l��ݒ�
        /// </summary>
        public void Default()
        {
            _scoreDatas.Clear();
        }

        /// <summary>
        /// ���[�h���̃f�[�^�Z�b�g
        /// </summary>
        /// <param name="data"></param>
        public void Set(LeaderBoard data)
        {
            _scoreDatas.Clear();
            _scoreDatas.AddRange(data.ScoreDatas);
        }

        /// <summary>
        /// �n�C�X�R�A�擾
        /// </summary>
        /// <param name="stageNumber"></param>
        public ScoreData GetHighScore(int stageNumber)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                return _scoreDatas[stageNumber];
            }

            // �f�[�^�����݂��Ȃ��ꍇ�͋�f�[�^��Ԃ�
            return new ScoreData();
        }

        /// <summary>
        /// �n�C�X�R�A�X�V
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="clearTime"></param>
        /// <param name="touchCount"></param>
        /// <param name="hintCount"></param>
        public bool UpdateHighScore(int stageNumber, float clearTime, int touchCount, int hintCount)
        {
            // �X�R�A�X�V���s��ꂽ�ꍇ�̃t���O
            var isUpdate = false;

            // �X�e�[�W���V�K�N���A�i�ǉ��j���A�����N���A�i�X�V�j���`�F�b�N
            if (stageNumber < _scoreDatas.Count)
            {
                // �����N���A�Ȃ�A�n�C�X�R�A�Ɣ�r���čX�V����Ă���΃f�[�^��ۑ�

                // �n�C�X�R�A�擾
                var highScore = GetHighScore(stageNumber);

                // ��r����
                // �܂��q���g�񐔂����Ȃ������ꍇ
                if (hintCount < highScore.HintCount)
                {
                    // �S�f�[�^�������X�V
                    highScore.HintCount = hintCount;
                    highScore.TouchCount = touchCount;
                    highScore.ClearTime = clearTime;
                    isUpdate = true;
                }

                // �q���g�񐔂������̏ꍇ
                else if (hintCount == highScore.HintCount)
                {
                    // �^�b�`�񐔂��r���A���Ȃ��ꍇ
                    if (touchCount < highScore.TouchCount)
                    {
                        // �^�b�`���ƃN���A�^�C���������X�V
                        highScore.TouchCount = touchCount;
                        highScore.ClearTime = clearTime;
                        isUpdate = true;
                    }

                    // �^�b�`���������̏ꍇ
                    else if (touchCount == highScore.TouchCount)
                    {
                        // �N���A�^�C����r
                        if (clearTime < highScore.ClearTime)
                        {
                            // �N���A�^�C���X�V
                            highScore.ClearTime = clearTime;
                            isUpdate = true;
                        }
                    }
                }
                // �f�[�^�̍X�V
                _scoreDatas.RemoveAt(stageNumber);
                _scoreDatas.Insert(stageNumber, highScore);
            }

            // �V�K�N���A�Ȃ�f�[�^�����̂܂ܒǉ�
            else
            {
                _scoreDatas.Add(new ScoreData(clearTime, touchCount, hintCount));
                isUpdate = true;
            }

            return isUpdate;

            /* ������
            // ���݂̃n�C�X�R�A���擾
            var highScore = GetHighScore(stageNumber);

            // �N���A�X�R�A�Ɣ�r���ėǂ�����I��
            if (CheckClearTime(stageNumber, clearTime))
            {
                highScore.ClearTime = clearTime;
            }

            if (CheckTouchCount(stageNumber, touchCount))
            {
                highScore.TouchCount = touchCount;
            }

            if (CheckHintCount(stageNumber, hintCount))
            {
                highScore.HintCount = hintCount;
            }

            if (hintCount < highScore.HintCount)

            // �X�R�A�̍X�V
            if (stageNumber < _scoreDatas.Count)
            {
                _scoreDatas.RemoveAt(stageNumber);
            }
            _scoreDatas.Insert(stageNumber, highScore);
            �����܂� */
        }

        /// <summary>
        /// �N���A�^�C���̔�r
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="clearTime"></param>
        /// <returns></returns>
        public bool CheckClearTime(int stageNumber, float clearTime)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                if (_scoreDatas[stageNumber].ClearTime <= clearTime)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �^�b�`�񐔂̔�r
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="touchCount"></param>
        /// <returns></returns>
        public bool CheckTouchCount(int stageNumber, int touchCount)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                if (_scoreDatas[stageNumber].TouchCount <= touchCount)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �q���g�񐔂̔�r
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="hintCount"></param>
        /// <returns></returns>
        public bool CheckHintCount(int stageNumber, int hintCount)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                if (_scoreDatas[stageNumber].HintCount <= hintCount)
                {
                    return false;
                }
            }
            return true;
        }
    }
}