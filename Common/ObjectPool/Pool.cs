using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace Poolable
{
    /// <summary>
    /// ObjectPoolクラス
    /// </summary>
    public class Pool : ObjectPool<PoolableObject>
    {
        /// <summary>
        /// プールされるオブジェクト
        /// </summary>
        readonly PoolableObject _originalObject;

        /// <summary>
        /// 親Transform
        /// </summary>
        Transform _parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameObjectPool"></param>
        /// <param name="parent"></param>
        public Pool(PoolableObject poolableObject, Transform parent)
        {
            _originalObject = poolableObject;
            _parent = parent;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        protected override PoolableObject CreateInstance()
        {
            var cloneObject = GameObject.Instantiate(_originalObject, _parent);

            // 生成されたオブジェクト名から（Clone)を消すために、オリジナルの名前で書き換える
            cloneObject.name = _originalObject.name;

            return cloneObject;
        }

        protected override void OnBeforeRent(PoolableObject instance)
        {
            if (Count < 1)
            {
                Debug.LogWarning($"{_originalObject} : プールが一杯です。");
            }
            base.OnBeforeRent(instance);
        }
    }
}