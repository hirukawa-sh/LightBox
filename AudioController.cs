using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// �I�[�f�B�I����
    /// </summary>
    public class AudioController : MonoBehaviour
    {
        /// <summary>
        /// �I�[�f�B�I�̐ݒ�f�[�^
        /// </summary>
        [SerializeField]
        SoundSettingsData _audioSettings;
        
        /// <summary>
        /// BGM�ꗗ
        /// </summary>
        [SerializeField]
        AudioListData _BGMList;

        /// <summary>
        /// SE�ꗗ
        /// </summary>
        [SerializeField]
        SEListData _SEList;

        /// <summary>
        /// �ǂݍ��ݎ��Ɏ����I��BGM���Đ����邩�H
        /// </summary>
        [SerializeField]
        bool _playBGMOnAwake = false;

        /// <summary>
        /// BGM��AudioSource
        /// </summary>
        AudioSource _audioBGM;

        /// <summary>
        /// SE��AudioSource
        /// </summary>
        AudioSource _audioSE;

        // Start is called before the first frame update
        void Awake()
        {
            // AudioSorce�̎擾�A������ΐ�������A�����͂Q�܂�
            var audios = GetComponents<AudioSource>();
            for (int i = 0; i < 2; i++)
            {
                if (i < audios.Length)
                {
                    if (i == 0)
                    {
                        _audioBGM = audios[i];
                    }
                    else
                    {
                        _audioSE = audios[i];
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        _audioBGM = gameObject.AddComponent<AudioSource>();
                    }
                    else
                    {
                        _audioSE = gameObject.AddComponent<AudioSource>();
                    }
                }
            }

            // Volume�̒l��ݒ�Ɠ���
            _audioSettings.BGMVolume.OnUpdateValue.AddListener(x => _audioBGM.volume = x);
            _audioSettings.SEVolume.OnUpdateValue.AddListener(x => _audioSE.volume = x);

            // �����l�̐ݒ�
            _audioBGM.volume = _audioSettings.BGMVolume.Value;
            _audioSE.volume = _audioSettings.SEVolume.Value;

            // BGM�̐ݒ�
            _audioBGM.loop = true;      // ���[�vON

            if (_playBGMOnAwake == true)
            {
                // BGM�J�n
                PlayBGM();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// BGM�Đ�
        /// </summary>
        public void PlayBGM()
        {
            _audioBGM.clip = _BGMList.ListData[_audioSettings.BGMID.Value];
            _audioBGM.Play();
        }

        /// <summary>
        /// ���艹SE�Đ�
        /// </summary>
        public void PlayDesideSE()
        {
            _audioSE.PlayOneShot(_SEList.DecideSEClip);
        }

        /// <summary>
        /// �L�����Z����SE�Đ�
        /// </summary>
        public void PlayCancelSE()
        {
            _audioSE.PlayOneShot(_SEList.CancelSEClip);
        }
    }
}