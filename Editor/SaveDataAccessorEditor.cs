using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Settings
{
    /// <summary>
    /// SaveDataAccesser のエディタークラス
    /// </summary>
    [CustomEditor(typeof(SaveDataAccesser))]
    public class SaveDataAccessorEditor : Editor
    {
        // クラス本体
        SaveDataAccesser _accesser;

        // プロパティ
        SerializedProperty _gameDataProperty;
        SerializedProperty _languageSettingsProperty;
        SerializedProperty _soundSettingsProperty;
        SerializedProperty _cameraSettingsProperty;
        SerializedProperty _graphicsSettingsProperty;

        // スライダーの低値・高値
        const float SLIDER_MIN = 0;
        const float SLIDER_MAX = 100;

        void OnEnable()
        {
            // 本体の取得
            _accesser = target as SaveDataAccesser;

            // プロパティ取得
            _gameDataProperty = serializedObject.FindProperty("_gameData");
            _languageSettingsProperty = serializedObject.FindProperty("_languageSettingsData");
            _soundSettingsProperty = serializedObject.FindProperty("_soundSettingsData");
            _cameraSettingsProperty = serializedObject.FindProperty("_cameraSettingsData");
            _graphicsSettingsProperty = serializedObject.FindProperty("_graphicsSettingsData");

            Debug.Log(_cameraSettingsProperty);
        }

        /// <summary>
        /// Inspectorの描画
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // SerializedPropertyを実体に変換
            var _gameData = _gameDataProperty.objectReferenceValue as GameData;
            var _languageSettings = _languageSettingsProperty.objectReferenceValue as LanguageSettingsData;
            var _soundSettings = _soundSettingsProperty.objectReferenceValue as SoundSettingsData;
            var _cameraSettings = _cameraSettingsProperty.objectReferenceValue as CameraSettingsData;
            var _graphicsSettings = _graphicsSettingsProperty.objectReferenceValue as GraphicsSettingsData;

            serializedObject.Update();

            // Load/Saveボタン
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Load"))
                {
                    _accesser.Load();
                }

                if (GUILayout.Button("Save"))
                {
                    _accesser.Save();
                }
            }

            // Openボタン
            if (GUILayout.Button("Open Directory"))
            {
                // セーブデータのディレクトリを開く
                //EditorUtility.RevealInFinder(Application.persistentDataPath); // 謎のエクスプローラ落ちするので非推奨
                Application.OpenURL(Application.persistentDataPath);
            }

            EditorGUILayout.Space();

            // 下記パラメータ内容が変更されたか監視
            EditorGUI.BeginChangeCheck();

            // パラメータ設定

            // ゲームデータ
            EditorGUILayout.LabelField("Game Data", EditorStyles.boldLabel);

            if (_gameData != null)
            {
                _gameData.ClearedStageNumber.Value = EditorGUILayout.IntField(
                    _gameData.ClearedStageNumber.name,
                    _gameData.ClearedStageNumber.Value
                    );
            }
            else
            {
                EditorGUILayout.HelpBox("Game Data is not set.", MessageType.Warning);
            }
            EditorGUILayout.Space();

            // 言語設定
            EditorGUILayout.LabelField("Language Settings", EditorStyles.boldLabel);

            if (_languageSettings != null)
            {
                _languageSettings.LocationIndex.Value = EditorGUILayout.IntField(
                    _languageSettings.LocationIndex.name,
                    _languageSettings.LocationIndex.Value
                    );
            }
            else
            {
                EditorGUILayout.HelpBox("Language Settings is not set.", MessageType.Warning);
            }
            EditorGUILayout.Space();

            // サウンド設定
            EditorGUILayout.LabelField("Sound Settings", EditorStyles.boldLabel);

            if (_soundSettings != null)
            {
                _soundSettings.BGMVolume.Value = EditorGUILayout.Slider(
                    _soundSettings.BGMVolume.name,
                    _soundSettings.BGMVolume.Value, SLIDER_MIN, SLIDER_MAX
                    );

                _soundSettings.SEVolume.Value = EditorGUILayout.Slider(
                    _soundSettings.SEVolume.name,
                    _soundSettings.SEVolume.Value, SLIDER_MIN, SLIDER_MAX
                    );

                _soundSettings.BGMID.Value = EditorGUILayout.IntField(
                    _soundSettings.BGMID.name,
                    _soundSettings.BGMID.Value
                    );
            }
            else
            {
                EditorGUILayout.HelpBox("Sound Settings is not set.", MessageType.Warning);
            }
            EditorGUILayout.Space();

            // カメラ設定
            EditorGUILayout.LabelField("Camera Settings", EditorStyles.boldLabel);

            if (_cameraSettings != null)
            {
                _cameraSettings.InverseCameraX.Value = EditorGUILayout.Toggle(
                    _cameraSettings.InverseCameraX.name,
                    _cameraSettings.InverseCameraX.Value
                    );

                _cameraSettings.InverseCameraY.Value = EditorGUILayout.Toggle(
                    _cameraSettings.InverseCameraY.name,
                    _cameraSettings.InverseCameraY.Value
                    );
            }
            else
            {
                EditorGUILayout.HelpBox("Camera Settings is not set.", MessageType.Warning);
            }
            EditorGUILayout.Space();

            // グラフィックス設定
            EditorGUILayout.LabelField("Graphics Settings", EditorStyles.boldLabel);

            if (_graphicsSettings != null)
            {
                _graphicsSettings.EnableBloom.Value = EditorGUILayout.Toggle(
                    _graphicsSettings.EnableBloom.name,
                    _graphicsSettings.EnableBloom.Value
                    );

                _graphicsSettings.BloomColor.Value = EditorGUILayout.ColorField(
                    _graphicsSettings.BloomColor.name,
                    _graphicsSettings.BloomColor.Value
                    );

                _graphicsSettings.BackgroundColor.Value = EditorGUILayout.ColorField(
                    _graphicsSettings.BackgroundColor.name,
                    _graphicsSettings.BackgroundColor.Value
                    );

                _graphicsSettings.EnableDepthOfField.Value = EditorGUILayout.Toggle(
                    _graphicsSettings.EnableDepthOfField.name,
                    _graphicsSettings.EnableDepthOfField.Value
                    );

                _graphicsSettings.FocusDistance.Value = EditorGUILayout.Slider(
                    _graphicsSettings.FocusDistance.name,
                    _graphicsSettings.FocusDistance.Value, SLIDER_MIN, SLIDER_MAX
                    );

                _graphicsSettings.Aperture.Value = EditorGUILayout.Slider(
                    _graphicsSettings.Aperture.name,
                    _graphicsSettings.Aperture.Value, SLIDER_MIN, SLIDER_MAX
                    );

                _graphicsSettings.FocalLength.Value = EditorGUILayout.Slider(
                    _graphicsSettings.FocalLength.name,
                    _graphicsSettings.FocalLength.Value, SLIDER_MIN, SLIDER_MAX
                    );
            }
            else
            {
                EditorGUILayout.HelpBox("Graphics Settings is not set.", MessageType.Warning);
            }

            // 設定ここまで

            // パラメータに変更があった場合、編集した内容をScriptableObjectに保存する
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_cameraSettings);
                EditorUtility.SetDirty(_graphicsSettings);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}