using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// �^�C�g���o�[
    /// </summary>
    [SerializeField, Header("TitleBarControl")] TitleBarControl titleBarControl_;

    /// <summary>
    /// �v���X�X�^�[�g
    /// </summary>
    [SerializeField, Header("PressStart")] PressStartControl pressStartControl_;

    bool isPress_ = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !titleBarControl_.IsStart())
        {
            if (!isPress_)
            {
                titleBarControl_.ResetFrame();
                pressStartControl_.ResetFrame();
            }
            isPress_ = true;
        }
        if (!isPress_)
        {
            //pressStart��o��
            pressStartControl_.SetIsStart(true);
        }
        if (isPress_)
        {
            pressStartControl_.SetIsStart(false);
            titleBarControl_.SetIsStart(false);
            titleBarControl_.SetIsRevers(true);
            pressStartControl_.SetIsRevers(true);
            // SceneManager.LoadScene("GameScene");
        }
       
    }
}
