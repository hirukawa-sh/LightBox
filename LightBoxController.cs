using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// LightBox�̐���
    /// </summary>
    public class LightBoxController : MonoBehaviour
    {
        /// <summary>
        /// ���C�g���]�t���O�i�ǂݎ���p�j
        /// </summary>
        public bool LightFlag
        {
            get
            {
                return _lightFlag;
            }
        }

        /// <summary>
        /// ���C�g���]�t���O
        /// </summary>
        [SerializeField]
        bool _lightFlag = true;

        /// <summary>
        /// ���C�̃��C���[�}�X�N
        /// </summary>
        [SerializeField]
        LayerMask _layerMask;

        /// <summary>
        /// �^�b�`�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent OnLightBoxTouchedEvent;

        /// <summary>
        /// �G�t�F�N�g�C�x���g
        /// </summary>
        [SerializeField]
        UnityEvent<Vector3> OnChangeLightEffectEvent;

        // LightBox�T�����W���[��
        LightBoxSearch _lightBoxSearch;

        // Renderer�R���|�[�l���g
        Renderer _renderer;

        // ���C�̒���
        const float RAY_DISTANCE = 2.0f;

        // �F�w��
        readonly Color _lightOnColor = Color.white;
        readonly Color _lightOffColor = new Color(0.1f, 0.1f, 0.1f);

        /// <summary>
        /// �q���g�A�j���[�V�������̓_�Ńt�F�[�h���ԁi�b�j
        /// </summary>
        const float HINT_BLINKANIME_TIME = 0.25f;

        /// <summary>
        /// �q���g�A�j���[�V�������̓_�Ń��[�v��
        /// </summary>
        const int HINT_BLINKANIME_LOOPCOUNT = 3;

        // Start is called before the first frame update
        private void Awake()
        {
            // �R���|�[�l���g�擾
            _renderer = GetComponent<Renderer>();

            // ���͂̃��C�g�{�b�N�X��T�����郂�W���[���쐬
            _lightBoxSearch = new LightBoxSearch();

            // �f�t�H���g�J���[�̐ݒ�
            SetColor();
        }

        /// <summary>
        /// �F�̐؂�ւ�
        /// </summary>
        void SetColor()
        {
            // �_����
            if (_lightFlag)
            {
                _renderer.material.SetColor("_Color", _lightOnColor);

                // ������I��
                _renderer.material.SetFloat("_Brightness", 10);
                _renderer.material.SetFloat("_Contrast", 0);
            }

            // ������
            else
            {
                _renderer.material.SetColor("_Color", _lightOffColor);

                // ������I�t
                _renderer.material.SetFloat("_Brightness", 0);
                _renderer.material.SetFloat("_Contrast", 1);
            }
        }

        public void OnTap()
        {
            // [ver.20211124]�}���`�^�b�`�𖳌��ɂ���i�����_���o�O����A�b�菈���j
            if (Input.touchCount < 2)
            {
                // ���C�g�_�����J�E���^�[
                var lightCount = 0;

                // �܂����g�̃��C�g��؂�ւ�
                lightCount += ChangeLight();

                // �����Ď��̓u���b�N�̃��C�g�؂�ւ�
                // �b�菈���Ƃ��āA���C�̒��� = �Œ�l �~ lossyScale.z �ŋ��߂�
                foreach (var box in _lightBoxSearch.Get(transform, RAY_DISTANCE * transform.lossyScale.z, _layerMask))
                {
                    var lightbox = box.GetComponent<LightBoxController>();
                    lightCount += lightbox.ChangeLight();
                }

                // �C�x���g���s
                OnLightBoxTouchedEvent.Invoke();

                // �G�t�F�N�g�����C�x���g
                OnChangeLightEffectEvent.Invoke(transform.position);
            }
        }

        /// <summary>
        /// ���C�g�؂�ւ��A�_���Ȃ�P�A�����Ȃ�-1��Ԃ�
        /// </summary>
        public int ChangeLight()
        {
            _lightFlag = !_lightFlag;
            SetColor();

            if (_lightFlag)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// �q���g���̓_�ŃA�j���[�V����
        /// </summary>
        /// <returns></returns>
        public async UniTask BlinkLight()
        {
            // ������I�t
            _renderer.material.SetFloat("_Brightness", 10);
            _renderer.material.SetFloat("_Contrast", 0);

            // �F�̕ύX
            await _renderer.material.DOColor(_lightOnColor, "_Color", HINT_BLINKANIME_TIME)

                // ���[�v�ݒ�
                .SetLoops(HINT_BLINKANIME_LOOPCOUNT, LoopType.Yoyo)

                // Tween���s��A���ۂ�LightBox�̓_���t���O������������
                .OnComplete(() => ChangeLight())

                // �����I���܂őҋ@
                .AsyncWaitForCompletion();
        }
    }
}