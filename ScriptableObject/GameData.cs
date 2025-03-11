using System.Collections;
using UnityEngine;
using ScriptableValue;

namespace Game.Settings
{
    /// <summary>
    /// ゲームデータの構造体（アプリ終了後にセーブされるデータのみ）
    /// </summary>
    public struct SavedGameData
    {
        public int ClearedStageNumber;

        public SavedGameData(GameData data)
        {
            ClearedStageNumber = data.ClearedStageNumber.Value;
        }
    }

    [CreateAssetMenu(fileName = "GameData", menuName = "GameData/GameData", order = 1)]
    public class GameData : ScriptableObject, ISaveDataAccess<SavedGameData>
    {
        /// <summary>
        /// クリア済みステージ番号
        /// </summary>
        public ScriptableIntValue ClearedStageNumber;

        public void Set(SavedGameData data)
        {
            ClearedStageNumber.Value = data.ClearedStageNumber;
        }

        public void Default()
        {
            ClearedStageNumber.Reset();
        }
    }
}