using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    private bool m_isVisibleTimer = false;//������
    public Image UITime; //�Q�[�W
    public bool roop;//�J��Ԃ�
    public float countTime = 5.0f;

    /// <summary>
    /// �������Ԃ��o�ߌ�A�Q�[�W������n�߂�
    /// </summary>
    void Start()
    {
        //GetComponent<Text>().gameObject.SetActive(false);
        Invoke("DelayMethod", 3.0f);//3�b��
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