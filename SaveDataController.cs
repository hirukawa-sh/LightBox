using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    /// <summary>
    /// �Z�[�u�f�[�^�̐���
    /// </summary>
    public class SaveDataController : MonoBehaviour
    {
        [SerializeField]
        SaveDataAccesser _saveData;

        /// <summary>
        /// �ǂݍ��ݎ��Ɏ����I�Ƀf�[�^�����[�h���邩�H
        /// </summary>
        [SerializeField]
        bool _loadOnAwake = false;

        /// <summary>
        /// �N�����Ƀf�[�^�ǂݍ���
        /// </summary>
        void Awake()
        {
            if (_loadOnAwake == true)
            {
                _saveData.Load();
            }
        }

        /// <summary>
        /// �|�[�Y�Ńf�[�^�Z�[�u
        /// </summary>
        /// <param name="pause"></param>
        void OnApplicationPause(bool pause)
        {
            // �|�[�Y�Ɣ��f���ꂽ�ꍇ�̂݃Z�[�u
            // �A�v���N������pause = false �ŌĂ΂�Ă��܂��̂ŉ��L�����ŉ��
            if (pause == true)
            {
                _saveData.Save();
            }
        }

        /// <summary>
        /// ���炩�̗��R�ŃA�v�������鎞���Z�[�u
        /// </summary>
        void OnApplicationQuit()
        {
            _saveData.Save();
        }
    }
}