using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    /// <summary>
    /// UI�̃A�j���[�V���������{
    /// Animation�R���|�[�l���g�ɂčĐ�����
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class UIAnimation : MonoBehaviour
    {
        /// <summary>
        /// UI�A�j���̃^�C�v
        /// </summary>
        public enum UIAnimationType
        {
            Open,       // �J��
            Close,      // ����
        }

        /// <summary>
        /// Open�A�j��
        /// </summary>
        [SerializeField]
        AnimationClip _openAnimeClip;

        /// <summary>
        /// Close�A�j��
        /// </summary>
        [SerializeField]
        AnimationClip _closeAnimeClip;

        /// <summary>
        /// Animation�R���|�[�l���g
        /// </summary>
        Animation _anime;

        const string OPENANIME_NAME = "ANIME_OPEN";
        const string CLOSEANIME_NAME = "ANIME_CLOSE";

        // Start is called before the first frame update
        void Awake()
        {
            _anime = GetComponent<Animation>();
            _anime.playAutomatically = false;   // �A�j���̏����Đ����I�t

            // AnimationClip�̓o�^
            _anime.AddClip(_openAnimeClip, OPENANIME_NAME);
            _anime.AddClip(_closeAnimeClip, CLOSEANIME_NAME);
        }

        /// <summary>
        /// �A�j���Đ�
        /// </summary>
        public IEnumerator Play(UIAnimationType animeType)
        {
            // ���ݍĐ����̃A�j�����~
            if (_anime.isPlaying)
            {
                _anime.Stop();
            }

            // �A�j���[�V�����̎�ނ��ƂɍĐ�
            switch (animeType)
            {
                case UIAnimationType.Open:
                    _anime.Play(OPENANIME_NAME);
                    break;

                case UIAnimationType.Close:
                    _anime.Play(CLOSEANIME_NAME);
                    break;
            }

            // �A�j���Đ��I���܂őҋ@
            yield return new WaitWhile(() => _anime.isPlaying);
        }
    }
}