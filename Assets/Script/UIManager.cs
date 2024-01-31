using UnityEngine;
using UnityEngine.UI;
//-----------------------------
/// <summary>
/// UIManager.cs
/// UI関連の表示を行う
/// 作成日：2024年01月24日
/// 作成者：對馬礼乃
/// </summary>
//-----------------------------
public class UIManager : MonoBehaviour
{
    //スタートテキスト
    //public Text statusText;
    //コンボテキスト
    public Text comboText;
    //コンボカウント初期値
    private int comboCount = 0;

    //コンボリセット
    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText();
    }
    //コンボカウント
    public void AddCombo()
    {
        comboCount++;
        UpdateComboText();
    }

    //ピースが消えたらコンボカウント
    private void UpdateComboText()
    {
        comboText.text = string.Format("{0}combo", comboCount);
    }

    //スタート時のテキストUI
    //public void SetStatusText(string status)
    //{
    //    statusText.text = status;
    //}

    //internal void SetStatusText(string v)
    //{
    //    throw new NotImplementedException();
    //}
}
