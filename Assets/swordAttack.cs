using Unity.Cinemachine;
using UnityEngine;

public class swordAttack : MonoBehaviour
{
    public Collider2D swordCollider;

    public float damage = 3;

    Vector2 rightattackOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightattackOffset = transform.localPosition;
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
