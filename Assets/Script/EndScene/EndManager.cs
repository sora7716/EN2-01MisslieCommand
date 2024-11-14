using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameScore.kScore > score_)
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
                //�V�F�C�N���J�n
                shakeRect_.ShakeStart();
                isAddScore_ = false;
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
