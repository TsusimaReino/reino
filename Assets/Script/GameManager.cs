using UnityEngine;
//-----------------------------
/// <summary>
/// GameManager.cs
/// ゲーム管理クラス
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public class GameManager : MonoBehaviour
{
    //3つ以上だと消える
    public const int MachingCount = 3;
    private const float SelectedPieceAlpha = 0.5f;
    /// <summary>
    ///　ゲームステータス
    /// </summary>
    private enum GameState
    {
        Idle,　//何もない
        PieceMove,//ピース移動
        MatchCheck,//マッチングしていない
        DeletePiece,//消えたピース
        FillPiece,//ピースが落ちる
        Wait,
    }
    //ボードの変数
    [SerializeField]
    private Board board;
    //UIManagerの変数
    [SerializeField]
    private UIManager uiManager;
    //ステータスの変数
    private GameState currentState;
    //ピーススクリプトの変数
    private Piece selectedPiece;
    //ゲームオブジェクトをセレクトする
    private GameObject selectedPieceObject;

    /// <summary>
    /// ゲームの初期化処理
    /// </summary>
    private void Start()
    {
        //Application.targetFrameRate = 60;

        //ボードのサイス(横６、縦５)
        board.InitializeBoard(6, 5);

        currentState = GameState.Idle;
    }
    
    /// <summary>
    /// ゲームのメインループ
    /// </summary>
    private void Update()
    {
        
        switch (currentState)
        {
            case GameState.Idle:
                Idle();
                break;
            case GameState.PieceMove:
                PieceMove();
                break;
            case GameState.MatchCheck:
                MatchCheck();
                break;
            case GameState.DeletePiece:
                DeletePiece();
                break;
            case GameState.FillPiece:
                FillPiece();
                break;
            case GameState.Wait:
                break;
            default:
                break;
        }
        //uiManager.SetStatusText(currentState.ToString());
    }

    // プレイヤーの入力を検知し、ピースを選択状態にする
    private void Idle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiManager.ResetCombo();
            SelectPiece();
        }
    }

    // プレイヤーがピースを選択しているときの処理、入力終了を検知したら盤面のチェックの状態に移行する
    private void PieceMove()
    {
        if (Input.GetMouseButton(0))
        {
            var piece = board.GetNearestPiece(Input.mousePosition);
            if (piece != selectedPiece)
            {
                board.SwitchPiece(selectedPiece, piece);
            }
            selectedPieceObject.transform.position = Input.mousePosition + Vector3.up * 10;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectedPiece.SetPieceAlpha(1f);
            Destroy(selectedPieceObject);
            currentState = GameState.MatchCheck;
        }
    }

    // 盤面上に3つ以上マッチングしているピースがあるかどうかを判断する
    private void MatchCheck()
    {
        if (board.HasMatch())
        {
            //3つ以上で消える
            currentState = GameState.DeletePiece;
        }
        else
        {
            currentState = GameState.Idle;
        }
    }
    private void DeletePiece()// マッチングしているピースを削除する
    {
        currentState = GameState.Wait;
        StartCoroutine(board.DeleteMatchPiece(() => currentState = GameState.FillPiece));
    }
    // 盤面上のかけている部分にピースを補充する
    private void FillPiece()
    {
        currentState = GameState.Wait;
        StartCoroutine(board.FillPiece(() => currentState = GameState.MatchCheck));
    }
    // ピースを選択する処理
    private void SelectPiece()
    {
        selectedPiece = board.GetNearestPiece(Input.mousePosition);
        var piece = board.InstantiatePiece(Input.mousePosition);
        piece.SetKind(selectedPiece.GetKind());
        piece.SetSize((int)(board.pieceWidth * 1.2f));
        piece.SetPieceAlpha(SelectedPieceAlpha);
        selectedPieceObject = piece.gameObject;

        selectedPiece.SetPieceAlpha(SelectedPieceAlpha);
        currentState = GameState.PieceMove;
    }
}
