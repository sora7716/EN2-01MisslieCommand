using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingRect : MonoBehaviour
{
    /// <summary>
    /// 最初の位置
    /// </summary>
    [SerializeField] private Vector3 beginPos_;

    /// <summary>
    /// 最後の位置
    /// </summary>
    [SerializeField] private Vector3 endPos_;

    /// <summary>
    /// かかる時間
    /// </summary>
    [SerializeField] private float endSecond_;

    //フレーム
    [SerializeField] float frame_;

    //レクトトランスフォーム
    RectTransform rectTransform_;

    //目標の地点に行く
    bool isStart_ = false;

    //元の位置に戻す
    bool isRevers_ = false;

    //終了フラグ
    bool isFinished_ = false;

    // Start is called before the first frame update
    void Start()
    {
        //レクトトランスフォームを受け取る
        rectTransform_ = GetComponent<RectTransform>();
        rectTransform_.localPosition = beginPos_; // 初期位置を設定
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
    /// 目標地点に行く
    /// </summary>
    public void LerpStart()
    {
        if (isStart_)
        {
            if (frame_ < 1.0f)
            {
                frame_ += Time.deltaTime / endSecond_;//時間を加算
            }
            else
            {
                isStart_ = false;//スタートフラグをへし折る
                isFinished_ = true;//終了フラグを立てる
            }
            rectTransform_.transform.localPosition = Vector3.Lerp(beginPos_, endPos_, EaseOutBounce(frame_));//線形補間
        }

    }

    /// <summary>
    /// 元の位置に戻す
    /// </summary>
   public  void LerpRevers()
    {
        if (isRevers_)
        {
            if (frame_ < 1.0f)
            {
                frame_ += Time.deltaTime / endSecond_;//時間を加算
            }
            else
            {
                isRevers_ = false; //元に戻すフラグを初期化
                isFinished_ = true;//終了フラグを立てる
            }
            rectTransform_.transform.localPosition = Vector3.Lerp(endPos_, beginPos_, frame_);//線形補間
        }
    }

    /// <summary>
    /// スタートフラグのセッター
    /// </summary>
    /// <param name="isStart">スタートフラグ</param>
    public void SetIsStart(bool isStart)
    {
        isStart_ = isStart;
    }

    /// <summary>
    /// 元に戻すフラグのセッター
    /// </summary>
    /// <param name="isRevers">元に戻すフラグ</param>
    public void SetIsRevers(bool isRevers)
    {
        isRevers_ = isRevers;
    }

    /// <summary>
    /// 終了フラグをリセット
    /// </summary>
    public void ResetFinished()
    {
        isFinished_ = false;
    }

    /// <summary>
    /// 終了フラグのゲッター
    /// </summary>
    /// <returns>終了フラグ</returns>
    public bool IsFinished()
    {
        return isFinished_;
    }

    /// <summary>
    /// フレームのリセット
    /// </summary>
    public void ResetFrame()
    {
        frame_ = 0.0f;
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
}
