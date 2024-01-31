using System;
using UnityEngine;
using UnityEngine.UI;
//-----------------------------
/// <summary>
/// AlphaTween.cs
/// アルファアニメーションクラス
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public class AlphaTween : MonoBehaviour
{
    public Image thisImage;
    private float fromAlpha;
    private float toAlpha;
    private float duration;
    private Action endCallBack;
    private bool _isTween;
    private float elapsedTime;

    private void Awake()
    {
        thisImage = GetComponent<Image>();
    }
    /// <summary>
    /// アニメーションの更新処理
    /// </summary>
    private void Update()
    {
        if (!_isTween)
        {
            return;
        }

        // アニメーション開始時からの経過時間
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            // アニメーションの終了処理
            SetAlpha(toAlpha);
            _isTween = false;
            if (endCallBack != null)
            {
                endCallBack();
            }

            Destroy(this);
            return;
        }

        var moveProgress = elapsedTime / duration;
        SetAlpha(Mathf.Lerp(fromAlpha, toAlpha, moveProgress));
    }

    public void DoTween(float fAlpha, float tAlpha, float dur, Action eCallBack)
    {
        this.fromAlpha = fAlpha;
        this.toAlpha = tAlpha;
        this.duration = dur;
        this.endCallBack = eCallBack;

        SetAlpha(fAlpha);
        elapsedTime = 0;
        _isTween = true;
    }


    private void SetAlpha(float alpha)
    {
        var col = thisImage.color;
        col.a = alpha;
        thisImage.color = col;
    }
}
