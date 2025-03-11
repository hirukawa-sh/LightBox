using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    /// <summary>
    /// UI�J��������
    /// </summary>
    public class UICameraController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // �V�[������1�������݂���悤�ɂ���
            // UICameraController�����ɑ��݂��Ă���Ȃ玩�g���폜����
            if (GameObject.FindObjectsOfType(typeof(UICameraController)).Length > 1)
            {
                Destroy(gameObject);
            }

            // EventSystem���V�[�����ɑ��݂��Ȃ��Ȃ琶��
            if (GameObject.FindObjectOfType(typeof(EventSystem)) == null)
            {
                var eventSystemObject = new GameObject("EventSystem");
                eventSystemObject.AddComponent<EventSystem>();
                eventSystemObject.AddComponent<StandaloneInputModule>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}