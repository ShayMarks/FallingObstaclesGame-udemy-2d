using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float xBoundary = 8f;
    public Sprite[] skins;

    void Start()
    {
        // טעינת הסקין הנבחר
        int selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        if (selectedSkin < skins.Length)
        {
            GetComponent<SpriteRenderer>().sprite = skins[selectedSkin];
        }
        else
        {
            Debug.LogError("Selected skin index out of range.");
        }
    }

    void Update()
    {
        // בדיקה אם יש נגיעה במסך
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            // מזיז את השחקן למיקום הנגיעה
            transform.position = new Vector3(touchPosition.x, transform.position.y, 0f);
        }
        else if (Input.GetMouseButton(0)) // עבור בדיקות במחשב
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        }

        // הגבלת תנועת השחקן בגבולות המסך
        float clampedX = Mathf.Clamp(transform.position.x, -xBoundary, xBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            // השחקן נפגע - מעבר למסך הסיום
            // GameManager.instance.GameOver(); // הוסר כדי למנוע קריאה כפולה
        }
        // הסרנו את הטיפול בהתנגשות עם המטבעות מהשחקן
    }
}
