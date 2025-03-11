using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Poolable
{
    /// <summary>
    /// �v�[���ΏۃI�u�W�F�N�g
    /// </summary>
    public class PoolableObject : MonoBehaviour
    {
        PoolManager _manager;

        void Awake()
        {
            // �e�I�u�W�F�N�g��PoolManager�����邱�Ƃ�M�����Ď擾����
            _manager = GetComponentInParent<PoolManager>();
            if (_manager == null)
            {
                Debug.LogWarning($"{this} -> PoolManager is not found.");
            }
        }

        /// <summary>
        /// ���g���폜����
        /// </summary>
        public void Kill()
        {
            _manager.Kill(gameObject.name, this);
        }
    }
}