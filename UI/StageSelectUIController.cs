using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableValue;

namespace Game.UI
{
    /// <summary>
    /// �X�e�[�W�Z���N�gUI
    /// </summary>
    public class StageSelectUIController : MonoBehaviour
    {
        /// <summary>
        /// �X�e�[�W�ꗗ
        /// </summary>
        [SerializeField]
        StageListData _stageList;

        /// <summary>
        /// ���݂̃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        ScriptableIntValue _currentStageNumber;

        /// <summary>
        /// �N���A�ς݃X�e�[�W�ԍ�
        /// </summary>
        [SerializeField]
        ScriptableIntValue _cleardStageNumber;

        /// <summary>
        /// GridLayout
        /// </summary>
        [SerializeField]
        GridLayoutGroup _gridLayout;

        /// <summary>
        /// �{�^���̃v���n�u
        /// </summary>
        [SerializeField]
        Button _stageButtonPrefab;

        /// <summary>
        /// ���b�N��Ԃ̃{�^���v���n�u
        /// </summary>
        [SerializeField]
        Button _stageLockButtonPrefab;

        // Start is called before the first frame update
        void Start()
        {
            CreateButton();
        }

        /// <summary>
        /// �{�^���̍쐬
        /// </summary>
        void CreateButton()
        {
            for (int i = 0; i < _stageList.ListData.Count; i++)
            {
                // �{�^���̐���
                Button button;

                // ���B�ς݂̃X�e�[�W�����{�^���̗L����,
                // ���N���A��Ԃ̃X�e�[�W�̓��b�N����
                if (i < _cleardStageNumber.Value + 1)
                {
                    // ���B�ς݃X�e�[�W
                    button = Instantiate(_stageButtonPrefab, _gridLayout.transform);

                    // �N���b�N�����{�^���̃C���f�b�N�X��ݒ�
                    // �����͈ꎞ�ϐ��𗘗p����i�������|�C���^���Q�Ƃ���H�炵���ꎞ�ϐ��łȂ��Ɣ��f����Ȃ��j
                    var index = i;  // ���ꎞ�ϐ�
                    button.onClick.AddListener(() => OnButtonClickedHandler(index));
                }
                else
                {
                    // ���N���A�X�e�[�W�i���b�N��ԁj
                    button = Instantiate(_stageLockButtonPrefab, _gridLayout.transform);
                }

                // ���x���̐ݒ�
                var label = button.GetComponentInChildren<Text>();
                label.text = $"{i+1:000}";

            }
        }

        /// <summary>
        /// �{�^���������ꂽ���̃C�x���g
        /// </summary>
        void OnButtonClickedHandler(int index)
        {
            // �X�e�[�W�ԍ�����������
            _currentStageNumber.Value = index;
        }
    }
}