using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    //スコア関係
    [SerializeField, Header("ScoreUISettings")]
    //スコア表示用テキスト
    private ScoreText scoreText_;
    //スコアのシェイク
    [SerializeField] private ShakeRect shakeRect_;
    //スコア
    private int score_;
    //スコアを加算する時間(秒)
    [SerializeField] private float scoreAddInterval_ = 2;
    //スコアの加算する時間のフレーム数
    float scoreAddFrame_ = 0.0f;
    //スコアを加算するかどうかのフラグ
    bool isAddScore_ = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameScore.kScore > score_)
        {
            //加算フラグを立てる
            isAddScore_ = true;
        }
        else
        {
            //加算フラグをへし折る
            isAddScore_ = false;
        }
        //加算フラグがたったら
        if (isAddScore_)
        {
            //インターバルを設けて加算する
            if (scoreAddFrame_ < scoreAddInterval_)
            {
                scoreAddFrame_ += Time.deltaTime / scoreAddInterval_;
            }
            else
            {
                //100ずつ加算
                AddScore(100);
                //シェイクを開始フラグを立てる
                shakeRect_.SetIsShake(true);
                //シェイクを開始
                shakeRect_.ShakeStart();
                //加算のフレームをゼロに初期化
                scoreAddFrame_ = 0.0f;
            }

            if (Input.GetMouseButtonDown(0))
            {
                score_ = GameScore.kScore;
                scoreText_.UpdateScoreText(score_);
                //シェイクを開始フラグを立てる
                shakeRect_.SetIsShake(true);
                //シェイクを開始
                shakeRect_.ShakeStart();
                isAddScore_ = false;
            }
        }
    }

    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="point">加算する値</param>
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }
}
