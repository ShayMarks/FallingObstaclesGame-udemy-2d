using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static float fallSpeedGlobal = 5f; // מהירות נפילה גלובלית של המכשולים
    private float rotationSpeed; // מהירות הסיבוב
    private Vector3 rotationAxis; // ציר הסיבוב

    void Start()
    {
        // הגדרת מהירות סיבוב רנדומלית
        rotationSpeed = Random.Range(-200f, 200f); // בין -200 ל-200 מעלות בשנייה
        rotationAxis = Vector3.forward; // סיבוב סביב ציר ה-Z

        // הגדרת גודל רנדומלי
        float randomScale = Random.Range(0.5f, 1.5f); // בין 0.5 ל-1.5
        transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }

    void Update()
    {
        // תנועה כלפי מטה
        transform.Translate(Vector3.down * fallSpeedGlobal * Time.deltaTime, Space.World);

        // סיבוב המכשול
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);

        // בדיקה אם המכשול יצא מהמסך
        if (transform.position.y < -6f)
        {
            // הגדלת הניקוד
            GameManager.instance.AddScore();

            // השמדת המכשול
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // השמדת המכשול
            Destroy(gameObject);

            // קריאה לפונקציית GameOver ב-GameManager
            GameManager.instance.GameOver();
        }
    }
}
