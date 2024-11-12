using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //�t�F�[�h�A�E�g�̊J�n�t���O
    [SerializeField] bool isFadeOut_ = false;
    //�t���[��
    float frame_ = 0.0f;
    //���b��ɏI��点��
    [SerializeField] float endSecond_ = 2.0f;
    //�����_���[
    [SerializeField] Renderer renderer_;
    //�I���t���O
    bool isFinished_ = false;
    // Start is called before the first frame update
    void Start()
    {
        renderer_.material.color = Vector4.zero;
    }

    // Update is called once per frame
    void Update()
    {
        FadeOut();
    }

    /// <summary>
    /// �t�F�[�h�A�E�g
    /// </summary>
    void FadeOut()
    {
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
            renderer_.material.color = Vector4.Lerp(Vector4.zero, Vector4.one, frame_);
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
    /// �I���t���O
    /// </summary>
    /// <returns></returns>
    public bool isFinished()
    {
        return isFinished_;
    }
}
