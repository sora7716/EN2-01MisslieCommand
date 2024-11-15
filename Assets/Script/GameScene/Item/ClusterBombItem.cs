using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombItem : ItemBase
{
    [SerializeField]
    private Explosion explosionPrefab_;
    //�擾���Ĕ�����Ԃ��ǂ����𔻒f����
    bool isGet = false;
    //�����������鎞��
    private float explosionImmissionTimer_ = 3;
    //�ׂ��Ȕ����𐶐�����Ԋu
    private float explosionInterval_ = 0.2f;
    private float explosionTimer_ = 0.0f;
    Renderer renderer_;
    public override void Get()
    {
        //Renderer���擾
        if(TryGetComponent(out renderer_))
        {
            renderer_.enabled = false;
        }
        //Collider��ItemBase�Ŏ擾�ς݂Ȃ̂ŁA�����ɂ��邾���ōς�
        collider_.enabled = false;
        //�q�I�u�W�F�N�g(TextMesh)�𖳌�������
        transform.GetChild(0).gameObject.SetActive(false);
        //�擾���ꂽ���Ƃ��L������
        isGet = true;
    }
    //�E�ړ������łȂ�������Update�ōs������override����
    protected override void Update()
    {
        //�擾����Ă��Ȃ���Βʏ��Update�A�܂���N���X��Update���Ă�
        if (!isGet)
        {
            //�����ł���base�͊��N���X
            //���N���X��Update��ǂ�ł���
            base.Update();
            return;
        }
        //���������^�C�}�[�����炷
        explosionImmissionTimer_ -= Time.deltaTime;
        //�����^�C�}�[���؂ꂽ��
        if( explosionImmissionTimer_ <= 0) { Destroy(gameObject); }
        //�ׂ��Ȕ����ɂ��Čv�Z����
        UpdateClusterExplosion();
    }
    //�����Ȕ������N����
    private void UpdateClusterExplosion()
    {
        //�N���X�^�[�����̃^�C�}�[�����炵�A�܂�����Α������^�[��
        explosionTimer_-=Time.deltaTime;
        if (explosionTimer_ > 0) { return; }
        //�����͈͂����߂āA�����_����offset�����߂�
        float randomWidth = 2;
        Vector3 offset = new Vector3(Random.Range(-randomWidth, randomWidth), Random.Range(-randomWidth, randomWidth), 0);
        //���g�̈ʒu��+offset�̈ʒu�ɔ����𐶐��B�^�C�}�[�ɃC���^�[�o�����Z
        Instantiate(explosionPrefab_, transform.position + offset, Quaternion.identity);
        explosionTimer_ += explosionInterval_;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
