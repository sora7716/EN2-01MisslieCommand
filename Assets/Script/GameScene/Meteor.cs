using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    /// <summary>
    /// �Œᗎ�����x
    /// </summary>
    [SerializeField] private float fallSpeedMin_ = 1;

    /// <summary>
    /// �ō��������x
    /// </summary>
    [SerializeField] private float fallSpeedMax_ = 3;

    /// <summary>
    /// �����̃v���n�u�B����������󂯎��
    /// </summary>
    private List<Explosion> explosionPrefabs_;
    private Explosion explosionPrefab_;

    /// <summary>
    /// �n�ʃR���C�_�[�B����������󂯎��
    /// </summary>
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManager gameManager_;

    /// <summary>
    /// �X�R�A�G�t�F�N�g�̃v���n�u
    /// </summary>
    [SerializeField] private ScoreEffect scoreEffectPrefab_;

    /// <summary>
    /// ���񂾂Ƃ�
    /// </summary>
    private Renderer renderer_;
    private float beginAlpha = 1.0f;//������alpha�l
    private float endAlpha = 0.0f;//�ڕW��alpha�l
    float frame_ = 0.0f;//�t���[���l
    bool isDead_ = false;//���񂾂��ǂ����̃t���O
    [SerializeField] private float maxLifeTimer_ = 1;
    [SerializeField] private float scaleUpTimer_ = 0;
    [SerializeField] Vector3 maxScale_ = new Vector3(3, 3, 3);
    private bool isScaleUpFinished_ = false;
    private ParticleSystem particleSystem_;
    // Start is called before the first frame update
    void Start()
    {
        //�����_���[�̏�����
        renderer_ = GetComponent<Renderer>();
        //���W�b�g�{�f�B�̏�����
        rb_ = GetComponent<Rigidbody2D>();
        SetupVelocity();
        //�X�P�[���A�b�v�^�C�}�[�Ƀ}�b�N�X���C�t�^�C�}�[����
        scaleUpTimer_ = maxLifeTimer_;
    }
    /// <summary>
    /// ����������K�v�ȏ��������p��
    /// </summary>
    public void Setup(BoxCollider2D ground, GameManager gameManager, List<Explosion> explosionPrefab)
    {
        gameManager_ = gameManager;
        groundCollider_ = ground;       
        explosionPrefabs_ = explosionPrefab;
    }

    /// <summary>
    /// �ړ��ʂ̐ݒ�
    /// </summary>
    private void SetupVelocity()
    {
        //�n�ʂ̏㉺���E�̈ʒu���擾
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
    /// ����
    /// </summary>
    private void Explosion(Explosion otherExplosion)
    {
        //�A�����Ǝ擾�Ɖ��Z
        int chainNum = otherExplosion.chainNum + 1;
        //���Z����X�R�A(�A�����ɉ�����)
        int score = chainNum * 100;
        ScoreEffect scoreEffect = Instantiate(
            scoreEffectPrefab_,
            transform.position,
            Quaternion.identity
            );
        scoreEffect.SetScore(score);
        //gameManager��score�����Z��ʒm
        gameManager_.AddScore(score);
        //�����𐶐���
        Explosion explosion = Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        //��������Explosion�ɘA������ݒ�
        explosion.chainNum = chainNum;
        //���g�����ł�����
        Destroy(gameObject);

    }

    /// <summary>
    /// �n�ʂɗ���
    /// </summary>
    private void Fall()
    {
        //GameManager�Ƀ_���[�W��ʒm
        gameManager_.Damage(1);
        //���g������
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
