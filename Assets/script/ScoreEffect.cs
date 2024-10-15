using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreEffect : MonoBehaviour
{
    //上昇速度
    [SerializeField]
    float upSpeed_ = 1;
    //消滅までの時間(秒)
    [SerializeField]
    float aliveTime_ = 1;
    //カウンター
    float alivedTimer_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alivedTimer_ += Time.deltaTime;
        if (alivedTimer_ >= aliveTime_)
        {
            gameObject.SetActive(false);
        }
        //上方向へ
        transform.Translate(Vector3.up * upSpeed_ * Time.deltaTime);
    }

    public void SetScore(int score)
    {
        //数字は何度も書き換えないので、GetComponentして
        //そのまま書き換え。変数も記録しない
        GetComponent<TMP_Text>().text= score.ToString();
    }
}
