using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //フェードアウトの開始フラグ
    bool isFadeOut_ = false;
    //フェードインの開始フラグ
    bool isFadeIn_ = false;
    //フレーム
    float frame_ = 0.0f;
    //何秒後に終わらせる
    [SerializeField] float endSecond_ = 2.0f;
    //image
    Image image_;
    //終了フラグ
    bool isFinished_ = false;
    // Start is called before the first frame update
    void Start()
    {
        image_ = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="beginColor">最初のカラー</param>
    /// <param name="endColor">最後のカラー</param>
    public void FadeOut(Color beginColor, Color endColor)
    {

        if (isFadeOut_)
        {
            if (frame_ < endSecond_)
            {
                frame_ += Time.deltaTime / endSecond_;
                image_.color = Vector4.Lerp(beginColor, endColor, frame_);
            }
            else
            {
                isFadeOut_ = false;
                isFinished_ = true;
                frame_ = 0.0f;
            }
        }
    }

    public void FadeIn()
    {
        if (isFadeIn_)
        {
            if (frame_ < endSecond_)
            {
                frame_ += Time.deltaTime / endSecond_;
                image_.color = Vector4.Lerp(Color.black , Vector4.zero, frame_);
            }
            else
            {
                isFadeIn_ = false;
                isFinished_ = true;
                frame_ = 0.0f;
            }
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
    /// 透明にする(透明にした値を返す)
    /// </summary>
    /// <param name="color">好きな色</param>
    public Color Invisible(Color color)
    {
        color.a = 0.0f;
        image_.color = color;//イメージを透明にする
        return color;
    }

    /// <summary>
    /// フェードが終わる時間変更(秒)
    /// </summary>
    /// <param name="endSecond">終わる時間</param>
    public void SetEndSecond(float endSecond)
    {
        endSecond_ = endSecond;
    }

    /// <summary>
    /// フェード完了フラグをへし折る
    /// </summary>
    /// <param name="isFinished">完了フラグ</param>
    public void SetIsFinished(bool isFinished)
    {
        isFinished_= isFinished;
    }
}
