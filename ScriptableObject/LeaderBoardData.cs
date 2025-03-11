using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    /// <summary>
    /// 個別データの構造体
    /// </summary>
    [System.Serializable]
    public struct ScoreData
    {
        public float ClearTime;
        public int TouchCount;
        public int HintCount;

        public ScoreData(float clearTime, int touchCount, int hintCount)
        {
            ClearTime = clearTime;
            TouchCount = touchCount;
            HintCount = hintCount;
        }

        public override string ToString()
        {
            return base.ToString() + $"({ClearTime}, {TouchCount}, {HintCount})";
        }
    }

    /// <summary>
    /// セーブ/ロード用の構造体
    /// </summary>
    [System.Serializable]
    public struct LeaderBoard
    {
        public readonly ScoreData[] ScoreDatas;

        public LeaderBoard(LeaderBoardData leaderBoardData)
        {
            ScoreDatas = leaderBoardData.ScoreDatas;
        }
    }

    /// <summary>
    /// LeaderBoardのデータ
    /// </summary>
    [CreateAssetMenu(fileName = "LeaderBoardData", menuName = "GameData/LeaderBoardData", order = 1)]
    public class LeaderBoardData : ScriptableObject, ISaveDataAccess<LeaderBoard>
    {
        /// <summary>
        /// データの配列
        /// </summary>
        [SerializeField]
        List<ScoreData> _scoreDatas = new List<ScoreData>();

        /// <summary>
        /// データの取得
        /// </summary>
        public ScoreData[] ScoreDatas
        {
            get
            {
                return _scoreDatas.ToArray();
            }
        }

        /// <summary>
        /// データ数取得
        /// </summary>
        public int Count
        {
            get
            {
                return _scoreDatas.Count;            
            }
        }

        /// <summary>
        /// 初期値を設定
        /// </summary>
        public void Default()
        {
            _scoreDatas.Clear();
        }

        /// <summary>
        /// ロード時のデータセット
        /// </summary>
        /// <param name="data"></param>
        public void Set(LeaderBoard data)
        {
            _scoreDatas.Clear();
            _scoreDatas.AddRange(data.ScoreDatas);
        }

        /// <summary>
        /// ハイスコア取得
        /// </summary>
        /// <param name="stageNumber"></param>
        public ScoreData GetHighScore(int stageNumber)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                return _scoreDatas[stageNumber];
            }

            // データが存在しない場合は空データを返す
            return new ScoreData();
        }

        /// <summary>
        /// ハイスコア更新
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="clearTime"></param>
        /// <param name="touchCount"></param>
        /// <param name="hintCount"></param>
        public bool UpdateHighScore(int stageNumber, float clearTime, int touchCount, int hintCount)
        {
            // スコア更新が行われた場合のフラグ
            var isUpdate = false;

            // ステージが新規クリア（追加）か、既存クリア（更新）かチェック
            if (stageNumber < _scoreDatas.Count)
            {
                // 既存クリアなら、ハイスコアと比較して更新されていればデータを保存

                // ハイスコア取得
                var highScore = GetHighScore(stageNumber);

                // 比較処理
                // まずヒント回数が少なかった場合
                if (hintCount < highScore.HintCount)
                {
                    // 全データを強制更新
                    highScore.HintCount = hintCount;
                    highScore.TouchCount = touchCount;
                    highScore.ClearTime = clearTime;
                    isUpdate = true;
                }

                // ヒント回数が同数の場合
                else if (hintCount == highScore.HintCount)
                {
                    // タッチ回数を比較し、少ない場合
                    if (touchCount < highScore.TouchCount)
                    {
                        // タッチ数とクリアタイムを強制更新
                        highScore.TouchCount = touchCount;
                        highScore.ClearTime = clearTime;
                        isUpdate = true;
                    }

                    // タッチ数が同数の場合
                    else if (touchCount == highScore.TouchCount)
                    {
                        // クリアタイム比較
                        if (clearTime < highScore.ClearTime)
                        {
                            // クリアタイム更新
                            highScore.ClearTime = clearTime;
                            isUpdate = true;
                        }
                    }
                }
                // データの更新
                _scoreDatas.RemoveAt(stageNumber);
                _scoreDatas.Insert(stageNumber, highScore);
            }

            // 新規クリアならデータをそのまま追加
            else
            {
                _scoreDatas.Add(new ScoreData(clearTime, touchCount, hintCount));
                isUpdate = true;
            }

            return isUpdate;

            /* 旧処理
            // 現在のハイスコアを取得
            var highScore = GetHighScore(stageNumber);

            // クリアスコアと比較して良い方を選択
            if (CheckClearTime(stageNumber, clearTime))
            {
                highScore.ClearTime = clearTime;
            }

            if (CheckTouchCount(stageNumber, touchCount))
            {
                highScore.TouchCount = touchCount;
            }

            if (CheckHintCount(stageNumber, hintCount))
            {
                highScore.HintCount = hintCount;
            }

            if (hintCount < highScore.HintCount)

            // スコアの更新
            if (stageNumber < _scoreDatas.Count)
            {
                _scoreDatas.RemoveAt(stageNumber);
            }
            _scoreDatas.Insert(stageNumber, highScore);
            ここまで */
        }

        /// <summary>
        /// クリアタイムの比較
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="clearTime"></param>
        /// <returns></returns>
        public bool CheckClearTime(int stageNumber, float clearTime)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                if (_scoreDatas[stageNumber].ClearTime <= clearTime)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// タッチ回数の比較
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="touchCount"></param>
        /// <returns></returns>
        public bool CheckTouchCount(int stageNumber, int touchCount)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                if (_scoreDatas[stageNumber].TouchCount <= touchCount)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ヒント回数の比較
        /// </summary>
        /// <param name="stageNumber"></param>
        /// <param name="hintCount"></param>
        /// <returns></returns>
        public bool CheckHintCount(int stageNumber, int hintCount)
        {
            if (stageNumber < _scoreDatas.Count)
            {
                if (_scoreDatas[stageNumber].HintCount <= hintCount)
                {
                    return false;
                }
            }
            return true;
        }
    }
}