using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poolable;

namespace Manager
{
    /// <summary>
    /// �G�t�F�N�g�Ǘ��N���X
    /// </summary>
    [RequireComponent(typeof(PoolManager))]
    public class EffectManager : MonoBehaviour
    {
        /// <summary>
        /// �v�[���̃T�C�Y
        /// </summary>
        [SerializeField]
        int _poolSize = 5;

        /// <summary>
        /// ���C�g�؂�ւ��G�t�F�N�g
        /// </summary>
        [SerializeField]
        ParticleSystem _changeLightEffect;

        PoolManager _poolManager;

        void Awake()
        {
            _poolManager = GetComponent<PoolManager>();

            // �p�[�e�B�N����PoolableObject�R���|�[�l���g���擾
            var poolable = _changeLightEffect.GetComponent<PoolableObject>();
            if (poolable == null)
            {
                // �R���|�[�l���g���Ȃ���Βǉ�����
                poolable = _changeLightEffect.gameObject.AddComponent<PoolableObject>();
            }

            // �v�[���ɒǉ�
            _poolManager.Add(poolable, _poolSize);
        }

        /// <summary>
        /// ���C�g�؂�ւ����̃G�t�F�N�g����
        /// </summary>
        /// <param name="position"></param>
        public void OnChangeLightEffect(Vector3 position)
        {
            Debug.Log($"OnChangeLightEffect -> {position}");
            _poolManager.Spawn(_changeLightEffect.name, position);
        }
    }
}
