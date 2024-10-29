using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Missile : MonoBehaviour
{
    //Explosion�̃v���n�u
    [SerializeField]
    private Explosion explosionPrefab_;
    //�ړ����x
    [SerializeField]
    private float speed_;
    //�ړ���
    private Vector3 velocity_;
    //���e�B�N��
    private GameObject reticle_;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�����̓��̌v�Z�B�����̔�r�p�Ȃ̂œ��̂܂܎g��
        float distanceSqr = Vector3.SqrMagnitude(reticle_.transform.position - transform.position);
        Vector3 velocityDeltaTime = velocity_ * Time.deltaTime;
        float velocityDistanceSqr = Vector3.SqrMagnitude(velocityDeltaTime);
        //�\������������ړ�
        if (distanceSqr >= velocityDistanceSqr)
        {
            transform.position += velocityDeltaTime;
            return;
        }
        //�߂������烌�e�B�N���Əd�Ȃ��Ĕ���
        transform.position = reticle_.transform.position;
        Explosion();
    }

    public void SetUp(GameObject reticle)
    {
        //���e�B�N���̊i�[
        reticle_ = reticle;
        //�����ʒu�ƃ��e�B�N������v���Ă����瑦����
        if (reticle_.transform.position != transform.position)
        {
            //1.�ړ��ʂ̌v�Z
            SetVelocity();
            //2.�����̌v�Z
            LookAtReticle();
        }
        else
        {
            //3.����
            Explosion();
        }
    }

    //�ړ��ʂ�ݒ�
    private void SetVelocity()
    {
        //���W�̍������Ƃ�A�x�N�g�����Z�o
        Vector3 direction = (reticle_.transform.position - transform.position);
        //�x�N�g�����[������Ȃ����
        Assert.IsTrue(direction != Vector3.zero);
        //�x�N�g���𐳋K��
        direction = direction.normalized;
        //�x�N�g���Ɉړ����x���|���ړ��ʂƂ���
        velocity_ = direction * speed_;
    }

    //�I�u�W�F�N�g�̌�����ݒ�
    private void LookAtReticle()
    {
        //�p�x�̎Z�o�BAtan2��Rad���Ԃ��Ă���̂ŁADegree�ɒ���
        float angle = Mathf.Atan2(velocity_.y, velocity_.x) * Mathf.Rad2Deg;
        //�p�x��90�x�X����
        angle -= 90;
        //�Q�[���I�u�W�F�N�g����]������B
        //���̎��̒P�ʂ�Degree�Ȃ̂Œ��ӁB
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Explosion()
    {
        //�����̐���
        Instantiate(
            explosionPrefab_,
            transform.position,
            Quaternion.identity
        );
        //�����ƂƂ��Ƀ��e�B�N��������
        Destroy(reticle_);
        //���g������
        Destroy(gameObject);

    }
}
