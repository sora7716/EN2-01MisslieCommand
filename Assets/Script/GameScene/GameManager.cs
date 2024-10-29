using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    //メインカメラ
    private Camera mainCamera_;

    //プレハブの設定
    [SerializeField, Header("Prefabs")]
    //爆発
    private List<Explosion> explosionPrefabs_;
    //隕石
    [SerializeField] private Meteor meteorPrefab_;
    //レティクル
    [SerializeField] private GameObject reticlePrefab_;
    //ミサイル
    [SerializeField] private Missile missilePrefab_;
    //アイテム
    [SerializeField]private List<ItemBase> itemPrefabs_;

    //隕石の生成関係
    [SerializeField, Header("MeteorSpawner")]
    //隕石にぶつかる地面
    private BoxCollider2D ground_;
    //隕石の生成の時間間隔
    [SerializeField] private float meteorInterval_ = 1;
    //隕石の生成までの時間
    private float meteorTimer_;
    //隕石の位置
    [SerializeField] private List<Transform> meteorSpawnPositions_;

    //スコア関係
    [SerializeField, Header("ScoreUISettings")]
    //スコア表示用テキスト
    private ScoreText scoreText_;
    //スコア
    private int score_;

    //ライフ関係
    [SerializeField, Header("LifeUISettings")]
    //ライフゲージ
    private LifeBar lifeBar_;
    //最大体力
    [SerializeField]
    private float maxLife_ = 10;
    //現在の体力
    private float life_;

    //ミサイルの発射位置
    [SerializeField, Header("Missile")]
    //発射位置
    private Transform[] shotPoints_;
    private Vector3[] distance_;

    //アイテム
    [SerializeField, Header("Item")]
    //アイテムの生成の時間間隔
    private float itemInterval_ = 1f;
    //アイテムの生成までの時間
    private float itemTimer_;
    //アイテムの位置
    [SerializeField] private List<Transform> itemSpawnPositions_;

    void Start()
    {
        distance_ = new Vector3[shotPoints_.Length];
        //「MainCamera」というタグを持つゲームオブジェクトを検索
        GameObject mainCamraObject = GameObject.FindGameObjectWithTag("MainCamera");
        //Nullではないことを確認
        Assert.IsNotNull(mainCamraObject, "MainCamraが見つかりませんでした");
        //Cameraコンポーネントが存在し、取得できることを確認
        Assert.IsTrue(mainCamraObject.TryGetComponent(out mainCamera_), "MainCameraにCameraコンポーネントがありません");

        //生成位置Listの要素数が1以上であることを確認
        Assert.IsTrue(meteorSpawnPositions_.Count > 0, "spawnPositions_に要素が一つもありません。");
        foreach (var t in meteorSpawnPositions_)
        {
            //各要素にNullが含まれていないことを確認
            Assert.IsNotNull(t, "spawnPositions_にNullが含まれています");
        }

        //体力の初期化
        ResetLife();
    }

    // Update is called once per frame
    void Update()
    {
        //クリックをしたら爆発を生成
        if (Input.GetMouseButtonDown(0)) { GenerateMissile(); }
        UpdateMeteorTimer();
        UpdateItemTimer();
    }

    /// <summary>
    /// ミサイルの生成
    /// </summary>
    private void GenerateMissile()
    {
        //クリックしたスクリーン座標の取得し、ワールド座標に変換する
        Vector3 clicPosition = mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clicPosition.z = 0;
        GameObject reticle = Instantiate(reticlePrefab_, clicPosition, Quaternion.identity);

        //ミサイルを生成
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
    /// スコアの加算
    /// </summary>
    /// <param name="point">加算するスコア</param>
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }

    /// <summary>
    /// ダメージの加算
    /// </summary>
    /// <param name="point"></param>
    public void Damage(int point)
    {
        life_ -= point;
        //UIの更新
        UpdateLifeBar();
    }

    /// <summary>
    ///隕石タイムの更新
    /// </summary>
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }

    /// <summary>
    /// 隕石の生成
    /// </summary>
    private void GenerateMeteor()
    {
        int max = meteorSpawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition = meteorSpawnPositions_[posIndex].position;
        Meteor meteor = Instantiate(meteorPrefab_, spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefabs_);
    }

    /// <summary>
    ///アイテムタイムの更新
    /// </summary>
    private void UpdateItemTimer()
    {
        itemTimer_ -= Time.deltaTime;
        if (itemTimer_ > 0) { return; }
        itemTimer_ += itemInterval_;
        GenerateItem();
    }

    /// <summary>
    /// アイテムの生成
    /// </summary>
    private void GenerateItem()
    {
        int spawnPosMaxIndex = itemSpawnPositions_.Count;
        int posIndex = Random.Range(0, spawnPosMaxIndex);
        int itemTypeMaxIndex = itemPrefabs_.Count;
        int typeIndex = Random.Range(0, itemTypeMaxIndex);
        Vector3 spawnPosition = itemSpawnPositions_[posIndex].position;
        ItemBase item = Instantiate(itemPrefabs_[typeIndex], spawnPosition,Quaternion.identity);
    }

    /// <summary>
    /// ライフの初期化
    /// </summary>
    private void ResetLife()
    {
        life_ = maxLife_;
        //UIの更新
        UpdateLifeBar();
    }

    /// <summary>
    /// ライフバーの更新
    /// </summary>
    private void UpdateLifeBar()
    {
        //最大体力と現在体力の割合で何割かを算出
        float lifeRatio = Mathf.Clamp01(life_ / maxLife_);
        //割合をlifeBar_へ伝え、UIに反映してもらう
        lifeBar_.SetGaugeRatio(lifeRatio);
    }
}
