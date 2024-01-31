using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldArrayData : MonoBehaviour　　//元からある
{
    //タグリストの名前に紐づく番号　（const 入れた変数定義がが変化しない）
    const int NO_BLOCK = 0;  //ブロックなし　
    const int STATIC_BLOCK = 1;　//動かないブロック
    const int MOVE_BLOCK = 2;　//動くブロック
    const int PLAYER = 3; //プレイヤー
    const int TARGET = 4; //

    //シーンに配置するオブジェクトのルートをヒエラルキーから設定する

    [Header("配置するオブジェクトの親オブジェクトを設定")]
    [SerializeField] GameObject g_fieldRootObject;


    /*フィールドのオブジェクトリスト
    0　空欄
    1　動かないブロック
    2　動くブロック
    3　プレイヤー
    4   ターゲット 　  */

    string[] g_fieldObjectTagList =    //タグリストの作成
    {
        "","StaticBlock","MoveBlock","Player" ,"TargetPosition"
    };

    [Header("動かないブロックを設定(Tagを識別する）")]
    [SerializeField] GameObject g_staticBlock;
    [Header("動くブロックのtag設定(Tagを識別する)")]
    [SerializeField] GameObject g_moveBlock;
    [Header("プレイヤーのtag 設定(Tagを識別する)")]
    [SerializeField] GameObject g_player;
    [Header("ターゲットオブジェクトを設定（Tagを識別する）")]
    [SerializeField] GameObject g_target;

   

    int[,] g_fieldData = //フィールドデータ用の変数を定義　("[,]は二次元配列"　縦横合わせたマップを作成)
    {
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
    };

    int g_horizontalMaxCount = 0;　　//横の最大数
    int g_verticalMaxCount = 0;　　//縦の最大数


   
    public Vector2 PlayerPosition { get; set; } //プレイヤーの位置情報用にプロパティ変数を追加

    ///ターゲットデータ用の変数を定義を追加
    ///初期にg_fieldDataを複製する
    ///※フィールドデータは常に変化するが
    ///　ターゲット用のデータは動かさないことで
    ///　ターゲットにオブジェクトが重なっても動かせるようにする
    ///　クリア判定はこのターゲットデータを使う
    ///　
    int[,] g_targetData =
    {
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
    };

    
    int g_targetClearCount = 0;//ブロックがターゲットに入った数
    
    int g_targetMaxCount = 0;//ターゲットの最大数

    ///fieldRootObujectの配下にあるオブジェクトのタグを読み取り
    ///ⅹとY座標を基にfieldDataへ格納する（fieldDataは上書き削除）
    ///fieldDataはfieldData[Y，ⅹ]で紐づいている
    ///フィールド初期化に使う
    ///  <param name="fieldRootObject">フィールドオブジェクトのルートオブジェクトを設定</param>
    public void ImageToArray()
    {
        //フィールドの縦と横の最大数を取得（フィールドの大きさを取得）
        foreach (Transform fieldObject in g_fieldRootObject.transform)
        //親オブジェクト(Stage)の中にある子オブジェクトが繰り返される(foreach)

        {
            /*
             * 縦方向に関して座標の兼ね合いの上
             * 下に行くほどｙは減っていくので、ーをつけることで
             * ｙの位置を逆転させている
             */

            //オブジェクトの位置と番号リストを紐づけてる
            int col = Mathf.FloorToInt(fieldObject.position.x); 
            int row = Mathf.FloorToInt(-fieldObject.position.y);

            if (g_fieldObjectTagList[STATIC_BLOCK].Equals(fieldObject.tag))
            {
                g_fieldData[row, col] = STATIC_BLOCK;
            }
            else if (g_fieldObjectTagList[MOVE_BLOCK].Equals(fieldObject.tag))
            {
                g_fieldData[row, col] = MOVE_BLOCK;
            }
            else if (g_fieldObjectTagList[PLAYER].Equals(fieldObject.tag))
            {
                g_fieldData[row, col] = PLAYER;

                PlayerPosition = new Vector2(row, col);
            }
            else if (g_fieldObjectTagList[TARGET].Equals(fieldObject.tag))
            {
                g_fieldData[row, col] = TARGET;

                    //ターゲットの最大カウント
                    g_targetMaxCount++;
            }
            //フィールドデータをターゲット用のデータにコピーする
            g_targetData = (int[,]) g_fieldData.Clone();
        }
    }

    //フィールドのサイズを設定する
    //フィールドの初期化を使う
    public void SetFieldMaxSize()
    {
        // フィールドの縦と横の最大数を取得(フィールドの大きさを取得)
        foreach (Transform fieldObject in g_fieldRootObject.transform)
        {
            /*
            * 縦方向に関して座標の兼ね合いの上
            * 下に行くほどｙは減っていくので、ーをつけることで
            * ｙの位置を逆転させている
            */

            //オブジェクトの位置と番号リストを紐づけてる
            int positionX = Mathf.FloorToInt(fieldObject.position.x);
            int positionY = Mathf.FloorToInt(-fieldObject.position.y);

            // 横の最大数を設定する
            if (g_horizontalMaxCount < positionX) { /*子オブジェクトを上から順に検査し、
                 　　　　　　　　　　　　　　　　　　　最大の横幅と縦幅を検出*/
                g_horizontalMaxCount = positionX;
            }

            //縦の最大数を設定する
            if (g_verticalMaxCount < positionY)
            {
                g_verticalMaxCount = positionY;
            }
        }
        ///フィールド配列の初期化
        ///縦の最大数と横の最大数に＋１する
        g_fieldData = new int[g_verticalMaxCount + 1, g_horizontalMaxCount + 1];
    }


    private void Awake() ///初回起動時にだけ実行
    {
        SetFieldMaxSize();//フィールドのサイズ設定
        ImageToArray();///シーンに配置されたオブジェクトを元に配列データを生成する
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)){
            //配列を出力するテスト
            print("Field---------------------------------------");
            for (int y = 0; y <= g_verticalMaxCount; y++){
                string outPutString = "";
                for (int x = 0; x <= g_horizontalMaxCount; x++)
                {
                    outPutString += g_fieldData[y, x];
                }
                print(outPutString);
            }
            print("Filed------------------------------------------");
            print("プレイヤーポジション;" + PlayerPosition);
        }
    }


    ///フィールドオブジェクトから指定した座標のオブジェクトを取得する
    ///tagldが-1の場合すべてのタグを対象に検索する
    ///検索にヒットしない場合Nullを返す

    ///<param name="tagId">検索対象のタグを指定</param>
    ///<param name="row">縦位置</param>
    ///<param name="col">横位置</param>
    ///<returns></returns>
    public GameObject GetFieldObject(int tagId, int row, int col)
    {
        foreach (Transform fieldObject in g_fieldRootObject.transform)
        {
            if (tagId != -1 && fieldObject.tag != g_fieldObjectTagList[tagId]) 
            {
                continue;
            }
            /*
             *縦方向に関しては座標の兼ね合い上
             *下に行くほどyは減っていくのでーをつけることで
             *yの位置を逆転させている
             */
            if (fieldObject.transform.position.x == col &&
                fieldObject.transform.position.y == -row)
            {
                return fieldObject.gameObject;
            }
        }
        return null;

    }

    ///オブジェクトを移動する
    ///データを上書きするので移動できるかどうか
    ///移動可能な場合の処理を実行

    ///<param name="preRow">移動前縦情報</param>
    ///<param name="preCol">移動前横情報</param>
    ///<param name="nextRow">移動後縦情報</param>
    ///<param name="nextCol">移動後横情報</param>
    public void MoveData(int preRow, int preCol, int nextRow, int nextCol)
    {
        //オブジェクトを移動する
        GameObject moveObject =
                        GetFieldObject(g_fieldData[preRow, preCol], preRow, preCol);
        if (moveObject != null)
        {
            /*
             *縦方向に関して座標の兼ね合いの上で
             *下に行くほどｙは減っていくのでーをつけることで
             *yの位置を逆転させている
             */
            //座標情報なので最初の引数は✕
            moveObject.transform.position = new Vector2(nextCol, -nextRow);
        }
        //上書きするので要注意
        g_fieldData[nextRow, nextCol] = g_fieldData[preRow, preCol];

        //移動したら元のデータは削除
        g_fieldData[preRow, preCol] = NO_BLOCK;
    }

    ///ブロックを移動することが可能かチェックする
    ///trueの場合移動可能　 flaseの場合移動不可

    ///<param name="y">移動先Y座標</param>
    ///<param name="x">移動先X座標</param>
    ///<returns>ブロック移動の可否</returns>
    public bool BlockMoveCheck(int y, int x)
    {
        //ターゲットブロックだったら
        if(g_targetData[y,x] == TARGET)
        {
            //ターゲットクリアカウントを上げる
            g_targetClearCount++;

            return true;
        }
        return g_fieldData[y, x] == NO_BLOCK;
    }
    /// <summary>
    /// ブロックを移動する(ブロック移動チェック実施)
    /// </summary>
    /// <param name="preRow">移動前　縦</param>
    /// <param name="preCol">移動前　横</param>
    /// <param name="nextRow">移動後　縦</param>
    /// <param name="nextCol">移動後　横</param>
    public bool BlockMove(int preRow, int preCol, int nextRow, int nextCol)
    {
        //境界線外エラー
        if (nextRow < 0 || nextCol < 0 ||
                nextRow > g_verticalMaxCount || nextCol > g_horizontalMaxCount)
        {
            return false;
        }
        bool moveFlsg = BlockMoveCheck(nextRow, nextCol);

        //移動可能かチェックする
        if (moveFlsg)
        {
            //移動が可能な場合移動する
            MoveData(preRow, preCol, nextRow, nextCol);
        }
        return moveFlsg;
    }


    ///プレイヤーを移動することが可能かチェックする
    ///trueの場合移動可能　flaseは移動不可
    ///<param name="preRow">移動前　縦</param>
    ///<param name="preCol">移動前　横</param>
    ///<param name="nextRow">移動後　縦</param>
    ///<param name="nextCol">移動後　横</param>
    ///<returns>プレイヤー移動の可否</returns>
    public bool PlayerMoveCheck(int preRow, int preCol, int nextRow, int nextCol)
    {
        /* プレイヤーの移動先が動くブロックの時
        * ブロックを移動する処理を実施する
        */
        if (g_fieldData[nextRow, nextCol] == MOVE_BLOCK)
        {
            bool blockMoveFlag = BlockMove(nextRow, nextCol,
                                            nextRow + (nextRow - preRow), 
                                            nextCol + (nextCol - preCol));
            //ターゲットブロックかつ移動できるブロックだったら
            if (g_targetData[nextRow,nextCol] == TARGET && blockMoveFlag)
            {
                
                g_targetClearCount--;  //ターゲットクリアアカウントを下げる
            }
            return blockMoveFlag;
        }
        // プレイヤーの移動先が空の時移動する
        // プレイヤーの移動先がターゲットの時移動する
        if (g_fieldData[nextRow, nextCol] == NO_BLOCK ||
            g_targetData[nextRow,nextCol] == TARGET){
            return true;
        }
        return false;
    }
    //プレイヤーを移動する(プレイヤー移動チェックも実施)
    ///<param name="preRow">移動前　縦</param>
    ///<param name="preCol">移動前　横</param>
    ///<param name="nextRow">移動後　縦</param>
    ///<param name="nextCol">移動後　横</param>
    public void PlayerMove(int preRow, int preCol, int nextRow, int nextCol)
    {
        //移動可能かチェックする
        if (PlayerMoveCheck(preRow, preCol, nextRow, nextCol))
        {
            //移動が可能な場所に移動する
            MoveData(preRow, preCol, nextRow, nextCol);
            //プレイヤーの位置を更新する
            //座標情報なので最初の引数はⅹ
            PlayerPosition = new Vector2(nextRow, nextCol);
        }
    }
    //ゲームクリアの判定
    public bool GetGameClearJudgment()
    {
        //ターゲットクリア数とターゲットの最大値が一致したらゲームクリア
        if (g_targetClearCount == g_targetMaxCount)
        {
            
           Debug.Log("ゲームクリア");
            return true;
        }
        
        return false;
    }

}