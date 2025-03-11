using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// LightBoxの制御
    /// </summary>
    public class LightBoxController : MonoBehaviour
    {
        /// <summary>
        /// ライト反転フラグ（読み取り専用）
        /// </summary>
        public bool LightFlag
        {
            get
            {
                return _lightFlag;
            }
        }

        /// <summary>
        /// ライト反転フラグ
        /// </summary>
        [SerializeField]
        bool _lightFlag = true;

        /// <summary>
        /// レイのレイヤーマスク
        /// </summary>
        [SerializeField]
        LayerMask _layerMask;

        /// <summary>
        /// タッチイベント
        /// </summary>
        [SerializeField]
        UnityEvent OnLightBoxTouchedEvent;

        /// <summary>
        /// エフェクトイベント
        /// </summary>
        [SerializeField]
        UnityEvent<Vector3> OnChangeLightEffectEvent;

        // LightBox探索モジュール
        LightBoxSearch _lightBoxSearch;

        // Rendererコンポーネント
        Renderer _renderer;

        // レイの長さ
        const float RAY_DISTANCE = 2.0f;

        // 色指定
        readonly Color _lightOnColor = Color.white;
        readonly Color _lightOffColor = new Color(0.1f, 0.1f, 0.1f);

        /// <summary>
        /// ヒントアニメーション時の点滅フェード時間（秒）
        /// </summary>
        const float HINT_BLINKANIME_TIME = 0.25f;

        /// <summary>
        /// ヒントアニメーション時の点滅ループ回数
        /// </summary>
        const int HINT_BLINKANIME_LOOPCOUNT = 3;

        // Start is called before the first frame update
        private void Awake()
        {
            // コンポーネント取得
            _renderer = GetComponent<Renderer>();

            // 周囲のライトボックスを探索するモジュール作成
            _lightBoxSearch = new LightBoxSearch();

            // デフォルトカラーの設定
            SetColor();
        }

        /// <summary>
        /// 色の切り替え
        /// </summary>
        void SetColor()
        {
            // 点灯時
            if (_lightFlag)
            {
                _renderer.material.SetColor("_Color", _lightOnColor);

                // 光沢をオン
                _renderer.material.SetFloat("_Brightness", 10);
                _renderer.material.SetFloat("_Contrast", 0);
            }

            // 消灯時
            else
            {
                _renderer.material.SetColor("_Color", _lightOffColor);

                // 光沢をオフ
                _renderer.material.SetFloat("_Brightness", 0);
                _renderer.material.SetFloat("_Contrast", 1);
            }
        }

        public void OnTap()
        {
            // [ver.20211124]マルチタッチを無効にする（同時点灯バグ回避、暫定処理）
            if (Input.touchCount < 2)
            {
                // ライト点灯数カウンター
                var lightCount = 0;

                // まず自身のライトを切り替え
                lightCount += ChangeLight();

                // 続いて周囲ブロックのライト切り替え
                // 暫定処理として、レイの長さ = 固定値 × lossyScale.z で求める
                foreach (var box in _lightBoxSearch.Get(transform, RAY_DISTANCE * transform.lossyScale.z, _layerMask))
                {
                    var lightbox = box.GetComponent<LightBoxController>();
                    lightCount += lightbox.ChangeLight();
                }

                // イベント発行
                OnLightBoxTouchedEvent.Invoke();

                // エフェクト発生イベント
                OnChangeLightEffectEvent.Invoke(transform.position);
            }
        }

        /// <summary>
        /// ライト切り替え、点灯なら１、消灯なら-1を返す
        /// </summary>
        public int ChangeLight()
        {
            _lightFlag = !_lightFlag;
            SetColor();

            if (_lightFlag)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// ヒント時の点滅アニメーション
        /// </summary>
        /// <returns></returns>
        public async UniTask BlinkLight()
        {
            // 光沢をオフ
            _renderer.material.SetFloat("_Brightness", 10);
            _renderer.material.SetFloat("_Contrast", 0);

            // 色の変更
            await _renderer.material.DOColor(_lightOnColor, "_Color", HINT_BLINKANIME_TIME)

                // ループ設定
                .SetLoops(HINT_BLINKANIME_LOOPCOUNT, LoopType.Yoyo)

                // Tween実行後、実際のLightBoxの点灯フラグを書き換える
                .OnComplete(() => ChangeLight())

                // 処理終了まで待機
                .AsyncWaitForCompletion();
        }
    }
}