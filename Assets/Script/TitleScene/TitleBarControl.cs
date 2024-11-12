using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleBarControl : MonoBehaviour
{
    /// <summary>
    /// �ŏ��̈ʒu
    /// </summary>
    [SerializeField] Vector3 beginPos_;

    /// <summary>
    /// �ڕW�̈ʒu
    /// </summary>
    [SerializeField] Vector3 endPos_;

    /// <summary>
    /// �t���[��
    /// </summary>
    float frame_;
    [SerializeField] float endSecond_ = 2.0f;

    /// <summary>
    /// ���N�g�|�X
    /// </summary>
    [SerializeField] RectTransform rectTransform_;

    /// <summary>
    /// �t���O
    /// </summary>
    bool isStart_ = true;//�ڕW�̂Ƃ���ɍs��
    bool isRevers_ = false;//���̈ʒu�ɖ߂�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ����Z
        if (frame_ < endSecond_ / 2.0f && isStart_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        else
        {
            isStart_ = false;
        }

        //�߂��Ƃ��̎��Ԃ��v�Z
        if (frame_ < endSecond_ / 2.0f && isRevers_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        else
        {
            isRevers_ = false;//���̈ʒu�ɖ߂��t���O���I��
        }

        //�ړ�������
        if (isStart_)
        {
            //���`���
            rectTransform_.transform.localPosition = Vector3.Lerp(beginPos_, endPos_, EaseOutBounce(frame_));
        }
        //�߂��Ƃ��̈ړ�
        else if (isRevers_)
        {
            rectTransform_.transform.localPosition = Vector3.Lerp(endPos_, beginPos_, EaseOutExpo(frame_));
        }
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

    /// <summary>
    /// �C�[�Y�Ȃ�Ƃ�
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float EaseOutExpo(float x)
    {
        // x�͈̔͂�0����1�Ɏ��܂�悤�ɒ���
        x = Mathf.Clamp01(x);

        // ���� x �� 1 �ɒB������A1 ��Ԃ�
        return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }

    /// <summary>
    /// �ڕW�̈ʒu�̂����t���O�̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public bool IsStart()
    {
        return isStart_;
    }

    /// <summary>
    /// ���ɖ߂��t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isRevers">�߂�t���O</param>
    public void SetIsRevers(bool isRevers)
    {
        isRevers_ = isRevers;
    }

    /// <summary>
    /// ���ɖ߂��t���O�̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public bool IsRevers()
    {
        return isRevers_;
    }

    /// <summary>
    /// �ڕW�̈ʒu�ɍs���t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isStart"></param>
    public void SetIsStart(bool isStart)
    {
        isStart_ = isStart; 
    }

    /// <summary>
    /// �t���[���̏�����
    /// </summary>
    public void ResetFrame()
    {
        frame_ = 0.0f;
    }
}
