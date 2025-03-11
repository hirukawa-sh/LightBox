using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game.UI
{
    public class GrayscalePicker : MonoBehaviour
    {
        /// <summary>
        /// �v���r���[�p�C���[�W
        /// </summary>
        [SerializeField]
        Image _previewColor;

        /// <summary>
        /// V�X���C�_�[
        /// </summary>
        [SerializeField]
        Slider _valueSlider;

        /// <summary>
        /// �l�ύX�C�x���g
        /// </summary>
        public UnityEvent<Color> onValueChanged;

        /// <summary>
        /// ���݂̃J���[���擾/�Z�b�g
        /// </summary>
        public Color Value
        {
            get
            {
                return new Color(V, V, V);
            }
            set
            {
                V = value.grayscale;
            }
        }

        /// <summary>
        /// V�l
        /// </summary>
        public float V
        {
            get
            {
                return _valueSlider.value;
            }
            set
            {
                _valueSlider.value = value;
            }
        }

        void Start()
        {
            // �v���r���[�������l�Ŕ��f
            _previewColor.color = Value;

            // �e��X���C�_�[�̒l���ύX���ꂽ��ʒm�C�x���g�𔭍s����
            _valueSlider.onValueChanged.AddListener(_ =>
            {
                if (onValueChanged != null)
                {
                    // ���݂̐F�𑗐M
                    onValueChanged.Invoke(Value);

                    // �v���r���[�摜�̐F��ύX
                    _previewColor.color = Value;
                }
            });
        }
    }
}