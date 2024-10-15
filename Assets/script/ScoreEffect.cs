using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreEffect : MonoBehaviour
{
    //�㏸���x
    [SerializeField]
    float upSpeed_ = 1;
    //���ł܂ł̎���(�b)
    [SerializeField]
    float aliveTime_ = 1;
    //�J�E���^�[
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
        //�������
        transform.Translate(Vector3.up * upSpeed_ * Time.deltaTime);
    }

    public void SetScore(int score)
    {
        //�����͉��x�����������Ȃ��̂ŁAGetComponent����
        //���̂܂܏��������B�ϐ����L�^���Ȃ�
        GetComponent<TMP_Text>().text= score.ToString();
    }
}
