using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float slowMoveSpeed;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPlaying)
        {
            float dirX = Input.GetAxisRaw("Horizontal");
            float dirY = Input.GetAxisRaw("Vertical");
            Vector2 direction = new Vector2(dirX, dirY);
            Move(direction.normalized);
        }       
    }

    void Move(Vector2 direction)
    {
        float speed = ObjectHandleManager.Instance.ObjectInHand == null ? moveSpeed : slowMoveSpeed;
        rb.linearVelocity = direction * speed;
    }
}
