using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Poolable
{
    /// <summary>
    /// Pool�̃e�X�g�@���N���b�N�c�w��ʒu�ɐ����@�E�N���b�N�c�I�������I�u�W�F�N�g�̔j��
    /// </summary>
    [RequireComponent(typeof(PoolManager))]
    public class PoolTest : MonoBehaviour
    {
        /// <summary>
        /// �e�X�g�p�̃I�u�W�F�N�g��
        /// </summary>
        [SerializeField]
        string _poolableObjectName;

        PoolManager _manager;

        const float SPAWN_DISTANCE = 10.0f; 

        const int MOUSEBUTTON_LEFT = 0;
        const int MOUSEBUTTON_RIGHT = 1;

        // Start is called before the first frame update
        void Start()
        {
            _manager = GetComponent<PoolManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(MOUSEBUTTON_LEFT))
            {
                ObjectSpawn();
            }
            if (Input.GetMouseButtonDown(MOUSEBUTTON_RIGHT))
            {
                ObjectKill();
            }
        }

        /// <summary>
        /// �w��ʒu�ɐ���
        /// </summary>
        void ObjectSpawn()
        {
            var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, SPAWN_DISTANCE);
            var pos = Camera.main.ScreenToWorldPoint(screenPos);

            _manager.Spawn(_poolableObjectName, pos);
        }

        /// <summary>
        /// �I���I�u�W�F�N�g��j��
        /// </summary>
        void ObjectKill()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var poolableObject = hit.transform.GetComponent<PoolableObject>();
                if (poolableObject != null)
                {
                    _manager.Kill(_poolableObjectName, poolableObject);
                }
            }
        }
    }
}