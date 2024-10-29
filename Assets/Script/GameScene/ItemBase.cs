using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ItemBase : MonoBehaviour
{
    //移動速度。派生クラスでも使えるようにprotected
    [SerializeField]
    protected float speed_ = 3;
    //  画面のサイズの確認用
    protected Camera camera_;
    //自機のサイズ確認用
    protected Collider2D collider_;
    //初期化  
    private void Awake()
    {
        camera_ = Camera.main;
        collider_ = GetComponent<Collider2D>();
    }
    //更新処理
    protected virtual void Update()
    {
        //移動処理
        transform.Translate(Vector3.right * speed_ * Time.deltaTime);

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
}
