using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Settings
{
    /// <summary>
    /// SaveDataAccesser �̃G�f�B�^�[�N���X
    /// </summary>
    [CustomEditor(typeof(SaveDataAccesser))]
    public class SaveDataAccessorEditor : Editor
    {
        // �N���X�{��
        SaveDataAccesser _accesser;

        // �v���p�e�B
        SerializedProperty _gameDataProperty;
        SerializedProperty _languageSettingsProperty;
        SerializedProperty _soundSettingsProperty;
        SerializedProperty _cameraSettingsProperty;
        SerializedProperty _graphicsSettingsProperty;

        // �X���C�_�[�̒�l�E���l
        const float SLIDER_MIN = 0;
        const float SLIDER_MAX = 100;

        void OnEnable()
        {
            // �{�̂̎擾
            _accesser = target as SaveDataAccesser;

            // �v���p�e�B�擾
            _gameDataProperty = serializedObject.FindProperty("_gameData");
            _languageSettingsProperty = serializedObject.FindProperty("_languageSettingsData");
            _soundSettingsProperty = serializedObject.FindProperty("_soundSettingsData");
            _cameraSettingsProperty = serializedObject.FindProperty("_cameraSettingsData");
            _graphicsSettingsProperty = serializedObject.FindProperty("_graphicsSettingsData");

            Debug.Log(_cameraSettingsProperty);
        }

        /// <summary>
        /// Inspector�̕`��
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // SerializedProperty�����̂ɕϊ�
            var _gameData = _gameDataProperty.objectReferenceValue as GameData;
            var _languageSettings = _languageSettingsProperty.objectReferenceValue as LanguageSettingsData;
            var _soundSettings = _soundSettingsProperty.objectReferenceValue as SoundSettingsData;
            var _cameraSettings = _cameraSettingsProperty.objectReferenceValue as CameraSettingsData;
            var _graphicsSettings = _graphicsSettingsProperty.objectReferenceValue as GraphicsSettingsData;

            serializedObject.Update();

            // Load/Save�{�^��
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

            // Open�{�^��
            if (GUILayout.Button("Open Directory"))
            {
                // �Z�[�u�f�[�^�̃f�B���N�g�����J��
                //EditorUtility.RevealInFinder(Application.persistentDataPath); // ��̃G�N�X�v���[����������̂Ŕ񐄏�
                Application.OpenURL(Application.persistentDataPath);
            }

            EditorGUILayout.Space();

            // ���L�p�����[�^���e���ύX���ꂽ���Ď�
            EditorGUI.BeginChangeCheck();

            // �p�����[�^�ݒ�

            // �Q�[���f�[�^
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

            // ����ݒ�
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

            // �T�E���h�ݒ�
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

            // �J�����ݒ�
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

            // �O���t�B�b�N�X�ݒ�
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

            // �ݒ肱���܂�

            // �p�����[�^�ɕύX���������ꍇ�A�ҏW�������e��ScriptableObject�ɕۑ�����
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_cameraSettings);
                EditorUtility.SetDirty(_graphicsSettings);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}