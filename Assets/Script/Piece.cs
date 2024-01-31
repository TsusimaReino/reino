using UnityEngine;
using UnityEngine.UI;
//-----------------------------
/// <summary>
/// Piece.cs
/// ピースクラス
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public class Piece : MonoBehaviour
{
    //消えるフラグ
    public bool _deleteFlag;
    //画像格納
    private Image thisImage;
    //ピースのサイズ
    private RectTransform thisRectTransform;
    //ピースの種類
    private PieceKind kind;
    internal bool deleteFlag;

    // 初期化処理
    private void Awake()
    {
        // アタッチされている各コンポーネントを取得
        thisImage = GetComponent<Image>();
        thisRectTransform = GetComponent<RectTransform>();

        // フラグを初期化
        _deleteFlag = false;
    }
    // ピースの種類とそれに応じた色をセットする
    public void SetKind(PieceKind pieceKind)
    {
        kind = pieceKind;
        BallColor();
    }

    // ピースの種類を返す
    public PieceKind GetKind()
    {
        return kind;
    }

    // ピースのサイズをセットする
    public void SetSize(int size)
    {
        this.thisRectTransform.sizeDelta = Vector2.one * size;
    }

    //ピースの透過を設定する
    public void SetPieceAlpha(float alpha)
    {
        var col = thisImage.color;
        col.a = alpha;
        thisImage.color = col;
    }

    //ピースの色を自身の種類の物に変える
    private void BallColor()
    {
        switch (kind)
        {
            case PieceKind.Red:
                thisImage.color = Color.red;
                break;
            case PieceKind.Blue:
                thisImage.color = Color.blue;
                break;
            case PieceKind.Green:
                thisImage.color = Color.green;
                break;
            case PieceKind.Yellow:
                thisImage.color = Color.yellow;
                break;
            case PieceKind.Black:
                thisImage.color = Color.black;
                break;
            case PieceKind.Magenta:
                thisImage.color = Color.magenta;
                break;
            default:
                break;
        }
    }
}