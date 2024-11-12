using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider2D))]
public abstract class ItemBase : MonoBehaviour
{
    //移動速度。派生クラスでも使えるようにprotected
    [SerializeField]
    protected float speed_ = 1;
    float theta_ = 0;
    Vector3 center_ = Vector3.zero;
    Vector3 firstPos_ = Vector3.zero;
    //  画面のサイズの確認用
    protected Camera camera_;
    //自機のサイズ確認用
    protected Collider2D collider_;
    //初期化  
    private void Awake()
    {
        camera_ = Camera.main;
        collider_ = GetComponent<Collider2D>();
        firstPos_ = transform.position;
        center_ = firstPos_;
    }
    //更新処理
    protected virtual void Update()
    {
        //移動処理
        center_ += new Vector3(speed_ * Time.deltaTime, 0.0f, 0.0f);
        theta_ += 1.0f * Time.deltaTime;
        transform.position = LissajousCurve(new Vector3(theta_ * 3.0f + Mathf.PI / 6.0f, theta_ *4.0f, 1.0f), center_, new Vector3(2.0f, 2.0f, 1.0f));
        gameObject.transform.localEulerAngles += new Vector3(0.0f, 0.0f, 1.0f);
        //画面外の確認
        //ワールド座標上のカメラ右端をカメラから算出
        float worldScreenRight = camera_.orthographicSize * camera_.aspect;
        //アイテムの当たり判定のサイズ
        float boundsSize = collider_.bounds.size.x;
        //当たり判定含め完全に画面外に出ていたらDestroy
        if (transform.position.x > worldScreenRight + boundsSize)
        {
            Destroy(gameObject);
        }
    }
    //衝突判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion")) { Get(); }
    }
    //アイテム取得の処理の抽象メソッド。派生クラスで実装が必須。
    //基底クラスでは実装しないので、最後が();であることに注意。
    public abstract void Get();

    //リサージュ曲線
    Vector3 LissajousCurve(Vector3 theta, Vector3 center, Vector3 scalar)
    {
        Vector3 result = Vector3.zero;
        result.x = scalar.x * Mathf.Sin(theta.x) + center.x;
        result.y = scalar.y * Mathf.Sin(theta.y) + center.y;
        result.z = scalar.z * Mathf.Sin(theta.z) + center.z;
        return result;
    }
}
