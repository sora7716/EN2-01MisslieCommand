using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStartControl : MonoBehaviour
{
    /// <summary>
    /// �ŏ��̈ʒu
    /// </summary>
    [SerializeField] Vector3 beginPos_;

    /// <summary>
    /// �ڕW�Ɉʒu
    /// </summary>
    [SerializeField] Vector3 endPos_;

    /// <summary>
    /// �t���[��
    /// </summary>
    float frame_ = 0.0f;
    [SerializeField] float endSecond_ = 2.0f;

    /// <summary>
    /// ���N�g�|�X
    /// </summary>
    [SerializeField] RectTransform rectTransform_;

    /// <summary>
    /// �t���O
    /// </summary>
    bool isStart_ = false;
    bool isRevers_ = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ����Z
        if (frame_ < endSecond_ && isStart_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        else
        {
            isStart_ = false;//�ڕW�̈ʒu�ɍs���t���O���I��
        }

        //�߂��Ƃ��̎��Ԃ��v�Z
        if (frame_ < endSecond_ && isRevers_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        //�ړ�������
        if (isStart_)
        {
            //���`���
            rectTransform_.transform.localPosition = Vector3.Lerp(beginPos_, endPos_, frame_);
        }
        //�߂��Ƃ��̈ړ�
        else if (isRevers_)
        {
            rectTransform_.transform.localPosition = Vector3.Lerp(endPos_, beginPos_, frame_);
        }
    }

    /// <summary>
    /// �ڕW�̈ʒu�̍s���t���O�̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public bool IsStart()
    {
        return isStart_;
    }

    /// <summary>
    /// �ڕW���̈ʒu�ɍs���t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isStart"></param>
    /// <returns></returns>
    public bool SetIsStart(bool isStart)
    {
        return isStart_ = isStart;
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
    /// �t���[���̏�����
    /// </summary>
    public void ResetFrame()
    {
        frame_ = 0.0f;
    }

    /// <summary>
    /// ���ɖ߂��t���O�̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public bool isRevers()
    {
        return isRevers_;
    }
}
