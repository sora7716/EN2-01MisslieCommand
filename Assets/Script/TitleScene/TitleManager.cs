using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// タイトルバー
    /// </summary>
    [SerializeField, Header("TitleBarControl")] private TitleBarControl titleBarControl_;

    /// <summary>
    /// プレススタート
    /// </summary>
    [SerializeField, Header("PressStart")] private PressStartControl pressStartControl_;

    [SerializeField, Header("FadeImage")] private FadeControl fadeControl_;
    Color beginFadeColor_;//フェードするときの最初の色

    bool isPress_ = false;
    // Start is called before the first frame update
    void Start()
    {
        //フェード用のimageを透明にする
       beginFadeColor_= fadeControl_.Invisible(Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        //押された瞬間でなおかつタイトルバーの目標地点に行くフラグがfalseだったら
        if (Input.GetMouseButtonDown(0) && !titleBarControl_.IsStart())
        {
            //押されたフラグかfalseだったら
            if (!isPress_)
            {
                //フレームの初期化
                titleBarControl_.ResetFrame();
                pressStartControl_.ResetFrame();
            }
            isPress_ = true;
        }
        if (!isPress_)
        {
            //pressStartを登場
            pressStartControl_.SetIsStart(true);
        }
        if (isPress_)
        {
            //目標地点に行くフラグを停止
            pressStartControl_.SetIsStart(false);
            titleBarControl_.SetIsStart(false);
            //元の位置に戻すフラグを開始」
            titleBarControl_.SetIsRevers(true);
            pressStartControl_.SetIsRevers(true);
            //フェードアウトを開始
            fadeControl_.SetIsFadeOut(true);
            fadeControl_.FadeOut(beginFadeColor_, Color.black);
        }
        //フェードが終了したらシーンを切り替え
        if (fadeControl_.isFinished())
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
