using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    /// <summary>
    /// セーブデータの制御
    /// </summary>
    public class SaveDataController : MonoBehaviour
    {
        [SerializeField]
        SaveDataAccesser _saveData;

        /// <summary>
        /// 読み込み時に自動的にデータをロードするか？
        /// </summary>
        [SerializeField]
        bool _loadOnAwake = false;

        /// <summary>
        /// 起動時にデータ読み込み
        /// </summary>
        void Awake()
        {
            if (_loadOnAwake == true)
            {
                _saveData.Load();
            }
        }

        /// <summary>
        /// ポーズでデータセーブ
        /// </summary>
        /// <param name="pause"></param>
        void OnApplicationPause(bool pause)
        {
            // ポーズと判断された場合のみセーブ
            // アプリ起動時にpause = false で呼ばれてしまうので下記条件で回避
            if (pause == true)
            {
                _saveData.Save();
            }
        }

        /// <summary>
        /// 何らかの理由でアプリが閉じる時もセーブ
        /// </summary>
        void OnApplicationQuit()
        {
            _saveData.Save();
        }
    }
}