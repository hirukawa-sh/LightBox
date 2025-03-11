using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    /// <summary>
    /// UIのアニメーションを実施
    /// Animationコンポーネントにて再生する
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class UIAnimation : MonoBehaviour
    {
        /// <summary>
        /// UIアニメのタイプ
        /// </summary>
        public enum UIAnimationType
        {
            Open,       // 開く
            Close,      // 閉じる
        }

        /// <summary>
        /// Openアニメ
        /// </summary>
        [SerializeField]
        AnimationClip _openAnimeClip;

        /// <summary>
        /// Closeアニメ
        /// </summary>
        [SerializeField]
        AnimationClip _closeAnimeClip;

        /// <summary>
        /// Animationコンポーネント
        /// </summary>
        Animation _anime;

        const string OPENANIME_NAME = "ANIME_OPEN";
        const string CLOSEANIME_NAME = "ANIME_CLOSE";

        // Start is called before the first frame update
        void Awake()
        {
            _anime = GetComponent<Animation>();
            _anime.playAutomatically = false;   // アニメの初期再生をオフ

            // AnimationClipの登録
            _anime.AddClip(_openAnimeClip, OPENANIME_NAME);
            _anime.AddClip(_closeAnimeClip, CLOSEANIME_NAME);
        }

        /// <summary>
        /// アニメ再生
        /// </summary>
        public IEnumerator Play(UIAnimationType animeType)
        {
            // 現在再生中のアニメを停止
            if (_anime.isPlaying)
            {
                _anime.Stop();
            }

            // アニメーションの種類ごとに再生
            switch (animeType)
            {
                case UIAnimationType.Open:
                    _anime.Play(OPENANIME_NAME);
                    break;

                case UIAnimationType.Close:
                    _anime.Play(CLOSEANIME_NAME);
                    break;
            }

            // アニメ再生終了まで待機
            yield return new WaitWhile(() => _anime.isPlaying);
        }
    }
}