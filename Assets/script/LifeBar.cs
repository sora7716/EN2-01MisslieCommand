using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LifeBar : MonoBehaviour
{
    //Sliderの割合
    private float rotio_;

    //Sliderのコンポーネント
    private Slider slider_;


    private void Awake() 
    { 
     slider_ = GetComponent<Slider>();
    }

    //Sliderの割合を設定
    public void SetGaugeRatio(float ratio) 
    {
        //0から1の範囲で切り詰める
        rotio_ = Mathf.Clamp01(ratio);
        //UIに反映
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
