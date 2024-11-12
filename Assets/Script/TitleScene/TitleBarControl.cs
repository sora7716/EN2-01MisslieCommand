using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleBarControl : MonoBehaviour
{
    /// <summary>
    /// 最初の位置
    /// </summary>
    [SerializeField] Vector3 beginPos_;

    /// <summary>
    /// 目標の位置
    /// </summary>
    [SerializeField] Vector3 endPos_;

    /// <summary>
    /// フレーム
    /// </summary>
    float frame_;
    [SerializeField] float endSecond_ = 2.0f;

    /// <summary>
    /// レクトポス
    /// </summary>
    [SerializeField] RectTransform rectTransform_;

    /// <summary>
    /// フラグ
    /// </summary>
    bool isStart_ = true;//目標のところに行く
    bool isRevers_ = false;//元の位置に戻す

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //時間を加算
        if (frame_ < endSecond_ / 2.0f && isStart_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        else
        {
            isStart_ = false;
        }

        //戻すときの時間を計算
        if (frame_ < endSecond_ / 2.0f && isRevers_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        else
        {
            isRevers_ = false;//元の位置に戻すフラグを終了
        }

        //移動させる
        if (isStart_)
        {
            //線形補間
            rectTransform_.transform.localPosition = Vector3.Lerp(beginPos_, endPos_, EaseOutBounce(frame_));
        }
        //戻すときの移動
        else if (isRevers_)
        {
            rectTransform_.transform.localPosition = Vector3.Lerp(endPos_, beginPos_, EaseOutExpo(frame_));
        }
    }

    // バウンスのイージング関数
    float EaseOutBounce(float x)
    {
        const float n1 = 7.5625f; // 定数 n1
        const float d1 = 2.75f;    // 定数 d1

        // xがd1の1/1より小さい場合
        if (x < 1 / d1)
        {
            return n1 * x * x; // 初期バウンス
        }
        // xがd1の2/1より小さい場合
        else if (x < 2 / d1)
        {
            // xを1.5/d1減少させ、バウンス計算
            return n1 * (x -= 1.5f / d1) * x + 0.75f; // 中間バウンス
        }
        // xがd1の2.5/1より小さい場合
        else if (x < 2.5 / d1)
        {
            // xを2.25/d1減少させ、バウンス計算
            return n1 * (x -= 2.25f / d1) * x + 0.9375f; // 中間バウンス
        }
        else
        {
            // xを2.625/d1減少させ、バウンス計算
            return n1 * (x -= 2.625f / d1) * x + 0.984375f; // 最終バウンス
        }
    }

    /// <summary>
    /// イーズなんとか
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float EaseOutExpo(float x)
    {
        // xの範囲が0から1に収まるように調整
        x = Mathf.Clamp01(x);

        // もし x が 1 に達したら、1 を返す
        return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }

    /// <summary>
    /// 目標の位置のいくフラグのゲッター
    /// </summary>
    /// <returns></returns>
    public bool IsStart()
    {
        return isStart_;
    }

    /// <summary>
    /// 元に戻すフラグのセッター
    /// </summary>
    /// <param name="isRevers">戻るフラグ</param>
    public void SetIsRevers(bool isRevers)
    {
        isRevers_ = isRevers;
    }

    /// <summary>
    /// 元に戻すフラグのゲッター
    /// </summary>
    /// <returns></returns>
    public bool IsRevers()
    {
        return isRevers_;
    }

    /// <summary>
    /// 目標の位置に行くフラグのセッター
    /// </summary>
    /// <param name="isStart"></param>
    public void SetIsStart(bool isStart)
    {
        isStart_ = isStart; 
    }

    /// <summary>
    /// フレームの初期化
    /// </summary>
    public void ResetFrame()
    {
        frame_ = 0.0f;
    }
}
