using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Slider�̒l�𔽉f����Text
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class SliderValueText : MonoBehaviour
    {
        [SerializeField]
        Slider _slider;

        [SerializeField]
        string _format = "{0:0}";

        Text _text;

        void Awake()
        {
            // Text�R���|�[�l���g�擾
            _text = GetComponent<Text>();

            // ������
            _text.text = string.Format(_format, _slider.value);

            // �l�̕ύX���Ď�
            _slider.onValueChanged.AddListener(value =>
                _text.text = string.Format(_format, value)
            );
        }
    }
}