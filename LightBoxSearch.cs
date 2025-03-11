using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 周囲のLightBoxを探索する
    /// </summary>
    public class LightBoxSearch
    {
        /// <summary>
        /// 探索結果を返す
        /// </summary>
        /// <param name="t">探索元のLigitBoxのTransform</param>
        /// <param name="layermask">レイヤーマスク</param>
        /// <returns></returns>
        public List<GameObject> Get(Transform t, float raydistance, int layermask)
        {
            var rayList = new List<Ray>();
            var result = new List<GameObject>();
            var hit = new RaycastHit();

            // 上下左右前後レイを用意
            rayList.Add(new Ray(t.position, t.up));
            rayList.Add(new Ray(t.position, -t.up));
            rayList.Add(new Ray(t.position, t.right));
            rayList.Add(new Ray(t.position, -t.right));
            rayList.Add(new Ray(t.position, t.forward));
            rayList.Add(new Ray(t.position, -t.forward));

            // 各方向のレイキャスト実行
            foreach (var r in rayList)
            {
                // レイを飛ばし、ヒットした物体を取得
                if (Physics.Raycast(r, out hit, raydistance, layermask))
                {
                    result.Add(hit.collider.gameObject);
                }
                /* 旧処理
                var hits = Physics.RaycastAll(r, raydistance, layermask);
                foreach (var hit in hits)
                {
                    // レイを飛ばした本人を除外
                    if (hit.transform != t)
                    {
                        result.Add(hit.collider.gameObject);
                    }
                }
                */
            }
            return result;
        }
    }
}