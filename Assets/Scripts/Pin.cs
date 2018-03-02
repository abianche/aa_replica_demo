using UnityEngine;

public class Pin : MonoBehaviour
{

    private bool isPinned = false;

    public float speed = 20f;
    public Rigidbody2D rb;

    void Update()
    {
        if (!isPinned)
            rb.MovePosition(rb.position + Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Rotator")
        {
            transform.SetParent(col.transform);

            isPinned = true;
            if (!GameManager.instance.gameHasEnded)
                Score.PinCount++;
        }
        else if (col.tag == "Pin")
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            GameManager.instance.EndGame();
        }
    }

}
