using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game.UI
{
    /// <summary>
    /// �J���[�s�b�J�[
    /// �v���r���[�p��Image, R/G/B�X���C�_�[������
    /// </summary>
    public class ColorPicker : MonoBehaviour
    {
        /// <summary>
        /// �v���r���[�p�C���[�W
        /// </summary>
        [SerializeField]
        Image _previewColor;

        /// <summary>
        /// R�X���C�_�[
        /// </summary>
        [SerializeField]
        Slider _RedSlider;

        /// <summary>
        /// G�X���C�_�[
        /// </summary>
        [SerializeField]
        Slider _GreenSlider;

        /// <summary>
        /// B�X���C�_�[
        /// </summary>
        [SerializeField]
        Slider _BlueSlider;

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
                return new Color(R, G, B);
            }
            set
            {
                R = value.r;
                G = value.g;
                B = value.b;
            }
        }

        /// <summary>
        /// R�l
        /// </summary>
        public float R
        {
            get
            {
                return _RedSlider.value;
            }
            set
            {
                _RedSlider.value = value;
            }
        }

        /// <summary>
        /// G�l
        /// </summary>
        public float G
        {
            get
            {
                return _GreenSlider.value;
            }
            set
            {
                _GreenSlider.value = value;
            }
        }

        /// <summary>
        /// B�l
        /// </summary>
        public float B
        {
            get
            {
                return _BlueSlider.value;
            }
            set
            {
                _BlueSlider.value = value;
            }
        }

        void Start()
        {
            // �v���r���[�������l�Ŕ��f
            _previewColor.color = Value;

            // �e��X���C�_�[�̒l���ύX���ꂽ��ʒm�C�x���g�𔭍s����
            _RedSlider.onValueChanged.AddListener(_ =>
            {
                if (onValueChanged != null)
                {
                    // ���݂̐F�𑗐M
                    onValueChanged.Invoke(Value);

                    // �v���r���[�摜�̐F��ύX
                    _previewColor.color = Value;
                }
            });

            _GreenSlider.onValueChanged.AddListener(_ =>
            {
                if (onValueChanged != null)
                {
                    onValueChanged.Invoke(Value);
                    _previewColor.color = Value;
                }
            });

            _BlueSlider.onValueChanged.AddListener(_ =>
            {
                if (onValueChanged != null)
                {
                    onValueChanged.Invoke(Value);
                    _previewColor.color = Value;
                }
            });
        }
    }
}