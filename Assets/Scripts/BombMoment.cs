using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMoment : QuocBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Xử lý khi va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem va chạm có xảy ra với đối tượng người chơi
        if (collision.gameObject.CompareTag("Player"))
        {
            // Đẩy bom đi (ví dụ: bằng cách thay đổi vận tốc của bom)
            Vector3 pushDirection = transform.position - collision.transform.position;
            rb.AddForce(pushDirection.normalized * 10f, ForceMode2D.Impulse);

            // Xử lý các tác động khác khi người chơi va chạm với bom
            // (ví dụ: mất điểm, kết thúc trò chơi, v.v.)
        }
    }
}
