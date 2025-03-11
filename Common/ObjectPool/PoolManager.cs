using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Poolable
{
    /// <summary>
    /// プールのデータ
    /// </summary>
    [System.Serializable]
    public struct PoolData
    {
        /// <summary>
        /// プールするオブジェクト
        /// </summary>
        public PoolableObject PoolableObject
        {
            get
            {
                return _poolableObject;
            }
        }

        /// <summary>
        /// プールのサイズ
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
    /// ObejectPool管理クラス
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        /// <summary>
        /// プールの自動リフレッシュを有効にするか？
        /// </summary>
        [SerializeField]
        bool _enableAutoRefresh;

        /// <summary>
        /// リフレッシュ間隔（ミリ秒）
        /// </summary>
        [SerializeField]
        float _refreshTime = 1000;

        /// <summary>
        /// 対象のオブジェクト
        /// </summary>
        [SerializeField]
        List<PoolData> _poolDatas = new List<PoolData>();

        /// <summary>
        /// Poolの配列
        /// </summary>
        Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

#region Private
        void Awake()
        {
            // Poolを作成
            foreach (var poolData in _poolDatas)
            {
                Add(poolData.PoolableObject, poolData.PoolSize);
            }
        }

        /// <summary>
        /// キー名チェック　キーは元オブジェクトの名前
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
        /// Poolから指定した名前のオブジェクトを取得
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

#region 登録
        /// <summary>
        /// Poolを追加
        /// </summary>
        /// <param name="poolableObject"></param>
        /// <param name="defaultPoolSize"></param>
        public void Add(PoolableObject poolableObject, int defaultPoolSize)
        {
            // 同名オブジェクトのプールが既にあるかチェック
            if (!CheckKeyName(poolableObject.name))
            {
                var pool = new Pool(poolableObject, transform);

                // 初期プールサイズを指定
                pool.PreloadAsync(defaultPoolSize, defaultPoolSize).Subscribe();

                // プールが大きくなりすぎないように自動リフレッシュ
                if (_enableAutoRefresh)
                {
                    pool.StartShrinkTimer(System.TimeSpan.FromMilliseconds(_refreshTime), 0, defaultPoolSize);
                }

                // このオブジェクトが破棄されたらプールも自動的に破棄する
                this.OnDestroyAsObservable().Subscribe(_ => pool.Dispose());

                _pools.Add(poolableObject.name, pool);
            }
        }

        /// <summary>
        /// Poolを削除
        /// </summary>
        public void Remove(PoolableObject poolableObject)
        {
            Pool pool;
            if (_pools.TryGetValue(poolableObject.name, out pool))
            {
                // PoolListから削除
                _pools.Remove(poolableObject.name);

                // Pool本体を破棄
                pool.Dispose();
            }
        }
#endregion

#region 操作
        /// <summary>
        /// Poolが存在するかチェック
        /// </summary>
        /// <param name="poolableObject"></param>
        /// <returns></returns>
        public bool Contains(PoolableObject poolableObject)
        {
            return CheckKeyName(poolableObject.name);
        }

        /// <summary>
        /// 生成
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
        /// 破棄
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