using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    /// <summary>
    /// 強化版トグルグループ
    /// </summary>
    [AddComponentMenu("UI/EnhancedToggleGroup", 33)]
    public class EnhancedToggleGroup : ToggleGroup
    {
        /// <summary>
        /// チェック付きトグル番号をビット値で管理するクラス
        /// </summary>
        public struct ToggleGroupFlag
        {
            /// <summary>
            /// 実値
            /// </summary>
            public int Value
            {
                get; set;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="value"></param>
            public ToggleGroupFlag(int value)
            {
                Value = value;
            }

            // int <=> ToggleGroupFlag の相互変換を可能にする implicit operator
            //  ToggleGroupFlag flag = 1;
            // のように、数値を直接代入できるようになる
            public static implicit operator int(ToggleGroupFlag flag) => flag.Value;
            public static implicit operator ToggleGroupFlag(int value) => new ToggleGroupFlag(value);
        }

        /// <summary>
        /// いずれかのトグルが切り替わった際に呼ばれる
        /// </summary>
        public UnityEvent _onAnyToggleValueChanged;

        /// <summary>
        /// グループ内のトグルに変化があった場合に呼ばれる
        /// </summary>
        public UnityEvent<ToggleGroupFlag> _onToggleGroupValueChanged;

        protected override void Start()
        {
            base.Start();

            // 子トグルの値変更を監視
            for (int i = 0; i < m_Toggles.Count; i++)
            {
                m_Toggles[i].onValueChanged.AddListener(OnAnyToggleValueChangedHandler);
            }
        }

        protected override void OnDestroy()
        {
            // 監視解除
            for (int i = 0; i < m_Toggles.Count; i++)
            {
                m_Toggles[i].onValueChanged.RemoveListener(OnAnyToggleValueChangedHandler);
            }

            base.OnDestroy();
        }

        // いずれかのトグルが切り替わると呼ばれる
        void OnAnyToggleValueChangedHandler(bool value)
        {
            if (value)
            {
                // 切り替わったことを外部に伝える
                if (_onAnyToggleValueChanged != null)
                {
                    _onAnyToggleValueChanged.Invoke();
                }
            }
        }

        /// <summary>
        /// チェック付きトグルの番号を配列で返す
        /// </summary>
        public List<int> GetActiveTogglesIndex()
        {
            List<int> togglesIndex = new List<int>();

            // トグル登録は後ろから順に行われている
            // 正しい順番に並べ替える必要がある
            // 実際の順番 [4] [3] [2] [1] [0]
            //　　　↓
            // 理想の順番 [0] [1] [2] [3] [4]

            // 一旦トグルリストのクローンを生成し、それを並べ替えて使用する
            var toggles = new List<Toggle>(m_Toggles);
            toggles.Reverse();

            // チェックの付いているトグル番号を保存
            if (AnyTogglesOn())
            {
                for (int i = 0; i < toggles.Count; i++)
                {
                    if (toggles[i].isOn)
                    {
                        togglesIndex.Add(i);
                    }
                }
            }
            return togglesIndex;
        }

        /// <summary>
        /// チェック付きトグルの番号をビット加算した数値で返す
        /// 例＞
        /// 　　[ ] [〇] [ ] [〇] [ ] 
        /// bit  0   1   2    4    8
        /// 上記の場合は 1 + 4 = 5 を返す
        /// </summary>
        /// <returns></returns>
        public int GetActiveToggleIndex()
        {
            int result = 0;

            var toggles = new List<Toggle>(m_Toggles);
            toggles.Reverse();

            if (AnyTogglesOn())
            {
                for (int i = 0; i < toggles.Count; i++)
                {
                    if (toggles[i].isOn)
                    {
                        result += 1 << i;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 指定した番号のトグルをオンにする
        /// </summary>
        /// <param name="index"></param>
        public void SetActiveToggle(int index)
        {
            // 番号を反転
            // 1を指定した場合
            // 実際の順番 [4] [3] [2] [1] [0]
            // 　　　　　　　　↑
            // なので、反転させるために(全体数 - 1) - index = 反転した番号
            // (5 - 1) - 1 = 3
            // 実際の順番 [4] [3] [2] [1] [0]
            // 　　　　　　　        　↑
            //            0   1   2   3   4
            // これで正しい番号になる
            var i = (m_Toggles.Count - 1) - index;
            m_Toggles[i].isOn = true;
        }
    }
}