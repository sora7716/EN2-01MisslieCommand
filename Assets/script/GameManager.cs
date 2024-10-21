using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    //���C���J����
    private Camera mainCamera_;

    //�v���n�u�̐ݒ�
    [SerializeField, Header("Prefabs")]
    //����
    private Explosion explosionPrefab_;
    //覐�
    [SerializeField] private Meteor meteorPrefab_;
    //���e�B�N��
    [SerializeField] private GameObject reticlePrefab_;
    //�~�T�C��
    [SerializeField] private Missile missilePrefab_;

    //覐΂̐����֌W
    [SerializeField, Header("MeteorSpawner")]
    //覐΂ɂԂ���n��
    private BoxCollider2D ground_;
    //覐΂̐����̎��ԊԊu
    [SerializeField] private float meteorInterval_ = 1;
    //覐΂̐����܂ł̎���
    private float meteorTimer_;
    //覐΂̈ʒu
    [SerializeField] private List<Transform> spawnPositions_;

    //�X�R�A�֌W
    [SerializeField, Header("ScoreUISettings")]
    //�X�R�A�\���p�e�L�X�g
    private ScoreText scoreText_;
    //�X�R�A
    private int score_;

    //���C�t�֌W
    [SerializeField, Header("LifeUISettings")]
    //���C�t�Q�[�W
    private LifeBar lifeBar_;
    //�ő�̗�
    [SerializeField]
    private float maxLife_ = 10;
    //���݂̗̑�
    private float life_;

    ////�~�T�C���̔��ˈʒu
    [SerializeField, Header("Missile")]
    //���ˈʒu
    private Transform[] shotPoints_;
    private Vector3[] distance_;
    void Start()
    {
        distance_ = new Vector3[shotPoints_.Length];
        //�uMainCamera�v�Ƃ����^�O�����Q�[���I�u�W�F�N�g������
        GameObject mainCamraObject = GameObject.FindGameObjectWithTag("MainCamera");
        //Null�ł͂Ȃ����Ƃ��m�F
        Assert.IsNotNull(mainCamraObject, "MainCamra��������܂���ł���");
        //Camera�R���|�[�l���g�����݂��A�擾�ł��邱�Ƃ��m�F
        Assert.IsTrue(mainCamraObject.TryGetComponent(out mainCamera_), "MainCamera��Camera�R���|�[�l���g������܂���");

        //�����ʒuList�̗v�f����1�ȏ�ł��邱�Ƃ��m�F
        Assert.IsTrue(spawnPositions_.Count > 0, "spawnPositions_�ɗv�f���������܂���B");
        foreach(var t in spawnPositions_)
        {
            //�e�v�f��Null���܂܂�Ă��Ȃ����Ƃ��m�F
            Assert.IsNotNull(t, "spawnPositions_��Null���܂܂�Ă��܂�");
        }

        //�̗͂̏�����
        ResetLife();
    }

    // Update is called once per frame
    void Update()
    {
        //�N���b�N�������甚���𐶐�
        if (Input.GetMouseButtonDown(0)) { GenerateMissile(); }
        UpdateMeteorTimer();
    }

    /// <summary>
    /// �~�T�C���̐���
    /// </summary>
    private void GenerateMissile()
    {
        //�N���b�N�����X�N���[�����W�̎擾���A���[���h���W�ɕϊ�����
        Vector3 clicPosition = mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clicPosition.z = 0;
        GameObject reticle = Instantiate(reticlePrefab_, clicPosition, Quaternion.identity);

        //�~�T�C���𐶐�
        Vector3 launchPosition = new Vector3(0, -3, 0);
        for (int i = 1; i < shotPoints_.Length; i++)
        {
            distance_[i-1] = clicPosition - shotPoints_[i].position;
            if (
                Mathf.Abs(distance_[i - 1].x) < Mathf.Abs(distance_[i].x) &&
                Mathf.Abs(distance_[i - 1].y) < Mathf.Abs(distance_[i].y)
                )
            {
                launchPosition = distance_[i - 1];
            }
            else
            {
                launchPosition = distance_[i];
            }
        }
        Missile missile = Instantiate(missilePrefab_, launchPosition, Quaternion.identity);
        missile.SetUp(reticle);
    }

    /// <summary>
    /// �X�R�A�̉��Z
    /// </summary>
    /// <param name="point">���Z����X�R�A</param>
    public void AddScore(int point) 
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }

    /// <summary>
    /// �_���[�W�̉��Z
    /// </summary>
    /// <param name="point"></param>
    public void Damage(int point) 
    {
        life_-=point;
        //UI�̍X�V
        UpdateLifeBar();
    }

    /// <summary>
    ///覐΃^�C���̍X�V
    /// </summary>
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }

    /// <summary>
    /// 覐΂̐���
    /// </summary>
    private void GenerateMeteor()
    {
        int max = spawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition = spawnPositions_[posIndex].position;
        Meteor meteor = Instantiate(meteorPrefab_, spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);
    }

    /// <summary>
    /// ���C�t�̏�����
    /// </summary>
    private void ResetLife() 
    {
        life_ = maxLife_;
        //UI�̍X�V
        UpdateLifeBar() ;
    }

    /// <summary>
    /// ���C�t�o�[�̍X�V
    /// </summary>
    private void UpdateLifeBar() 
    {
        //�ő�̗͂ƌ��ݑ̗͂̊����ŉ��������Z�o
        float lifeRatio = Mathf.Clamp01(life_ / maxLife_);
        //������lifeBar_�֓`���AUI�ɔ��f���Ă��炤
        lifeBar_.SetGaugeRatio(lifeRatio);
    }
}
