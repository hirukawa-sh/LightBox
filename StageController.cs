using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// �X�e�[�W����
    /// </summary>
    public class StageController : MonoBehaviour
    {
        /// <summary>
        /// �X�e�[�W�N���A�񍐗p�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent onStageClearEvent;

        /// <summary>
        /// ���C�g�{�b�N�X�̃��X�g
        /// </summary>
        List<LightBoxController> _lightBoxsList;

        /// <summary>
        /// �N���A�ςݔ���t���O
        /// </summary>
        public bool IsCleared
        {
            get
            {
                return _isCleared;
            }
        }
        bool _isCleared = false;

        void Awake()
        {
            // ���g�̎q�ɂȂ��Ă��郉�C�g�{�b�N�X���ׂĂ��擾
            _lightBoxsList = new List<LightBoxController>(GetComponentsInChildren<LightBoxController>());
        }

        /// <summary>
        /// �X�e�[�W�N���A�`�F�b�N
        /// </summary>
        public void CheckStageClear()
        {
            // �_������LightBox�𐔂���
            var lightOnBoxCount = _lightBoxsList.Where(x => x.LightFlag == true).Count();

            // �_�����`�F�b�N�@���ׂē_���������H
            if (_lightBoxsList.Count == lightOnBoxCount)
            {
                // �N���A�ς݃t���O�I��
                _isCleared = true;

                // �X�e�[�W�N���A�C�x���g���s
                onStageClearEvent.Invoke();
            }
        }

        /// <summary>
        /// �q���g���̏���
        /// </summary>
        public async UniTask HintAction()
        {
            // �_�����Ă��Ȃ�Box��T��
            var box = _lightBoxsList.Where(x => x.LightFlag == false).FirstOrDefault();

            if (box != null)
            {
                await box.BlinkLight();

                // �X�e�[�W�N���A�`�F�b�N���s
                CheckStageClear();
            }
        }
    }
}
