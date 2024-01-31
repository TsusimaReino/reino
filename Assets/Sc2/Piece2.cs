//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
///// <summary>
///// ピースクラス
///// ピーススクリプトの複製
///// </summary>10.11.39.148
//public class Piece2 : MonoBehaviour
//{
//    public bool deleteFlag;
//    //ピースのサイズ
//    private RectTransform thisRectTransform;
//    //ピースの種類
//    [SerializeField]
//    private PieceKind kind;
//    [SerializeField]
//    public Renderer renderer;
//    [SerializeField]
//    public bool isTouch = false;
//    [SerializeField]
//    public GameResources.kind color;


//    // 初期化処理
//    private void Awake()
//    {
//        // アタッチされている各コンポーネントを取得
//        thisRectTransform = GetComponent<RectTransform>();
//        // フラグを初期化
//        deleteFlag = false;
//    }
//    // ピースの種類とそれに応じた色をセットする
//    public void SetKind(PieceKind pieceKind)
//    {
//        kind = pieceKind;
//        ChangeColor();
//    }

//    // ピースの種類を返す
//    public PieceKind GetKind()
//    {
//        return kind;
//    }

//    // ピースのサイズをセットする
//    public void SetSize(int size)
//    {
//        this.thisRectTransform.sizeDelta = Vector2.one * size;
//    }
//    public void ChangeColor()
//    {
//        switch (color)
//        {
//            case GameResources.BallColor.red:
//                GetComponent<Piece2>().renderer.material.SetColor("_Color", Color.red);
//                break;
//            case GameResources.BallColor.blue:
//                GetComponent<Piece2>().renderer.material.SetColor("_Color", Color.blue);
//                break;
//            case GameResources.BallColor.green:
//                GetComponent<Piece2>().renderer.material.SetColor("_Color", Color.green);
//                break;
//            case GameResources.BallColor.purple:
//                GetComponent<Piece2>().renderer.material.SetColor("_Color", new Color(1, 0, 1));
//                break;
//        }
//    }
//}
//}
