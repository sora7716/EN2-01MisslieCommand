using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //���C���J����
    private Camera mainCamera_;
    [SerializeField] private Shake mainCameraShake_;

    //�v���n�u�̐ݒ�
    [SerializeField, Header("Prefabs")]
    //����
    private List<Explosion> explosionPrefabs_;
    //覐�
    [SerializeField] private Meteor meteorPrefab_;
    //���e�B�N��
    [SerializeField] private GameObject reticlePrefab_;
    //�~�T�C��
    [SerializeField] private Missile missilePrefab_;
    //�A�C�e��
    [SerializeField] private List<ItemBase> itemPrefabs_;

    //覐΂̐����֌W
    [SerializeField, Header("MeteorSpawner")]
    //覐΂ɂԂ���n��
    private BoxCollider2D ground_;
    //覐΂̐����̎��ԊԊu
    [SerializeField] private float meteorInterval_ = 1;
    //覐΂̐����܂ł̎���
    private float meteorTimer_;
    //覐΂̈ʒu
    [SerializeField] private List<Transform> meteorSpawnPositions_;
    //覐ΒP��
    Meteor meteor_;

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

    //�~�T�C���̔��ˈʒu
    [SerializeField, Header("Missile")]
    //���ˈʒu
    private Transform[] shotPoints_;
    private Vector3[] distance_;
    [SerializeField] private RemainingMissile remainingMissile_;

    //�A�C�e��
    [SerializeField, Header("Item")]
    //�A�C�e���̐����̎��ԊԊu
    private float itemInterval_ = 1f;
    //�A�C�e���̐����܂ł̎���
    private float itemTimer_;
    //�A�C�e���̈ʒu
    [SerializeField] private List<Transform> itemSpawnPositions_;
    //�A�C�e�����|�b�v������
    bool isItemPop_ = false;
    /// <summary>
    /// �t�F�[�h�̃R���g���[���[
    /// </summary>
    [SerializeField, Header("FadeImage")] private FadeControl fadeControl_;

    /// <summary>
    /// �n��
    /// </summary>
    [SerializeField, Header("Ground")] private Shake groundShake_;

    //�Q�[���̏I���֌W
    [SerializeField, Header("DeadFhase")]
    private float deadFadeEndSecond_ = 2.0f;//���񂾂Ƃ��̃t�F�[�h�̎���
    [SerializeField] private float deadMeteorInterval_ = 1.0f;//���񂾂Ƃ���覐΂̗�����C���^�[�o��
    [SerializeField] private int fallMeteorNum_ = 1;//����覐΂𗎂Ƃ���
    //���񂾂��ǂ����̃t���O
    bool isDead_ = false;

    //�Q�[�����X�^�[�g���邩�ǂ����̃t���O
    bool isGameStart_ = false;

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
        Assert.IsTrue(meteorSpawnPositions_.Count > 0, "spawnPositions_�ɗv�f���������܂���B");
        foreach (var t in meteorSpawnPositions_)
        {
            if (t == null)
            {
                //�e�v�f��Null���܂܂�Ă��Ȃ����Ƃ��m�F
                Assert.IsNotNull(t, "spawnPositions_��Null���܂܂�Ă��܂�");
            }
        }
        mainCamera_.TryGetComponent(out mainCamera_);
        //�̗͂̏�����
        ResetLife();
        //�t�F�[�h�C�����J�n����
        fadeControl_.SetIsFadeIn(true);
        //�t�F�[�h��\��������
        fadeControl_.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart_)
        {
            //�N���b�N�������甚���𐶐�
            if (Input.GetMouseButtonDown(0)&&!isDead_)
            {
                if (remainingMissile_.GetMissileCount() >= 0)
                {
                    GenerateMissile();
                }
                remainingMissile_.MissileShot();
            }
            UpdateMeteorTimer();
            if (score_ > 500)
            {
                isItemPop_ = true;
            }
            UpdateItemTimer();
            if (isDead_)
            {
                Dead();
                if (fadeControl_.isFinished())
                {
                    SceneManager.LoadScene("EndScene");
                }
            }
        }
        else
        {
            fadeControl_.FadeIn();
        }
        //�t�F�[�h���I��������Q�[���̏������J�n����
        if (fadeControl_.isFinished())
        {
            //�Q�[�����X�^�[�g������
            isGameStart_ = true;
            fadeControl_.SetIsFinished(false);
        }
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
        Vector3 launchPosition = Vector3.zero;
        distance_[0] = clicPosition - shotPoints_[0].position;
        int num = 0;
        for (int i = 1; i < shotPoints_.Length; i++)
        {
            distance_[i] = clicPosition - shotPoints_[i].position;
            if (Mathf.Abs(distance_[i - 1].x) < Mathf.Abs(distance_[i].x))
            {
                num = i - 1;
                break;
            }
            else
            {
                num = i;
            }
        }
        launchPosition = shotPoints_[num].position;
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
        //���C�t�����炷
        life_ -= point;
        //�_���[�W������������̃V�F�C�N
        groundShake_.SetIsShake(true);//�t���O�𗧂Ă�
        groundShake_.ShakeStart();//�V�F�C�N���X�^�[�g
        //UI�̍X�V
        UpdateLifeBar();
    }

    /// <summary>
    ///覐΃^�C���̍X�V
    /// </summary>
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;//�^�C�}�[�����炷
        if (meteorTimer_ > 0) { return; }//�^�C�}�[���[������Ȃ���Δ�����
        meteorTimer_ += meteorInterval_;//�C���^�[�o����ݒ�

        if (life_ <= 0.0f)//���C�t���[���Ȃ��
        {
            isDead_ = true;
            fallMeteorNum_--;
            if (fallMeteorNum_ <= 0)
            {
                fadeControl_.SetIsFadeOut(true);//�t�F�[�h�A�E�g�̃t���O�𗧂Ă�
            }
        }
        GenerateMeteor();
    }

    /// <summary>
    /// 覐΂̐���
    /// </summary>
    private void GenerateMeteor()
    {
        int max = meteorSpawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition = meteorSpawnPositions_[posIndex].position;
        meteor_ = Instantiate(meteorPrefab_, spawnPosition, Quaternion.identity);
        meteor_.Setup(ground_, this, explosionPrefabs_);
    }

    /// <summary>
    ///�A�C�e���^�C���̍X�V
    /// </summary>
    private void UpdateItemTimer()
    {
        itemTimer_ -= Time.deltaTime;
        if (itemTimer_ > 0 || !isItemPop_) { return; }
        itemTimer_ += itemInterval_;
        GenerateItem();
    }

    /// <summary>
    /// �A�C�e���̐���
    /// </summary>
    private void GenerateItem()
    {
        int spawnPosMaxIndex = itemSpawnPositions_.Count;
        int posIndex = Random.Range(0, spawnPosMaxIndex);
        int itemTypeMaxIndex = itemPrefabs_.Count;
        int typeIndex = Random.Range(0, itemTypeMaxIndex);
        Vector3 spawnPosition = itemSpawnPositions_[posIndex].position;
        ItemBase item = Instantiate(itemPrefabs_[typeIndex], spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// ���C�t�̏�����
    /// </summary>
    private void ResetLife()
    {
        life_ = maxLife_;
        //UI�̍X�V
        UpdateLifeBar();
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

    /// <summary>
    /// ���񂾂Ƃ��̏���
    /// </summary>
    private void Dead()
    {
        mainCameraShake_.SetIsShake(true);//�V�F�C�N����t���O�𗧂Ă�
        mainCameraShake_.ShakeStart();//�V�F�C�N���X�^�[�g������
        meteorInterval_ = deadMeteorInterval_;//�C���^�[�o����ݒ�
        meteor_.SetIsSpeedUp(true);//�X�s�[�h�A�b�v�t���O��ݒ�
        scoreText_.SaveScore();//�X�R�A��ۑ�
        fadeControl_.SetEndSecond(deadFadeEndSecond_);//�t�F�[�h�̃G���h���Ԃ�ݒ�
        fadeControl_.FadeOut(Color.white - new Color(0.0f, 0.0f, 0.0f, 1.0f), Color.white);//�t�F�[�h�A�E�g���X�^�[�g
    }
}
