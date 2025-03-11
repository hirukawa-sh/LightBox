using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    /// <summary>
    /// UIカメラ制御
    /// </summary>
    public class UICameraController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // シーン内に1個だけ存在するようにする
            // UICameraControllerが既に存在しているなら自身を削除する
            if (GameObject.FindObjectsOfType(typeof(UICameraController)).Length > 1)
            {
                Destroy(gameObject);
            }

            // EventSystemがシーン内に存在しないなら生成
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