//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(SpriteRenderer))]
//public class Tower : MonoBehaviour
//{
//    private SpriteRenderer spriteRenderer_;
//    float time_ = 0.0f;
//    [SerializeField] private float shotCoolTime_ = 5.0f;
//    [SerializeField] private Color normalColor_ = Color.white;
//    [SerializeField] private Color changeColor_ = Color.gray;
//    [SerializeField] private Bullet bullet_;

//    public bool isShootReady { get { return time_ <= 0.0f; } }
//    // Start is called before the first frame update
//    void Start()
//    {
//        spriteRenderer_ = GetComponent<SpriteRenderer>();
//        spriteRenderer_.color = normalColor_;
//        time_ = 0.0f;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (time_ > 0.0f)
//        {
//            time_ -= Time.deltaTime;
//        }
//        ColorChange();

//    }

//    private void ColorChange()
//    {
//        float ratio = 1.0f - time_ / shotCoolTime_;
//        spriteRenderer_.color = Color.Lerp(normalColor_, changeColor_, ratio);
//    }

//    private void Shot(Vector3 target)
//    {
//        time_ = shotCoolTime_;
//        spriteRenderer_.color = changeColor_;

//        Bullet bullet = Instantiate(bullet_, transform.position, Quaternion.identity, bullet.SetUp(target));
//    }
//}
