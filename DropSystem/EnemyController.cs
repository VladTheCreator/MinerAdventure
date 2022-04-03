using UnityEngine;

public class EnemyController : Drop
{
    private Mover mover;
    [SerializeField] private float speed;
    private float castDistance = 0.7f;
    [SerializeField] private float castDistanceForWall;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallMask;
    private float attackCooldown;
    [SerializeField] private float startAttackCooldown;
    private void Awake()
    {

    }
    public override string GetName()
    {
        return "enemy";
    }
    public override int GetDropChance()
    {
        return 30;
    }
    private void Update()
    {
        if (CloseToTheWall())
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        CastForPlayer();
    }
    private bool CloseToTheWall()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right,
            castDistanceForWall, wallMask);
        return hitRight;
    }
    private void CastForPlayer()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, castDistance, playerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, castDistance, playerMask);
        if (hitRight)
        {
            if (attackCooldown <= 0)
            {
                hitRight.collider.GetComponent<Health>().TakeOneDamage(name);
                attackCooldown = startAttackCooldown;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
        else if (hitLeft)
        {
            if (attackCooldown <= 0)
            {
                hitLeft.collider.GetComponent<Health>().TakeOneDamage(name);
                attackCooldown = startAttackCooldown;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.right);
    }

}
