using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LifeBar : MonoBehaviour
{
    //Slider�̊���
    private float rotio_;

    //Slider�̃R���|�[�l���g
    private Slider slider_;


    private void Awake() 
    { 
     slider_ = GetComponent<Slider>();
    }

    //Slider�̊�����ݒ�
    public void SetGaugeRatio(float ratio) 
    {
        //0����1�͈̔͂Ő؂�l�߂�
        rotio_ = Mathf.Clamp01(ratio);
        //UI�ɔ��f
        slider_.value = rotio_;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
