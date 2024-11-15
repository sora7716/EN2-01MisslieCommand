using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    //�X�R�A�֌W
    [SerializeField, Header("ScoreUISettings")]
    //�X�R�A�\���p�e�L�X�g
    private ScoreText scoreText_;
    //�X�R�A�̃V�F�C�N
    [SerializeField] private ShakeRect shakeRect_;
    //�X�R�A
    private int score_;
    //�X�R�A�����Z���鎞��(�b)
    [SerializeField] private float scoreAddInterval_ = 2;
    //�X�R�A�̉��Z���鎞�Ԃ̃t���[����
    float scoreAddFrame_ = 0.0f;
    //�X�R�A�����Z���邩�ǂ����̃t���O
    bool isAddScore_ = false;

    //�Q�[���I�[�o�[�֌W
    [SerializeField, Header("GameOver")] private EasingRect gameOver_;

    //�V�[���J�ڂ��Ă������ǂ����̃t���O
    bool isSceneChange_ = false;
    //�V�[�����؂�ւ��܂ł̎���
    [SerializeField] private float sceneChangeTime_ = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //�ڕW�n�_�ɍs�����`��Ԃ��J�n����t���O�𗧂Ă�
        gameOver_.SetIsStart(true);
    }

    // Update is called once per frame
    void Update()
    {
        //���`��Ԃ��J�n
        gameOver_.LerpStart();
        if (GameScore.kScore > score_ && gameOver_.IsFinished())
        {
            //���Z�t���O�𗧂Ă�
            isAddScore_ = true;
        }
        else
        {
            //���Z�t���O���ւ��܂�
            isAddScore_ = false;
        }
        //���Z�t���O����������
        if (isAddScore_)
        {
            //�C���^�[�o����݂��ĉ��Z����
            if (scoreAddFrame_ < scoreAddInterval_)
            {
                scoreAddFrame_ += Time.deltaTime / scoreAddInterval_;
            }
            else
            {
                //100�����Z
                AddScore(100);
                //�V�F�C�N���J�n�t���O�𗧂Ă�
                shakeRect_.SetIsShake(true);
                //�V�F�C�N���J�n
                shakeRect_.ShakeStart();
                //���Z�̃t���[�����[���ɏ�����
                scoreAddFrame_ = 0.0f;
            }

            if (Input.GetMouseButtonDown(0))
            {
                score_ = GameScore.kScore;
                scoreText_.UpdateScoreText(score_);
                //�V�F�C�N���J�n�t���O�𗧂Ă�
                shakeRect_.SetIsShake(true);
                isAddScore_ = false;
            }
        }
        //�V�F�C�N���J�n
        shakeRect_.ShakeStart();
        //�X�R�A�����ׂĉ��Z����������V�[����؂�ւ�����悤�ɂ���
        if (GameScore.kScore == score_ && gameOver_.IsFinished())
        {
            isSceneChange_ = true;//�V�[���؂�ւ�OK
        }
        if (isSceneChange_)
        {
            //�V�[���؂�ւ���܂ł̑҂�����
            sceneChangeTime_ -= Time.deltaTime;
            if (Input.GetMouseButton(0) && sceneChangeTime_ < 0.0f)
            {
                //�؂�ւ���܂ł̎��Ԃ�0��菬�����Ȃ�����ƃ{�_������������V�[����؂�ւ���
                SceneManager.LoadScene("TitleScene");
            }
        }

    }

    /// <summary>
    /// �X�R�A�̉��Z
    /// </summary>
    /// <param name="point">���Z����l</param>
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }

}
