using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticExplosion : ItemBase
{
    //大爆発のプレハブを設定
    [SerializeField]
    private Explosion giganticExplosionPrefab_;

    //抽象クラスの実装
    public override void Get()
    {
        Instantiate(giganticExplosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
