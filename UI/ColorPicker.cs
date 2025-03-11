using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game.UI
{
    /// <summary>
    /// カラーピッカー
    /// プレビュー用のImage, R/G/Bスライダーを持つ
    /// </summary>
    public class ColorPicker : MonoBehaviour
    {
        /// <summary>
        /// プレビュー用イメージ
        /// </summary>
        [SerializeField]
        Image _previewColor;

        /// <summary>
        /// Rスライダー
        /// </summary>
        [SerializeField]
        Slider _RedSlider;

        /// <summary>
        /// Gスライダー
        /// </summary>
        [SerializeField]
        Slider _GreenSlider;

        /// <summary>
        /// Bスライダー
        /// </summary>
        [SerializeField]
        Slider _BlueSlider;

        /// <summary>
        /// 値変更イベント
        /// </summary>
        public UnityEvent<Color> onValueChanged;

        /// <summary>
        /// 現在のカラーを取得/セット
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
        /// R値
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
        /// G値
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
        /// B値
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
            // プレビューを初期値で反映
            _previewColor.color = Value;

            // 各種スライダーの値が変更されたら通知イベントを発行する
            _RedSlider.onValueChanged.AddListener(_ =>
            {
                if (onValueChanged != null)
                {
                    // 現在の色を送信
                    onValueChanged.Invoke(Value);

                    // プレビュー画像の色を変更
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