using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    //�X�R�A�֌W
    [SerializeField, Header("ScoreUISettings")]
    //�X�R�A�\���p�e�L�X�g
    private ScoreText scoreText_;
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
            isAddScore_ = true;
        }
        else
        {
            isAddScore_ = false;
        }
        if (isAddScore_)
        {
            if (scoreAddFrame_ < scoreAddInterval_)
            {
                scoreAddFrame_ += Time.deltaTime / scoreAddInterval_;
            }
            else
            {
                AddScore(100);
                scoreAddFrame_ = 0.0f;
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
