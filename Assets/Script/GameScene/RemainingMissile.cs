using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingMissile : MonoBehaviour
{
    //�~�T�C���̎c�e��
    [SerializeField] private List<Image> missiles_;
    //�c�e���̍ŏ��̐F
    [SerializeField] private Color missileBeginColor_;
    //������Ƃ��̃J���[
    [SerializeField] private Color missileUseColor_;
    //�ǂꂭ�炢�c���Ă���̂�
    int missileCount_;
    //�N�[���^�C��
    bool isCoolTime_ = false;//�����t���O
    float coolTimer_;//�^�C�}�[
    [SerializeField] float coolTimeInterval_ = 1.0f;//�C���^�[�o��(�b)
    // Start is called before the first frame update
    void Start()
    {
        //�F�̏�����
        foreach (var missile in missiles_) 
        {
            missile.color = missileBeginColor_;
        }
        //�~�T�C���̎c�e����ݒ�
        missileCount_ = missiles_.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        //�e��max����Ȃ�������N�[���^�C������������
        if (missileCount_ < 4)
        {
            isCoolTime_ = true;
        }
        if (isCoolTime_)
        {
            CoolTime();
        }
    }

    /// <summary>
    /// �~�T�C���̎c�i�������炷�֐�
    /// </summary>
    public void MissileShot()
    {
        //�e�̃J�E���g���[������Ȃ�������
        if (missileCount_ >= 0)
        {
            missiles_[missileCount_].color = missileUseColor_;//�F��ύX
            missileCount_ -= 1;//�c�e�������炷
        }
    }

    void CoolTime()
    {
        if (coolTimer_ < coolTimeInterval_)
        {
            coolTimer_ += Time.deltaTime / coolTimeInterval_;//�N�[���^�C��
        }
        else
        {
            missileCount_ += 1;//�c�e���𑝂₷
            missiles_[missileCount_].color = missileBeginColor_;//�F��߂�
            coolTimer_ = 0;//�N�[���^�C�������Z�b�g
            isCoolTime_ = false;//�N�[���^�C�����s���t���O�����Z�b�g
        }
    }

    /// <summary>
    /// �~�T�C���̎c�e���̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public int GetMissileCount()
    {
        return missileCount_;
    }
}
