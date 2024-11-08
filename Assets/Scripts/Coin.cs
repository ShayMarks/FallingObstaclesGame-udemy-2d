using UnityEngine;

public class Coin : MonoBehaviour
{
    public static float fallSpeedGlobal = 5f; // מהירות נפילה גלובלית של המטבעות

    void Update()
    {
        // תנועה כלפי מטה
        transform.Translate(Vector3.down * fallSpeedGlobal * Time.deltaTime, Space.World);

        // בדיקה אם המטבע יצא מהמסך
        if (transform.position.y < -6f)
        {
            // השמדת המטבע
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // השחקן אסף מטבע
            GameManager.instance.AddCoin();

            // השמדת המטבע
            Destroy(gameObject);
        }
    }
}
