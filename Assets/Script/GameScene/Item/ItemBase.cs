using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider2D))]
public abstract class ItemBase : MonoBehaviour
{
    //�ړ����x�B�h���N���X�ł��g����悤��protected
    [SerializeField]
    protected float speed_ = 1;
    float theta_ = 0;
    Vector3 center_ = Vector3.zero;
    Vector3 firstPos_ = Vector3.zero;
    //  ��ʂ̃T�C�Y�̊m�F�p
    protected Camera camera_;
    //���@�̃T�C�Y�m�F�p
    protected Collider2D collider_;
    //������  
    private void Awake()
    {
        camera_ = Camera.main;
        collider_ = GetComponent<Collider2D>();
        firstPos_ = transform.position;
        center_ = firstPos_;
    }
    //�X�V����
    protected virtual void Update()
    {
        //�ړ�����
        center_ += new Vector3(speed_ * Time.deltaTime, 0.0f, 0.0f);
        theta_ += 1.0f * Time.deltaTime;
        transform.position = LissajousCurve(new Vector3(theta_ * 3.0f + Mathf.PI / 6.0f, theta_ *4.0f, 1.0f), center_, new Vector3(2.0f, 2.0f, 1.0f));
        gameObject.transform.localEulerAngles += new Vector3(0.0f, 0.0f, 1.0f);
        //��ʊO�̊m�F
        //���[���h���W��̃J�����E�[���J��������Z�o
        float worldScreenRight = camera_.orthographicSize * camera_.aspect;
        //�A�C�e���̓����蔻��̃T�C�Y
        float boundsSize = collider_.bounds.size.x;
        //�����蔻��܂ߊ��S�ɉ�ʊO�ɏo�Ă�����Destroy
        if (transform.position.x > worldScreenRight + boundsSize)
        {
            Destroy(gameObject);
        }
    }
    //�Փ˔���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion")) { Get(); }
    }
    //�A�C�e���擾�̏����̒��ۃ��\�b�h�B�h���N���X�Ŏ������K�{�B
    //���N���X�ł͎������Ȃ��̂ŁA�Ōオ();�ł��邱�Ƃɒ��ӁB
    public abstract void Get();

    //���T�[�W���Ȑ�
    Vector3 LissajousCurve(Vector3 theta, Vector3 center, Vector3 scalar)
    {
        Vector3 result = Vector3.zero;
        result.x = scalar.x * Mathf.Sin(theta.x) + center.x;
        result.y = scalar.y * Mathf.Sin(theta.y) + center.y;
        result.z = scalar.z * Mathf.Sin(theta.z) + center.z;
        return result;
    }
}
