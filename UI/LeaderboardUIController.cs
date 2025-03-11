using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Leaderboard��UI����
    /// </summary>
    public class LeaderboardUIController : MonoBehaviour
    {
        /// <summary>
        /// Leaderboard �� ScriptableObject
        /// </summary>
        [SerializeField]
        Game.Settings.LeaderBoardData _leaderboardData;
        
        /// <summary>
        /// LeaderboardContentUI
        /// </summary>
        [SerializeField]
        RectTransform _contents;

        /// <summary>
        /// NoDataUI
        /// </summary>
        [SerializeField]
        RectTransform _noData;

        /// <summary>
        /// �C���X�^���X������RowUI
        /// </summary>
        [SerializeField]
        LeaderboardRowUIController _leaderboardRowUIPrefabs;

        /// <summary>
        /// �X�e�[�W�ԍ��t�H�[�}�b�g
        /// </summary>
        [SerializeField]
        string _stageNumberFormat = "{0:000}";

        /// <summary>
        /// �N���A�^�C���̕\���t�H�[�}�b�g
        /// </summary>
        [SerializeField]
        string _clearTimeFormat = "{0:mm\\:ss\\.fff}";

        /// <summary>
        /// �^�b�`�񐔃t�H�[�}�b�g
        /// </summary>
        [SerializeField]
        string _touchCountFormat = "{0:000}";

        /// <summary>
        /// �q���g�񐔃t�H�[�}�b�g
        /// </summary>
        [SerializeField]
        string _hintCountFormat = "{0:000}";

        // Start is called before the first frame update
        void Start()
        {
            // �f�[�^�����݂���Ȃ烊�X�g���쐬
            if (0 < _leaderboardData.Count)
            {
                // No Data �\��������
                _noData.gameObject.SetActive(false);

                // ���X�g�\��
                CreateRows();
            }

            // �����Ȃ� No Data ��\��
            else
            {
                _noData.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// �f�[�^���X�g���쐬
        /// </summary>
        void CreateRows()
        {
            for (int i = 0; i < _leaderboardData.Count; i++)
            {
                var row = Instantiate(_leaderboardRowUIPrefabs, _contents);

                // �q���g�g�p�񐔂��[���Ȃ�m�[�q���g�A�C�R����\��
                if (_leaderboardData.ScoreDatas[i].HintCount == 0)
                {
                    row.NoHintClearIcon.enabled = true;
                }
                else
                {
                    row.NoHintClearIcon.enabled = false;
                }
                row.StageNumber = string.Format(_stageNumberFormat, i + 1);
                row.ClearTime = string.Format(_clearTimeFormat, System.TimeSpan.FromMilliseconds(_leaderboardData.ScoreDatas[i].ClearTime));
                row.TouchCount = string.Format(_touchCountFormat, _leaderboardData.ScoreDatas[i].TouchCount);
                row.HintCount = string.Format(_hintCountFormat, _leaderboardData.ScoreDatas[i].HintCount);
            }
        }
    }
}
