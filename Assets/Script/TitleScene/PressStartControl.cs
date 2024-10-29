using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStartControl : MonoBehaviour
{
    /// <summary>
    /// 最初の位置
    /// </summary>
    [SerializeField] Vector3 beginPos_;

    /// <summary>
    /// 目標に位置
    /// </summary>
    [SerializeField] Vector3 endPos_;

    /// <summary>
    /// フレーム
    /// </summary>
    float frame_ = 0.0f;
    [SerializeField] float endSecond_ = 2.0f;

    /// <summary>
    /// レクトポス
    /// </summary>
    [SerializeField] RectTransform rectTransform_;

    /// <summary>
    /// フラグ
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
        //時間を加算
        if (frame_ < endSecond_ / 2.0f && isStart_)
        {
            frame_ += Time.deltaTime / endSecond_;
        }
        else
        {
            isStart_ = false;//目標の位置に行くフラグを終了
            if (!isRevers_)
            {
                frame_ = 0.0f;
            }
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
            rectTransform_.transform.localPosition = Vector3.Lerp(beginPos_, endPos_, frame_);
        }
        //戻すときの移動
        else if (isRevers_)
        {
            rectTransform_.transform.localPosition = Vector3.Lerp(endPos_, beginPos_, frame_);
        }
    }

    /// <summary>
    /// 目標の位置の行くフラグのゲッター
    /// </summary>
    /// <returns></returns>
    public bool IsStart()
    {
        return isStart_;
    }

    /// <summary>
    /// 目標尾の位置に行くフラグのセッター
    /// </summary>
    /// <param name="isStart"></param>
    /// <returns></returns>
    public bool SetIsStart(bool isStart)
    {
        return isStart_=isStart;
    }

    /// <summary>
    /// 元に戻すフラグのセッター
    /// </summary>
    /// <param name="isRevers">戻るフラグ</param>
    public void SetIsRevers(bool isRevers)
    {
        isRevers_ = isRevers;
    }
}
