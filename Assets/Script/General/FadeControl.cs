using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //�t�F�[�h�A�E�g�̊J�n�t���O
    [SerializeField] bool isFadeOut_ = false;
    //�t�F�[�h�C���̊J�n�t���O
    [SerializeField] bool isFadeIn_ = false;
    //�t���[��
    float frame_ = 0.0f;
    //���b��ɏI��点��
    [SerializeField] float endSecond_ = 2.0f;
    //image
    [SerializeField] Image image_;
    //�I���t���O
    bool isFinished_ = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �t�F�[�h�A�E�g
    /// </summary>
    public void FadeOut()
    {
        if (frame_ <= 0.0f)
        {
            image_.material.color = Vector4.zero;
        }
        if (isFadeOut_)
        {
            if (frame_ < endSecond_)
            {
                frame_ += Time.deltaTime / endSecond_;
            }
            else
            {
                isFinished_ = true;
            }
            image_.material.color = Vector4.Lerp(Vector4.zero, Vector4.one, frame_);
        }
    }

    public void Fadein()
    {
        if (frame_ <= 0.0f)
        {
            image_.material.color = Vector4.one-new Vector4(1,1,1,0);
        }
        if (isFadeIn_)
        {
            if (frame_ < endSecond_)
            {
                frame_ += Time.deltaTime / endSecond_;
            }
            else
            {
                isFinished_ = true;
            }
            image_.material.color = Vector4.Lerp(Vector4.one - new Vector4(1, 1, 1, 0), Vector4.zero, frame_);
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�̃t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isFadeOut"></param>
    public void SetIsFadeOut(bool isFadeOut)
    {
        isFadeOut_ = isFadeOut;
    }

    /// <summary>
    /// �t�F�[�h�C���̃t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isFadeIn"></param>
    public void SetIsFadeIn(bool isFadeIn)
    {
        isFadeIn_ = isFadeIn;
    }
    /// <summary>
    /// �I���t���O
    /// </summary>
    /// <returns></returns>
    public bool isFinished()
    {
        return isFinished_;
    }

    /// <summary>
    /// image�𓧖��ɂ���
    /// </summary>
    public void Invisible()
    {
        image_.material.color = Vector4.zero;
    }
}
