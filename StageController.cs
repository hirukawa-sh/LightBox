using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// ステージ制御
    /// </summary>
    public class StageController : MonoBehaviour
    {
        /// <summary>
        /// ステージクリア報告用イベント
        /// </summary>
        [SerializeField]
        UnityEvent onStageClearEvent;

        /// <summary>
        /// ライトボックスのリスト
        /// </summary>
        List<LightBoxController> _lightBoxsList;

        /// <summary>
        /// クリア済み判定フラグ
        /// </summary>
        public bool IsCleared
        {
            get
            {
                return _isCleared;
            }
        }
        bool _isCleared = false;

        void Awake()
        {
            // 自身の子になっているライトボックスすべてを取得
            _lightBoxsList = new List<LightBoxController>(GetComponentsInChildren<LightBoxController>());
        }

        /// <summary>
        /// ステージクリアチェック
        /// </summary>
        public void CheckStageClear()
        {
            // 点灯中のLightBoxを数える
            var lightOnBoxCount = _lightBoxsList.Where(x => x.LightFlag == true).Count();

            // 点灯数チェック　すべて点灯したか？
            if (_lightBoxsList.Count == lightOnBoxCount)
            {
                // クリア済みフラグオン
                _isCleared = true;

                // ステージクリアイベント発行
                onStageClearEvent.Invoke();
            }
        }

        /// <summary>
        /// ヒント時の処理
        /// </summary>
        public async UniTask HintAction()
        {
            // 点灯していないBoxを探索
            var box = _lightBoxsList.Where(x => x.LightFlag == false).FirstOrDefault();

            if (box != null)
            {
                await box.BlinkLight();

                // ステージクリアチェック実行
                CheckStageClear();
            }
        }
    }
}
