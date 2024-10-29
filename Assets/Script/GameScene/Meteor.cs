using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    /// <summary>
    /// 最低落下速度
    /// </summary>
    [SerializeField] private float fallSpeedMin_ = 1;

    /// <summary>
    /// 最高落下速度
    /// </summary>
    [SerializeField] private float fallSpeedMax_ = 3;

    /// <summary>
    /// 爆発のプレハブ。生成元から受け取る
    /// </summary>
    private List<Explosion> explosionPrefabs_;
    private Explosion explosionPrefab_;

    /// <summary>
    /// 地面コライダー。生成元から受け取る
    /// </summary>
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManager gameManager_;

    /// <summary>
    /// スコアエフェクトのプレハブ
    /// </summary>
    [SerializeField] private ScoreEffect scoreEffectPrefab_;

    /// <summary>
    /// 死んだとき
    /// </summary>
    private Renderer renderer_;
    private float beginAlpha = 1.0f;//初期のalpha値
    private float endAlpha = 0.0f;//目標のalpha値
    float frame_ = 0.0f;//フレーム値
    bool isDead_ = false;//死んだかどうかのフラグ
    [SerializeField] private float maxLifeTimer_ = 1;
    [SerializeField] private float scaleUpTimer_ = 0;
    [SerializeField] Vector3 maxScale_ = new Vector3(3, 3, 3);
    private bool isScaleUpFinished_ = false;
    private ParticleSystem particleSystem_;
    // Start is called before the first frame update
    void Start()
    {
        //レンダラーの初期化
        renderer_ = GetComponent<Renderer>();
        //リジットボディの初期化
        rb_ = GetComponent<Rigidbody2D>();
        SetupVelocity();
        //スケールアップタイマーにマックスライフタイマーを代入
        scaleUpTimer_ = maxLifeTimer_;
    }
    /// <summary>
    /// 生成元から必要な情報を引き継ぐ
    /// </summary>
    public void Setup(BoxCollider2D ground, GameManager gameManager, List<Explosion> explosionPrefab)
    {
        gameManager_ = gameManager;
        groundCollider_ = ground;       
        explosionPrefabs_ = explosionPrefab;
    }

    /// <summary>
    /// 移動量の設定
    /// </summary>
    private void SetupVelocity()
    {
        //地面の上下左右の位置を取得
        float left = groundCollider_.bounds.center.x - groundCollider_.bounds.size.x / 2f;
        float right = groundCollider_.bounds.center.x + groundCollider_.bounds.size.x / 2f;
        float top = groundCollider_.bounds.center.y + groundCollider_.bounds.size.y / 2f;
        float down = groundCollider_.bounds.center.y - groundCollider_.bounds.size.y / 2f;

        float targetX = Mathf.Lerp(left, right, Random.Range(0.0f, 1.0f));
        Vector3 target = new Vector3(targetX, top, 0.0f);
        Vector3 directioin = (target - transform.position).normalized;
        float fallSpeed = Random.Range(fallSpeedMin_, fallSpeedMax_);
        rb_.velocity = directioin * fallSpeed;
    }

    /// <summary>
    /// 爆発
    /// </summary>
    private void Explosion(Explosion otherExplosion)
    {
        //連鎖数と取得と加算
        int chainNum = otherExplosion.chainNum + 1;
        //加算するスコア(連鎖数に応じた)
        int score = chainNum * 100;
        ScoreEffect scoreEffect = Instantiate(
            scoreEffectPrefab_,
            transform.position,
            Quaternion.identity
            );
        scoreEffect.SetScore(score);
        //gameManagerにscoreを加算を通知
        gameManager_.AddScore(score);
        //爆発を生成し
        Explosion explosion = Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        //生成したExplosionに連鎖数を設定
        explosion.chainNum = chainNum;
        //自身を自滅させる
        Destroy(gameObject);

    }

    /// <summary>
    /// 地面に落下
    /// </summary>
    private void Fall()
    {
        //GameManagerにダメージを通知
        gameManager_.Damage(1);
        //自身を自滅
        isDead_ = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explosion explosion;
        if (collision.gameObject.CompareTag("Explosion") &&
            collision.TryGetComponent(out explosion))
        {
            explosionPrefab_ = explosionPrefabs_[0];
            Explosion(explosion);
        }else if (collision.gameObject.CompareTag("ClusterExplosion") &&
            collision.TryGetComponent(out explosion))
        {
            explosionPrefab_ = explosionPrefabs_[1];
            Explosion(explosion);
        }
        else if (collision.gameObject.CompareTag("GiganticExplosion") &&
            collision.TryGetComponent(out explosion))
        {
            explosionPrefab_ = explosionPrefabs_[2];
            Explosion(explosion);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Fall();
        }
    }

    private void Blend()
    {
        frame_ += Time.deltaTime * 3;
        Color color = renderer_.material.color;
        color.a = Mathf.Lerp(beginAlpha, endAlpha, frame_);
        renderer_.material.color = color;
        if (renderer_.material.color.a == endAlpha)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ScaleUp()
    {
        if (particleSystem_ != null){
            particleSystem_.Stop();
        }
        rb_.velocity = Vector2.zero;
        transform.localScale = maxScale_ * (1.0f - scaleUpTimer_ / maxLifeTimer_);
        if (
            transform.localScale.x >= maxScale_.x&&
            transform.localScale.y >= maxScale_.y
            )
        {
            isScaleUpFinished_ = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead_)
        {
            scaleUpTimer_ -= Time.deltaTime / 2.0f;
            ScaleUp();
            if (isScaleUpFinished_)
            {
                Blend();

            }
        }
    }
}
