using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float maxLifeTimer_ = 1;
    [SerializeField] private float time_ = 0;
    [SerializeField] Vector3 maxScale_ = new Vector3(3, 3, 3);
    public int chainNum = 0;//�A�����Ă��鐔
    Renderer renderer;
    Color begin =Color.yellow;//�ŏ��̐F
    Color end = new Color(1, 0.92f, 0.016f, 0);//�Ō�̐F
    float frame = 0;//�t���[��
    // Start is called before the first frame update
    void Start()
    {
        time_ = maxLifeTimer_;
        renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = begin;
    }

    // Update is called once per frame
    void Update()
    {
        time_ -= Time.deltaTime;
        ScaleUp();
        if (time_ > 0)
        {
            return;
        }
        Blend();
    }

    protected virtual void ScaleUp()
    {
        transform.localScale = maxScale_ * (1.0f - time_ / maxLifeTimer_);
    }

    protected virtual void Blend()
    {
        frame += Time.deltaTime * 3;
        renderer.material.color = Color.Lerp(begin, end, frame);
        if (renderer.material.color == end)
        {
            gameObject.SetActive(false);
        }
    }
}
