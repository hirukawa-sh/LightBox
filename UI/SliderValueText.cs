using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Sliderの値を反映するText
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
            // Textコンポーネント取得
            _text = GetComponent<Text>();

            // 初期化
            _text.text = string.Format(_format, _slider.value);

            // 値の変更を監視
            _slider.onValueChanged.AddListener(value =>
                _text.text = string.Format(_format, value)
            );
        }
    }
}