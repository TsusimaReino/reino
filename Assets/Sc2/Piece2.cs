//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
///// <summary>
///// �s�[�X�N���X
///// �s�[�X�X�N���v�g�̕���
///// </summary>10.11.39.148
//public class Piece2 : MonoBehaviour
//{
//    public bool deleteFlag;
//    //�s�[�X�̃T�C�Y
//    private RectTransform thisRectTransform;
//    //�s�[�X�̎��
//    [SerializeField]
//    private PieceKind kind;
//    [SerializeField]
//    public Renderer renderer;
//    [SerializeField]
//    public bool isTouch = false;
//    [SerializeField]
//    public GameResources.kind color;


//    // ����������
//    private void Awake()
//    {
//        // �A�^�b�`����Ă���e�R���|�[�l���g���擾
//        thisRectTransform = GetComponent<RectTransform>();
//        // �t���O��������
//        deleteFlag = false;
//    }
//    // �s�[�X�̎�ނƂ���ɉ������F���Z�b�g����
//    public void SetKind(PieceKind pieceKind)
//    {
//        kind = pieceKind;
//        ChangeColor();
//    }

//    // �s�[�X�̎�ނ�Ԃ�
//    public PieceKind GetKind()
//    {
//        return kind;
//    }

//    // �s�[�X�̃T�C�Y���Z�b�g����
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
