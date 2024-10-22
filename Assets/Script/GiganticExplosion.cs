using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticExplosion : ItemBase
{
    //�唚���̃v���n�u��ݒ�
    [SerializeField]
    private Explosion giganticExplosionPrefab_;

    //���ۃN���X�̎���
    public override void Get()
    {
        Instantiate(giganticExplosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
