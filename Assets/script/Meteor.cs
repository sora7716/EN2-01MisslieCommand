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

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        SetupVelocity();
    }
    /// <summary>
    /// ����������K�v�ȏ��������p��
    /// </summary>
    public void Setup(BoxCollider2D ground, GameManager gameManager, Explosion explosionPrefab)
    {
        gameManager_ = gameManager;
        groundCollider_ = ground;
        explosionPrefab_ = explosionPrefab;
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
