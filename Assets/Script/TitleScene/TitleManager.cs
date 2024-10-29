using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// タイトルバー
    /// </summary>
    [SerializeField, Header("TitleBarControl")] TitleBarControl titleBarControl_;

    /// <summary>
    /// プレススタート
    /// </summary>
    [SerializeField, Header("PressStart")] PressStartControl pressStartControl_;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!titleBarControl_.IsStart())
        {
            //pressStartを登場
            pressStartControl_.SetIsStart(true);
            if (Input.GetMouseButtonDown(0)&&!pressStartControl_.IsStart())
            {
                titleBarControl_.SetIsRevers(true);
                pressStartControl_.SetIsRevers(true);
                //SceneManager.LoadScene("GameScene");
            }
        }
    }
}
