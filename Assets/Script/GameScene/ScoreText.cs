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
       score_=score;
        UpdateScoreText();
    }

    /// <summary>
    /// テキストの更新
    /// </summary>
    public void UpdateScoreText()
    {
        //数値は8桁0詰め
        scoreText_.text = $"SCORE:{score_:000000}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
