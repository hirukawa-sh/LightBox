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
        /// プレビュー用イメージ
        /// </summary>
        [SerializeField]
        Image _previewColor;

        /// <summary>
        /// Vスライダー
        /// </summary>
        [SerializeField]
        Slider _valueSlider;

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
                return new Color(V, V, V);
            }
            set
            {
                V = value.grayscale;
            }
        }

        /// <summary>
        /// V値
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
            // プレビューを初期値で反映
            _previewColor.color = Value;

            // 各種スライダーの値が変更されたら通知イベントを発行する
            _valueSlider.onValueChanged.AddListener(_ =>
            {
                if (onValueChanged != null)
                {
                    // 現在の色を送信
                    onValueChanged.Invoke(Value);

                    // プレビュー画像の色を変更
                    _previewColor.color = Value;
                }
            });
        }
    }
}