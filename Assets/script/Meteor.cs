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

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        SetupVelocity();
    }
    /// <summary>
    /// 生成元から必要な情報を引き継ぐ
    /// </summary>
    public void Setup(BoxCollider2D ground, GameManager gameManager, Explosion explosionPrefab)
    {
        gameManager_ = gameManager;
        groundCollider_ = ground;
        explosionPrefab_ = explosionPrefab;
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explosion explosion;
        if (collision.gameObject.CompareTag("Explosion") &&
            collision.TryGetComponent(out explosion))
        {
            Explosion(explosion);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Fall();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
