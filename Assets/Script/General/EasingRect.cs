using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingRect : MonoBehaviour
{
    /// <summary>
    /// �ŏ��̈ʒu
    /// </summary>
    [SerializeField] private Vector3 beginPos_;

    /// <summary>
    /// �Ō�̈ʒu
    /// </summary>
    [SerializeField] private Vector3 endPos_;

    /// <summary>
    /// �����鎞��
    /// </summary>
    [SerializeField] private float endSecond_;

    //�t���[��
    [SerializeField] float frame_;

    //���N�g�g�����X�t�H�[��
    RectTransform rectTransform_;

    //�ڕW�̒n�_�ɍs��
    bool isStart_ = false;

    //���̈ʒu�ɖ߂�
    bool isRevers_ = false;

    //�I���t���O
    bool isFinished_ = false;

    // Start is called before the first frame update
    void Start()
    {
        //���N�g�g�����X�t�H�[�����󂯎��
        rectTransform_ = GetComponent<RectTransform>();
        rectTransform_.localPosition = beginPos_; // �����ʒu��ݒ�
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isStart_ = true;
            frame_ = 1.0f;
        }
    }

    /// <summary>
    /// �ڕW�n�_�ɍs��
    /// </summary>
    public void LerpStart()
    {
        if (isStart_)
        {
            if (frame_ < 1.0f)
            {
                frame_ += Time.deltaTime / endSecond_;//���Ԃ����Z
            }
            else
            {
                isStart_ = false;//�X�^�[�g�t���O���ւ��܂�
                isFinished_ = true;//�I���t���O�𗧂Ă�
            }
            rectTransform_.transform.localPosition = Vector3.Lerp(beginPos_, endPos_, EaseOutBounce(frame_));//���`���
        }

    }

    /// <summary>
    /// ���̈ʒu�ɖ߂�
    /// </summary>
   public  void LerpRevers()
    {
        if (isRevers_)
        {
            if (frame_ < 1.0f)
            {
                frame_ += Time.deltaTime / endSecond_;//���Ԃ����Z
            }
            else
            {
                isRevers_ = false; //���ɖ߂��t���O��������
                isFinished_ = true;//�I���t���O�𗧂Ă�
            }
            rectTransform_.transform.localPosition = Vector3.Lerp(endPos_, beginPos_, frame_);//���`���
        }
    }

    /// <summary>
    /// �X�^�[�g�t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isStart">�X�^�[�g�t���O</param>
    public void SetIsStart(bool isStart)
    {
        isStart_ = isStart;
    }

    /// <summary>
    /// ���ɖ߂��t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isRevers">���ɖ߂��t���O</param>
    public void SetIsRevers(bool isRevers)
    {
        isRevers_ = isRevers;
    }

    /// <summary>
    /// �I���t���O�����Z�b�g
    /// </summary>
    public void ResetFinished()
    {
        isFinished_ = false;
    }

    /// <summary>
    /// �I���t���O�̃Q�b�^�[
    /// </summary>
    /// <returns>�I���t���O</returns>
    public bool IsFinished()
    {
        return isFinished_;
    }

    /// <summary>
    /// �t���[���̃��Z�b�g
    /// </summary>
    public void ResetFrame()
    {
        frame_ = 0.0f;
    }

    // �o�E���X�̃C�[�W���O�֐�
    float EaseOutBounce(float x)
    {
        const float n1 = 7.5625f; // �萔 n1
        const float d1 = 2.75f;    // �萔 d1

        // x��d1��1/1��菬�����ꍇ
        if (x < 1 / d1)
        {
            return n1 * x * x; // �����o�E���X
        }
        // x��d1��2/1��菬�����ꍇ
        else if (x < 2 / d1)
        {
            // x��1.5/d1���������A�o�E���X�v�Z
            return n1 * (x -= 1.5f / d1) * x + 0.75f; // ���ԃo�E���X
        }
        // x��d1��2.5/1��菬�����ꍇ
        else if (x < 2.5 / d1)
        {
            // x��2.25/d1���������A�o�E���X�v�Z
            return n1 * (x -= 2.25f / d1) * x + 0.9375f; // ���ԃo�E���X
        }
        else
        {
            // x��2.625/d1���������A�o�E���X�v�Z
            return n1 * (x -= 2.625f / d1) * x + 0.984375f; // �ŏI�o�E���X
        }
    }
}
