using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //フェードアウトの開始フラグ
    [SerializeField] bool isFadeOut_ = false;
    //フレーム
    float frame_ = 0.0f;
    //何秒後に終わらせる
    [SerializeField] float endSecond_ = 2.0f;
    //レンダラー
    [SerializeField] Renderer renderer_;
    //終了フラグ
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
    /// フェードアウト
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
    /// フェードアウトのフラグのセッター
    /// </summary>
    /// <param name="isFadeOut"></param>
    public void SetIsFadeOut(bool isFadeOut)
    {
        isFadeOut_ = isFadeOut;
    }

    /// <summary>
    /// 終了フラグ
    /// </summary>
    /// <returns></returns>
    public bool isFinished()
    {
        return isFinished_;
    }
}
