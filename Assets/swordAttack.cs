using Unity.Cinemachine;
using UnityEngine;

public class swordAttack : MonoBehaviour
{
    public Collider2D swordCollider;

    public float damage = 3;

    Vector2 rightattackOffset;
    Vector2 upAttackOffset;
    Vector2 downAttackOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightattackOffset = transform.localPosition;

        // You can adjust these offsets as needed
        upAttackOffset = new Vector2(0, Mathf.Abs(rightattackOffset.x));   // assume sword is about same distance up
        downAttackOffset = new Vector2(0, -Mathf.Abs(rightattackOffset.x));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackRight()
    {
        swordCollider.enabled = true;

        transform.localPosition = rightattackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightattackOffset.x * -1, rightattackOffset.y);
    }

    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.localPosition = upAttackOffset;
    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
        transform.localPosition = downAttackOffset;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();

            if(enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
