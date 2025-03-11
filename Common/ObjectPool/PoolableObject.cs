using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Poolable
{
    /// <summary>
    /// プール対象オブジェクト
    /// </summary>
    public class PoolableObject : MonoBehaviour
    {
        PoolManager _manager;

        void Awake()
        {
            // 親オブジェクトにPoolManagerがあることを信頼して取得する
            _manager = GetComponentInParent<PoolManager>();
            if (_manager == null)
            {
                Debug.LogWarning($"{this} -> PoolManager is not found.");
            }
        }

        /// <summary>
        /// 自身を削除する
        /// </summary>
        public void Kill()
        {
            _manager.Kill(gameObject.name, this);
        }
    }
}