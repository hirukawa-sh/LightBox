using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace Poolable
{
    /// <summary>
    /// ObjectPool�N���X
    /// </summary>
    public class Pool : ObjectPool<PoolableObject>
    {
        /// <summary>
        /// �v�[�������I�u�W�F�N�g
        /// </summary>
        readonly PoolableObject _originalObject;

        /// <summary>
        /// �eTransform
        /// </summary>
        Transform _parent;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="gameObjectPool"></param>
        /// <param name="parent"></param>
        public Pool(PoolableObject poolableObject, Transform parent)
        {
            _originalObject = poolableObject;
            _parent = parent;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected override PoolableObject CreateInstance()
        {
            var cloneObject = GameObject.Instantiate(_originalObject, _parent);

            // �������ꂽ�I�u�W�F�N�g������iClone)���������߂ɁA�I���W�i���̖��O�ŏ���������
            cloneObject.name = _originalObject.name;

            return cloneObject;
        }

        protected override void OnBeforeRent(PoolableObject instance)
        {
            if (Count < 1)
            {
                Debug.LogWarning($"{_originalObject} : �v�[������t�ł��B");
            }
            base.OnBeforeRent(instance);
        }
    }
}