using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Settings;

namespace Game
{
    /// <summary>
    /// オーディオ制御
    /// </summary>
    public class AudioController : MonoBehaviour
    {
        /// <summary>
        /// オーディオの設定データ
        /// </summary>
        [SerializeField]
        SoundSettingsData _audioSettings;
        
        /// <summary>
        /// BGM一覧
        /// </summary>
        [SerializeField]
        AudioListData _BGMList;

        /// <summary>
        /// SE一覧
        /// </summary>
        [SerializeField]
        SEListData _SEList;

        /// <summary>
        /// 読み込み時に自動的にBGMを再生するか？
        /// </summary>
        [SerializeField]
        bool _playBGMOnAwake = false;

        /// <summary>
        /// BGMのAudioSource
        /// </summary>
        AudioSource _audioBGM;

        /// <summary>
        /// SEのAudioSource
        /// </summary>
        AudioSource _audioSE;

        // Start is called before the first frame update
        void Awake()
        {
            // AudioSorceの取得、無ければ生成する、生成は２個まで
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

            // Volumeの値を設定と同期
            _audioSettings.BGMVolume.OnUpdateValue.AddListener(x => _audioBGM.volume = x);
            _audioSettings.SEVolume.OnUpdateValue.AddListener(x => _audioSE.volume = x);

            // 初期値の設定
            _audioBGM.volume = _audioSettings.BGMVolume.Value;
            _audioSE.volume = _audioSettings.SEVolume.Value;

            // BGMの設定
            _audioBGM.loop = true;      // ループON

            if (_playBGMOnAwake == true)
            {
                // BGM開始
                PlayBGM();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// BGM再生
        /// </summary>
        public void PlayBGM()
        {
            _audioBGM.clip = _BGMList.ListData[_audioSettings.BGMID.Value];
            _audioBGM.Play();
        }

        /// <summary>
        /// 決定音SE再生
        /// </summary>
        public void PlayDesideSE()
        {
            _audioSE.PlayOneShot(_SEList.DecideSEClip);
        }

        /// <summary>
        /// キャンセル音SE再生
        /// </summary>
        public void PlayCancelSE()
        {
            _audioSE.PlayOneShot(_SEList.CancelSEClip);
        }
    }
}