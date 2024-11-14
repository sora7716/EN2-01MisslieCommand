using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// �^�C�g���o�[
    /// </summary>
    [SerializeField, Header("TitleBarControl")] private TitleBarControl titleBarControl_;

    /// <summary>
    /// �v���X�X�^�[�g
    /// </summary>
    [SerializeField, Header("PressStart")] private PressStartControl pressStartControl_;

    [SerializeField, Header("FadeImage")] private FadeControl fadeControl_;
    Color beginFadeColor_;//�t�F�[�h����Ƃ��̍ŏ��̐F

    bool isPress_ = false;
    // Start is called before the first frame update
    void Start()
    {
        //�t�F�[�h�p��image�𓧖��ɂ���
       beginFadeColor_= fadeControl_.Invisible(Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        //�����ꂽ�u�ԂłȂ����^�C�g���o�[�̖ڕW�n�_�ɍs���t���O��false��������
        if (Input.GetMouseButtonDown(0) && !titleBarControl_.IsStart())
        {
            //�����ꂽ�t���O��false��������
            if (!isPress_)
            {
                //�t���[���̏�����
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
            //�ڕW�n�_�ɍs���t���O���~
            pressStartControl_.SetIsStart(false);
            titleBarControl_.SetIsStart(false);
            //���̈ʒu�ɖ߂��t���O���J�n�v
            titleBarControl_.SetIsRevers(true);
            pressStartControl_.SetIsRevers(true);
            //�t�F�[�h�A�E�g���J�n
            fadeControl_.SetIsFadeOut(true);
            fadeControl_.FadeOut(beginFadeColor_, Color.black);
        }
        //�t�F�[�h���I��������V�[����؂�ւ�
        if (fadeControl_.isFinished())
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
