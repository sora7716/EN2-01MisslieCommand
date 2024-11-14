using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingMissile : MonoBehaviour
{
    //ミサイルの残弾数
    [SerializeField] private List<Image> missiles_;
    //残弾数の最初の色
    [SerializeField] private Color missileBeginColor_;
    //消費したときのカラー
    [SerializeField] private Color missileUseColor_;
    //どれくらい残っているのか
    int missileCount_;
    //クールタイム
    bool isCoolTime_ = false;//発生フラグ
    float coolTimer_;//タイマー
    [SerializeField] float coolTimeInterval_ = 1.0f;//インターバル(秒)
    // Start is called before the first frame update
    void Start()
    {
        //色の初期化
        foreach (var missile in missiles_) 
        {
            missile.color = missileBeginColor_;
        }
        //ミサイルの残弾数を設定
        missileCount_ = missiles_.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        //弾がmaxじゃなかったらクールタイムが発生する
        if (missileCount_ < 4)
        {
            isCoolTime_ = true;
        }
        if (isCoolTime_)
        {
            CoolTime();
        }
    }

    /// <summary>
    /// ミサイルの残段数を減らす関数
    /// </summary>
    public void MissileShot()
    {
        //弾のカウントがゼロじゃなかったら
        if (missileCount_ >= 0)
        {
            missiles_[missileCount_].color = missileUseColor_;//色を変更
            missileCount_ -= 1;//残弾数を減らす
        }
    }

    void CoolTime()
    {
        if (coolTimer_ < coolTimeInterval_)
        {
            coolTimer_ += Time.deltaTime / coolTimeInterval_;//クールタイム
        }
        else
        {
            missileCount_ += 1;//残弾数を増やす
            missiles_[missileCount_].color = missileBeginColor_;//色を戻す
            coolTimer_ = 0;//クールタイムをリセット
            isCoolTime_ = false;//クールタイムを行うフラグをリセット
        }
    }

    /// <summary>
    /// ミサイルの残弾数のゲッター
    /// </summary>
    /// <returns></returns>
    public int GetMissileCount()
    {
        return missileCount_;
    }
}
