using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    /// <summary>
    /// 表示用のスコア
    /// </summary>
    private int score_;

    /// <summary>
    /// テキスト本体
    /// </summary>
    private TMP_Text scoreText_;
    // Start is called before the first frame update
    void Start()
    {
        score_ = 0;
        scoreText_ = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// スコアの更新とテキストへの適応
    /// </summary>
    /// <param name="score">新しいスコア値</param>
    public void SetScore(int score)
    {
        score_ = score;
        UpdateScoreText(score_);
    }

    /// <summary>
    /// テキストの更新
    /// </summary>
    public void UpdateScoreText(int sciore)
    {
        //数値は8桁0詰め
        scoreText_.text = $"SCORE:{sciore:000000}";
    }

    /// <summary>
    /// スコアのセーブ
    /// </summary>
    public void SaveScore()
    {
        GameScore.kScore = score_;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public static class GameScore
{
    public static int kScore = 0;//ゲーム全体でスコアを格納
}

