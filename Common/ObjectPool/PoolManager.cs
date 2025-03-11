using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Poolable
{
    /// <summary>
    /// �v�[���̃f�[�^
    /// </summary>
    [System.Serializable]
    public struct PoolData
    {
        /// <summary>
        /// �v�[������I�u�W�F�N�g
        /// </summary>
        public PoolableObject PoolableObject
        {
            get
            {
                return _poolableObject;
            }
        }

        /// <summary>
        /// �v�[���̃T�C�Y
        /// </summary>
        public int PoolSize
        {
            get
            {
                return _poolSize;
            }
        }

        [SerializeField]
        PoolableObject _poolableObject;

        [SerializeField]
        int _poolSize;
    }

    /// <summary>
    /// ObejectPool�Ǘ��N���X
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        /// <summary>
        /// �v�[���̎������t���b�V����L���ɂ��邩�H
        /// </summary>
        [SerializeField]
        bool _enableAutoRefresh;

        /// <summary>
        /// ���t���b�V���Ԋu�i�~���b�j
        /// </summary>
        [SerializeField]
        float _refreshTime = 1000;

        /// <summary>
        /// �Ώۂ̃I�u�W�F�N�g
        /// </summary>
        [SerializeField]
        List<PoolData> _poolDatas = new List<PoolData>();

        /// <summary>
        /// Pool�̔z��
        /// </summary>
        Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

#region Private
        void Awake()
        {
            // Pool���쐬
            foreach (var poolData in _poolDatas)
            {
                Add(poolData.PoolableObject, poolData.PoolSize);
            }
        }

        /// <summary>
        /// �L�[���`�F�b�N�@�L�[�͌��I�u�W�F�N�g�̖��O
        /// </summary>
        /// <param name="originalName"></param>
        /// <returns></returns>
        bool CheckKeyName(string originalName)
        {
            if (!_pools.ContainsKey(originalName))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Pool����w�肵�����O�̃I�u�W�F�N�g���擾
        /// </summary>
        /// <param name="originalName"></param>
        /// <returns></returns>
        PoolableObject GetPoolableObject(string originalName)
        {
            if (!CheckKeyName(originalName))
            {
                Debug.LogWarning($"{this} -> {originalName} pool is not found.");
                return null;
            }
            return _pools[originalName].Rent();
        }
#endregion

#region �o�^
        /// <summary>
        /// Pool��ǉ�
        /// </summary>
        /// <param name="poolableObject"></param>
        /// <param name="defaultPoolSize"></param>
        public void Add(PoolableObject poolableObject, int defaultPoolSize)
        {
            // �����I�u�W�F�N�g�̃v�[�������ɂ��邩�`�F�b�N
            if (!CheckKeyName(poolableObject.name))
            {
                var pool = new Pool(poolableObject, transform);

                // �����v�[���T�C�Y���w��
                pool.PreloadAsync(defaultPoolSize, defaultPoolSize).Subscribe();

                // �v�[�����傫���Ȃ肷���Ȃ��悤�Ɏ������t���b�V��
                if (_enableAutoRefresh)
                {
                    pool.StartShrinkTimer(System.TimeSpan.FromMilliseconds(_refreshTime), 0, defaultPoolSize);
                }

                // ���̃I�u�W�F�N�g���j�����ꂽ��v�[���������I�ɔj������
                this.OnDestroyAsObservable().Subscribe(_ => pool.Dispose());

                _pools.Add(poolableObject.name, pool);
            }
        }

        /// <summary>
        /// Pool���폜
        /// </summary>
        public void Remove(PoolableObject poolableObject)
        {
            Pool pool;
            if (_pools.TryGetValue(poolableObject.name, out pool))
            {
                // PoolList����폜
                _pools.Remove(poolableObject.name);

                // Pool�{�̂�j��
                pool.Dispose();
            }
        }
#endregion

#region ����
        /// <summary>
        /// Pool�����݂��邩�`�F�b�N
        /// </summary>
        /// <param name="poolableObject"></param>
        /// <returns></returns>
        public bool Contains(PoolableObject poolableObject)
        {
            return CheckKeyName(poolableObject.name);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="originalName"></param>
        public void Spawn(string originalName)
        {
            Spawn(originalName, Vector3.zero, Vector3.zero, Vector3.one);
        }

        public void Spawn(string originalName, Vector3 position)
        {
            Spawn(originalName, position, Vector3.zero, Vector3.one);
        }

        public void Spawn(string originalName, Vector3 position, Vector3 rotation)
        {
            Spawn(originalName, position, rotation, Vector3.one);
        }

        public void Spawn(string originalName, Transform transform)
        {
            Spawn(originalName, transform.localPosition, transform.localEulerAngles, transform.localScale);
        }
        public void Spawn(string originalName, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            var spawnObject = GetPoolableObject(originalName);
            spawnObject.transform.localPosition = position;
            spawnObject.transform.localRotation = Quaternion.Euler(rotation);
            spawnObject.transform.localScale = scale;
        }

        /// <summary>
        /// �j��
        /// </summary>
        /// <param name="originalName"></param>
        /// <param name="targetObject"></param>
        public void Kill(string originalName, PoolableObject targetObject)
        {
            if (CheckKeyName(originalName))
            {
                _pools[originalName].Return(targetObject);
            }
        }
#endregion
    }
}