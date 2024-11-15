using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeRect : MonoBehaviour
{
    //�J�����̏����ʒu
    Vector3 firstPos_ = Vector3.zero;

    //�V�F�C�N���J�n�t���O
    bool isShake_ = false;

    //�V�F�C�N�̕�
    [SerializeField] private Vector2 randomRangeX_ = new Vector2(-0.5f, 0.5f);
    [SerializeField] private Vector2 randomRangeY_ = new Vector2(-0.5f, 0.5f);

    //�V�F�C�N�̍s���Ă��鎞��
    [SerializeField] float shakeTime_ = 2.0f;
    float frame_ = 0.0f;

    //���N�g
    RectTransform rectTransform_;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform_ = GetComponent<RectTransform>(); 
        firstPos_ = rectTransform_.anchoredPosition;
    }

    /// <summary>
    /// �V�F�C�N�̃t���O�̃Z�b�^�[
    /// </summary>
    /// <param name="isShake"></param>
    public void SetIsShake(bool isShake)
    {
        isShake_ = isShake;
    }

    /// <summary>
    /// �V�F�C�N���X�^�[�g������
    /// </summary>
    public void ShakeStart()
    {
        if (isShake_)
        {
            if (frame_ < shakeTime_)
            {
                frame_ += Time.deltaTime / shakeTime_;
            }
            else
            {
                isShake_ = false;
            }
            Vector3 randomPos;
            randomPos = new Vector3(
                Random.Range(randomRangeX_.x, randomRangeX_.y),
                Random.Range(randomRangeY_.x, randomRangeY_.y),
                -10.0f
            );
            rectTransform_.anchoredPosition = randomPos;
        }
        else
        {
            rectTransform_.anchoredPosition = firstPos_;
            frame_ = 0.0f;
        }
    }
}
