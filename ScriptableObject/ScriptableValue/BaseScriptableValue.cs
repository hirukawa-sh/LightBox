using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableValue
{
    /// <summary>
    /// グローバル変数のように使える ScriptableValue のインターフェース
    /// </summary>
    public abstract class BaseScriptableValue<T> : ScriptableObject
    {
        /// <summary>
        /// 読み込み時に初期化するか？
        /// </summary>
        [SerializeField]
        protected bool _loadOnInitialize;

        /// <summary>
        /// 初期値
        /// 実値と分けることで、エディタ実行時に初期値でリセットできるようにする
        /// </summary>
        [SerializeField]
        protected T _initialValue;

        /// <summary>
        /// 実値
        /// </summary>
        protected T _value;

        /// <summary>
        /// 値
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                // イベント発行
                if (OnUpdateValue != null)
                {
                    OnUpdateValue.Invoke(value);
                }
            }
        }

        /// <summary>
        /// 値更新時のイベント
        /// </summary>
        public UnityEvent<T> OnUpdateValue;

        /// <summary>
        /// 初期値を設定
        /// </summary>
        void OnEnable()
        {
            if (_loadOnInitialize)
            {
                Reset();
            }
        }

        /// <summary>
        /// 値のリセット
        /// </summary>
        public void Reset()
        {
            Value = _initialValue;
        }
    }
}