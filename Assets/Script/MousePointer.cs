using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    private bool m_isVisibleTimer = false;//たいｍ
    public Image UITime; //ゲージ
    public bool roop;//繰り返し
    public float countTime = 5.0f;

    /// <summary>
    /// 初期時間が経過後、ゲージが減り始める
    /// </summary>
    void Start()
    {
        //GetComponent<Text>().gameObject.SetActive(false);
        Invoke("DelayMethod", 3.0f);//3秒後
    }

    void Update()
    {
        if (m_isVisibleTimer)
        {
            UITime.fillAmount -= 1.0f / countTime * Time.deltaTime;
        }
    }

    void DelayMethod()
    {
        // GetComponent<Text>().gameObject.SetActive(true);
        m_isVisibleTimer = true;
    }
}