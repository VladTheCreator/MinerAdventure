using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float horisontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private AudioClip jump;
    //bool grounded = false;
    public event System.Action<bool> flip;
    public bool facingRight;

    private float previouseHorizontalInput = -1;

    private float currentHorizontalInputRaw;
    public float CurrentHorizontalInputRaw => currentHorizontalInputRaw;
    private AudioSource audioSource;
    private void Awake()
    {
        facingRight = false;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetXVelocityToZero()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    public void ToggleSimulation()
    {
        rb.simulated = !rb.simulated;
    }
    public void RunRL(float input, float rawInput)
    {
        currentHorizontalInputRaw = rawInput;
        transform.position += new Vector3(input * horisontalSpeed * Time.deltaTime, 0);
        Flip(input);
    }
    private void Flip(float input)
    {
        if (input > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180,
                transform.rotation.z);
            facingRight = true;
        }
        else if (input < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0,
               transform.rotation.z);
            facingRight = false;
        }
        if ((previouseHorizontalInput > 0 && input < 0)
            || (previouseHorizontalInput < 0 && input > 0))
        {
            flip?.Invoke(facingRight);
            previouseHorizontalInput = input;
        }
    }
    public void Jump(Rigidbody2D pickaxRb)
    {
        pickaxRb.simulated = false;
        if (Grounded())
        {
            rb.AddForce(new Vector3(0, verticalSpeed), ForceMode2D.Impulse);
            audioSource.clip = jump;
            audioSource.Play();
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Diamond"
    //        || collision.gameObject.tag == "Enemy")
    //    {
    //        grounded = true;
    //    }
    //}
    public bool Grounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.down, 1);
        Debug.Log(hits.Length);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag == "Ground"
                || hit.collider.gameObject.tag == "Diamond"
                || hit.collider.gameObject.tag == "Enemy")
            {
                return true;
            }
        }
        return false;
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //    if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Diamond"
    //        || collision.gameObject.tag == "Enemy")
    //    {
    //        grounded = false;
    //    }
    //}
}
