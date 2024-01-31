using UnityEngine;
//-----------------------------
/// <summary>
/// AnimData.cs
/// アニメーション定義保持用構造体
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public struct AnimData
{   //ターゲットオブジェクト
    public GameObject targetObject;
    //ターゲットの位置
    public Vector3 targetPosition;
    //アニメーションが切り替わるのに要する時間
    public float duration;
    //ターゲットの場所と切り替え時間を定義する
    public AnimData(GameObject target, Vector3 pos, float dur)
    {
        //ターゲットオブジェクト
        targetObject = target;
        //ターゲットの場所
        targetPosition = pos;
        //アニメーションが切り替わるのに要する時間
        duration = dur;
    }
}