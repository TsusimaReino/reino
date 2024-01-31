using System.Collections.Generic;
using UnityEngine;
//-----------------------------
/// <summary>
/// TweenAnimationManager.cs
/// アニメーションの管理クラス
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public class TweenAnimationManager : MonoBehaviour
{
    //アニメーション定義をanimQueueに格納
    private Queue<List<AnimData>> animQueue = new Queue<List<AnimData>>();
    //アニメーションライブラリ
    private bool _isTween = default;
    //アニメーションカウント
    private int tweenAnimationCount = default;
    //アニメーションが終わった回数
    private int endAnimCount = default;

    /// <summary>
    /// アニメーション実行処理
    /// </summary>
    private void Update()
    {
        if (_isTween)
        {   //アニメーション実行
            return;
        }
        //滑らかなアニメーションカウントループ
        if (animQueue.Count > 0)
        {
            //終わったアニメーションの初期値
            endAnimCount = 0;
            //ループ内に入ったらアニメーション再生
            _isTween = true;
            var queue = animQueue.Dequeue();
            //アニメーションとキューカウントと同じ回数再生
            tweenAnimationCount = queue.Count;
            //queueの要素をdataに格納するまでループ
            foreach (var data in queue)
            {
                //ターゲットオブジェクトが動いたらアニメーションが再生される
                var tween = data.targetObject.AddComponent<MoveTween>();
                //アニメーションがターゲット
                tween.DoTween(data.targetObject.transform.position, data.targetPosition, data.duration, () =>
                {
                    //カウント数を増やす
                    endAnimCount++;
                    //アニメーションカウントと終わった回数が同じなったら実行終了
                    if (tweenAnimationCount == endAnimCount)
                    {
                        _isTween = false;
                    }
                });
            }
        }
    }
    /// <summary>
    /// アニメーションのセット処理
    /// </summary>
    /// <param name="animData">アニメーション定義保持用構造体</param>
    public void AddListAnimData(List<AnimData> animData)
    {
        animQueue.Enqueue(animData);
    }
}
