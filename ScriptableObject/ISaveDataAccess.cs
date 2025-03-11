using System.Collections;
using UnityEngine;

namespace Game.Settings
{
    /// <summary>
    /// セーブデータアクセス用のインターフェイス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISaveDataAccess<T>
    {
        public void Set(T data);
        public void Default();
    }
}