using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //カメラの初期位置
    Vector3 firstPos_ = Vector3.zero;

    //シェイクを開始フラグ
    bool isShake_ = false;

    //シェイクの幅
    [SerializeField] private Vector2 randomRangeX_ = new Vector2(-0.5f, 0.5f);
    [SerializeField] private Vector2 randomRangeY_ = new Vector2(-0.5f, 0.5f);

    //シェイクの行っている時間
    [SerializeField] float shakeTime_ = 2.0f;
    float frame_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        firstPos_ = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShake_ = true;
        }
        ShakeStart();
    }

    /// <summary>
    /// シェイクのフラグのセッター
    /// </summary>
    /// <param name="isShake"></param>
    public void SetIsShake(bool isShake)
    {
        isShake_ = isShake;
    }

    /// <summary>
    /// シェイクをスタートさせる
    /// </summary>
    public void ShakeStart()
    {
        if (isShake_)
        {
            if (frame_ < shakeTime_)
            {
                frame_ += Time.deltaTime / shakeTime_;
            }
            else
            {
                isShake_ = false;
            }
            Vector3 randomPos;
            randomPos = new Vector3(
                Random.Range(randomRangeX_.x, randomRangeX_.y),
                Random.Range(-0.5f, 0.5f),
                -10.0f
            );
            transform.position = randomPos;
        }
        else
        {
            transform.position = firstPos_;
            frame_ = 0.0f;
        }
    }
}