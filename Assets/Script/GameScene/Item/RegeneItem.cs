using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneItem : ItemBase
{
    //�唚���̃v���n�u��ݒ�
    [SerializeField]
    private Explosion giganticExplosionPrefab_;

    [SerializeField]private float addLife_=3.0f;

    //���ۃN���X�̎���
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
