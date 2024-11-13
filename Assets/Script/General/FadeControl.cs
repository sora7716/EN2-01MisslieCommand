using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //フェードアウトの開始フラグ
    [SerializeField] bool isFadeOut_ = false;
    //フェードインの開始フラグ
    [SerializeField] bool isFadeIn_ = false;
    //フレーム
    float frame_ = 0.0f;
    //何秒後に終わらせる
    [SerializeField] float endSecond_ = 2.0f;
    //image
    [SerializeField] Image image_;
    //終了フラグ
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
    /// フェードアウト
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
    /// フェードアウトのフラグのセッター
    /// </summary>
    /// <param name="isFadeOut"></param>
    public void SetIsFadeOut(bool isFadeOut)
    {
        isFadeOut_ = isFadeOut;
    }

    /// <summary>
    /// フェードインのフラグのセッター
    /// </summary>
    /// <param name="isFadeIn"></param>
    public void SetIsFadeIn(bool isFadeIn)
    {
        isFadeIn_ = isFadeIn;
    }
    /// <summary>
    /// 終了フラグ
    /// </summary>
    /// <returns></returns>
    public bool isFinished()
    {
        return isFinished_;
    }

    /// <summary>
    /// imageを透明にする
    /// </summary>
    public void Invisible()
    {
        image_.material.color = Vector4.zero;
    }
}
