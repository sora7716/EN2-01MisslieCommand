using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneItem : ItemBase
{
    //大爆発のプレハブを設定
    [SerializeField]
    private Explosion giganticExplosionPrefab_;

    [SerializeField]private float addLife_=3.0f;

    //抽象クラスの実装
    public override void Get()
    {
        Instantiate(giganticExplosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public float GetAddLife()
    {
        return addLife_;
    }
}
