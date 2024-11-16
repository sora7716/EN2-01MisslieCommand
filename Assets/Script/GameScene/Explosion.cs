using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float maxLifeTimer_ = 1;
    [SerializeField] private float time_ = 0;
    [SerializeField] Vector3 maxScale_ = new Vector3(1f, 1f, 1f);
    public int chainNum = 0;//連鎖している数
    Renderer renderer_;
    private float beginAlpha = 1.0f;//最初の色
    private float endAlpha = 0.0f;//最後の色
    float frame_ = 0.0f;//フレーム値
    /// <summary>
    /// ブレンドする時間
    /// </summary>
    [SerializeField] private float blendSpeed_ = 5.0f;

    //回復するかのフラグ
    [SerializeField] bool isRecovery_ = false;
    // Start is called before the first frame update
    void Start()
    {
        time_ = maxLifeTimer_;
        renderer_ = gameObject.GetComponent<Renderer>();
        gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        time_ -= Time.deltaTime * 2;
        ScaleUp();
        if (time_ > 0)
        {
            return;
        }
        Blend();
    }

    protected virtual void ScaleUp()
    {
        transform.localScale = maxScale_ * (1.0f - time_ / maxLifeTimer_);
    }

    protected virtual void Blend()
    {
        transform.localScale = maxScale_;
        frame_ += Time.deltaTime * blendSpeed_;
        Color color = renderer_.material.color;
        color.a = Mathf.Lerp(beginAlpha, endAlpha, frame_);
        renderer_.material.color = color;
        if (renderer_.material.color.a == endAlpha)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //回復させる
        if (gameObject.CompareTag("RegeneItem"))
        {
            isRecovery_ = true;
        }
    }

    /// <summary>
    /// 回復するかのフラグ
    /// </summary>
    /// <returns></returns>
    public bool IsRecovery()
    {
        return isRecovery_;
    }
}
