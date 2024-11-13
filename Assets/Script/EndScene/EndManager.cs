using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    //スコア関係
    [SerializeField, Header("ScoreUISettings")]
    //スコア表示用テキスト
    private ScoreText scoreText_;
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
            isAddScore_ = true;
        }
        else
        {
            isAddScore_ = false;
        }
        if (isAddScore_)
        {
            if (scoreAddFrame_ < scoreAddInterval_)
            {
                scoreAddFrame_ += Time.deltaTime / scoreAddInterval_;
            }
            else
            {
                AddScore(100);
                scoreAddFrame_ = 0.0f;
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
