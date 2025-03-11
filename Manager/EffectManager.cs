using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poolable;

namespace Manager
{
    /// <summary>
    /// エフェクト管理クラス
    /// </summary>
    [RequireComponent(typeof(PoolManager))]
    public class EffectManager : MonoBehaviour
    {
        /// <summary>
        /// プールのサイズ
        /// </summary>
        [SerializeField]
        int _poolSize = 5;

        /// <summary>
        /// ライト切り替えエフェクト
        /// </summary>
        [SerializeField]
        ParticleSystem _changeLightEffect;

        PoolManager _poolManager;

        void Awake()
        {
            _poolManager = GetComponent<PoolManager>();

            // パーティクルのPoolableObjectコンポーネントを取得
            var poolable = _changeLightEffect.GetComponent<PoolableObject>();
            if (poolable == null)
            {
                // コンポーネントがなければ追加する
                poolable = _changeLightEffect.gameObject.AddComponent<PoolableObject>();
            }

            // プールに追加
            _poolManager.Add(poolable, _poolSize);
        }

        /// <summary>
        /// ライト切り替え時のエフェクト発生
        /// </summary>
        /// <param name="position"></param>
        public void OnChangeLightEffect(Vector3 position)
        {
            Debug.Log($"OnChangeLightEffect -> {position}");
            _poolManager.Spawn(_changeLightEffect.name, position);
        }
    }
}
