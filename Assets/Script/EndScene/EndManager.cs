using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //ゲームオーバー関係
    [SerializeField, Header("GameOver")] private EasingRect gameOver_;

    //シーン遷移していいかどうかのフラグ
    bool isSceneChange_ = false;
    //シーンが切り替わるまでの時間
    [SerializeField] private float sceneChangeTime_ = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //目標地点に行く線形補間を開始するフラグを立てる
        gameOver_.SetIsStart(true);
    }

    // Update is called once per frame
    void Update()
    {
        //線形補間を開始
        gameOver_.LerpStart();
        if (GameScore.kScore > score_ && gameOver_.IsFinished())
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
                isAddScore_ = false;
            }
        }
        //シェイクを開始
        shakeRect_.ShakeStart();
        //スコアがすべて加算しきったらシーンを切り替えられるようにする
        if (GameScore.kScore == score_ && gameOver_.IsFinished())
        {
            isSceneChange_ = true;//シーン切り替えOK
        }
        if (isSceneChange_)
        {
            //シーン切り替えるまでの待ち時間
            sceneChangeTime_ -= Time.deltaTime;
            if (Input.GetMouseButton(0) && sceneChangeTime_ < 0.0f)
            {
                //切り替えるまでの時間が0より小さくなったらとボダンを押したらシーンを切り替える
                SceneManager.LoadScene("TitleScene");
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
