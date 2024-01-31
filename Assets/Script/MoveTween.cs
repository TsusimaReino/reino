using System;
using UnityEngine;
//-----------------------------
/// <summary>
/// MoveTween.cs
/// 移動アニメーションクラス
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public class MoveTween : MonoBehaviour
{
    //
    public Vector3 fromPosition;
    public Vector3 toPosition;
    public float duration;

    private bool _isTween = default;
    private float elapsedTime = default;
    private Action endCallBack = default;

    /// <summary>
    /// アニメーションの更新処理
    /// </summary>
    private void Update()
    {
        //アニメーション実行しない
        if (!_isTween)
        {
            return;
        }
        // アニメーション開始時からの経過時間
        elapsedTime += Time.deltaTime;
        //
        if (elapsedTime >= duration)
        {
            // アニメーションの終了処理
            transform.position = toPosition;
            _isTween = false;
            if (endCallBack != null)
            {
                endCallBack();
            }
            Destroy(this);
            return;
        }

        var moveProgress = elapsedTime / duration;
        transform.position = Vector3.Lerp(fromPosition, toPosition, moveProgress);
    }
    /// <summary>
    /// アニメーション開始処理
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="dur"></param>
    /// <param name="endcb"></param>
    public void DoTween(Vector3 from, Vector3 to, float dur, Action endcb)
    {
        fromPosition = from;
        toPosition = to;
        duration = dur;
        endCallBack = endcb;

        transform.position = from;
        elapsedTime = 0;
        _isTween = true;
    }
}