using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;

namespace Game.Settings
{
    /// <summary>
    /// セーブデータにアクセスしてやりとりするクラス
    /// 実値↔セーブ値の更新作業を行う
    /// </summary>
    [CreateAssetMenu(fileName = "SaveDataAccesser", menuName = "GameData/Settings/SaveDataAccesser", order = 1)]
    public class SaveDataAccesser : ScriptableObject
    {
        /// <summary>
        /// シリアライズ種類の定義
        /// </summary>
        enum SerializerType
        {
            XML,
            Json,
            Binary,
        }

        /// <summary>
        /// セーブデータをエンコードするか？
        /// </summary>
        [SerializeField]
        bool _enableEncode;

        /// <summary>
        /// エンコード用パスワード
        /// </summary>
        [SerializeField]
        string _encodingPassword;

        /// <summary>
        /// シリアライズ種類
        /// </summary>
        [SerializeField]
        SerializerType _serializerType;

        /// <summary>
        /// ゲームデータ
        /// </summary>
        [SerializeField]
        GameData _gameData;

        /// <summary>
        /// ランキングデータ
        /// </summary>
        [SerializeField]
        LeaderBoardData _LeaderBoardData;

        /// <summary>
        /// 言語設定データ
        /// </summary>
        [SerializeField]
        LanguageSettingsData _languageSettingsData;

        /// <summary>
        /// サウンド設定データ
        /// </summary>
        [SerializeField]
        SoundSettingsData _soundSettingsData;

        /// <summary>
        /// カメラ設定データ
        /// </summary>
        [SerializeField]
        CameraSettingsData _cameraSettingsData;

        /// <summary>
        /// グラフィック設定データ
        /// </summary>
        [SerializeField]
        GraphicsSettingsData _graphicsSettingsData;

        /// <summary>
        /// セーブデータの名前(ファイル名)
        /// </summary>
        const string SAVEDATA_GAMEDATA_NAME = "GameData";
        const string SAVEDATA_LEADERBORADS_NAME = "LeaderBoards";
        const string SAVEDATA_LANGUAGESETTINGS_NAME = "LanguageSettings";
        const string SAVEDATA_SOUNDSETTINGS_NAME = "SoundSettings";
        const string SAVEDATA_CAMERASETTINGS_NAME = "CameraSettings";
        const string SAVEDATA_GRAPHICSSETTINGS_NAME = "graphicsSettings";

        /// <summary>
        /// 初期化処理
        /// </summary>
        void OnEnable()
        {
            Init();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Init()
        {
            Debug.Log($"[{this}] Get Savedata Directory.");

            // ファイルの場所をログに通知
            foreach (var file in SaveGame.GetFiles())
            {
                Debug.Log(file.FullName);
            }

            // セーブのエンコードに関する設定
            if (_enableEncode)
            {
                SaveGame.Encode = _enableEncode;
                SaveGame.EncodePassword = _encodingPassword;
            }
            SaveGame.Encoder = new SaveGameSimpleEncoder();

            // シリアライズの設定
            switch (_serializerType)
            {
                case SerializerType.XML:
                    SaveGame.Serializer = new SaveGameXmlSerializer();
                    break;
                case SerializerType.Json:
                    SaveGame.Serializer = new SaveGameJsonSerializer();
                    break;
                case SerializerType.Binary:
                    SaveGame.Serializer = new SaveGameBinarySerializer();
                    break;
            }
        }

        /// <summary>
        /// 読み込まれたデータを実値にセット
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        void SetLoadData<T>(string path, ISaveDataAccess<T> data)
        {
            // ゲームデータ読み込み
            if (SaveGame.Exists(path))
            {
                var loadData = SaveGame.Load<T>(path);
                data.Set(loadData);

                Debug.Log($"{path} Load Completed.");
            }
            // 見つからなかったらエラーメッセージをログに出力、初期値をセット
            else
            {
                Debug.LogWarning($"{path} is Not Exists.");
                data.Default();
            }
        }

        /// <summary>
        /// データ保存
        /// </summary>
        public void Save()
        {
            SaveGame.Save<SavedGameData>(SAVEDATA_GAMEDATA_NAME, new SavedGameData(_gameData));
            SaveGame.Save<LeaderBoard>(SAVEDATA_LEADERBORADS_NAME, new LeaderBoard(_LeaderBoardData));
            SaveGame.Save<LanguageSettings>(SAVEDATA_LANGUAGESETTINGS_NAME, new LanguageSettings(_languageSettingsData));
            SaveGame.Save<SoundSettings>(SAVEDATA_SOUNDSETTINGS_NAME, new SoundSettings(_soundSettingsData));
            SaveGame.Save<CameraSettings>(SAVEDATA_CAMERASETTINGS_NAME, new CameraSettings(_cameraSettingsData));
            SaveGame.Save<GraphicsSettings>(SAVEDATA_GRAPHICSSETTINGS_NAME, new GraphicsSettings(_graphicsSettingsData));

            Debug.Log("Settings Save Completed.");
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public void Load()
        {
            SetLoadData(SAVEDATA_GAMEDATA_NAME, _gameData);
            SetLoadData(SAVEDATA_LEADERBORADS_NAME, _LeaderBoardData);
            SetLoadData(SAVEDATA_LANGUAGESETTINGS_NAME, _languageSettingsData);
            SetLoadData(SAVEDATA_SOUNDSETTINGS_NAME, _soundSettingsData);
            SetLoadData(SAVEDATA_CAMERASETTINGS_NAME, _cameraSettingsData);
            SetLoadData(SAVEDATA_GRAPHICSSETTINGS_NAME, _graphicsSettingsData);
        }
    }
}