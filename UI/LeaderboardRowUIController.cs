using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Leaderboard�̊e�s�f�[�^UI
    /// </summary>
    public class LeaderboardRowUIController : MonoBehaviour
    {
        // �eText�R���|�[�l���g
        [SerializeField]
        Image _noHintClearIcon;

        [SerializeField]
        Text _stageNumberText;

        [SerializeField]
        Text _clearTimeText;

        [SerializeField]
        Text _touchCountText;

        [SerializeField]
        Text _hintCountText;

        /// <summary>
        /// �m�[�q���g�N���A�A�C�R��
        /// </summary>
        public Image NoHintClearIcon
        {
            get
            {
                return _noHintClearIcon;
            }
            set
            {
                _noHintClearIcon = value;
            }
        }

        /// <summary>
        /// �X�e�[�W�ԍ�
        /// </summary>
        public string StageNumber
        {
            get
            {
                return _stageNumberText.text;
            }
            set
            {
                _stageNumberText.text = value;
            }
        }

        /// <summary>
        /// �N���A����
        /// </summary>
        public string ClearTime
        {
            get
            {
                return _clearTimeText.text;
            }
            set
            {
                _clearTimeText.text = value;
            }
        }

        /// <summary>
        /// �^�b�`��
        /// </summary>
        public string TouchCount
        {
            get
            {
                return _touchCountText.text;
            }
            set
            {
                _touchCountText.text = value;
            }
        }

        /// <summary>
        /// �q���g��
        /// </summary>
        public string HintCount
        {
            get
            {
                return _hintCountText.text;
            }
            set
            {
                _hintCountText.text = value;
            }
        }
    }
}
