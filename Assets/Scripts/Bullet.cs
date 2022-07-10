using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Normal Turret Values")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 1f;

    [Header("Explosive Turret Values")]
    [SerializeField] private float explosionRange = 0f;

    [Header("Debuff Turret Values")]
    [SerializeField] private float debuffTime = 0f;
    [SerializeField] private float timeBeforeDestroy = 0.3f;

    private GameObject _target = null;

    // Setting the damage of the bullet
    public void SetDamage(float pDamage)
    {
        damage = pDamage;
    }

    // Setting the target of the bullet
    public void SetTarget(GameObject pTarget)
    {
        _target = pTarget;
    }

    private void Update()
    {
        // If no input is allowed, return
        if (GameManager.instance != null)
            if (!GameManager.instance.inputEnabled)
                return;

        // If the target is null, destroy the bullet and return
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Setting the direction of the bullet and the distance it will travel each (this) frame
        Vector2 direction = _target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If the length of the direction if equal or smaller then the distance this frame, hit the enemy
        if (direction.magnitude <= distanceThisFrame || debuffTime > 0f)
        {
            DetectHit();
            return;
        }

        Move(direction, distanceThisFrame);
        FaceTarget();
    }

    private void Move(Vector2 pDirection, float pDistanceThisFrame)
    {
        // Move the bullet to the target
        transform.Translate(pDirection.normalized * pDistanceThisFrame, Space.World);
    }

    private void FaceTarget()
    {
        // Rotate the bullet to face the target
        Vector3 dir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void DetectHit()
    {
        // If the explosion range is bigger than 0 and the debuff time is 0 or less, call Explode function
        if (explosionRange > 0f && debuffTime <= 0f)
            Explode();
        // If the explosion range is bigger than 0 and the debuff time is bigger than 0, call Debuff function
        else if (explosionRange > 0f && debuffTime > 0f)
            Debuff();
        // If all things mentioned earlier are not true, call Damage function
        else
            DamageTarget(_target);

        // If the debuff time is bigger then 0, destroy the bullet after the animation time and return
        if (debuffTime > 0f)
        {
            Destroy(gameObject, timeBeforeDestroy);
            return;
        }
        // If above condition is false, just destroy the bullet immediately and return
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Do damage to the target with the given damage if the BasicEnemy script is located on the target
    private void DamageTarget(GameObject pTarget)
    {
        if (pTarget.GetComponent<BasicEnemy>() != null)
            pTarget.GetComponent<BasicEnemy>().DoDamageToEnemy(damage);
    }

    private void Explode()
    {
        // Add all the colliders that are in the explosion range to the colliders array
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        // Damage each enemy that are in the colliders list
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
                DamageTarget(collider.gameObject);
        }
    }

    private void Debuff()
    {
        // Add all the colliders that are in the explosion range to the colliders array
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        // Enable the debuf for each enemy that is in the colliders list
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                if (collider.gameObject.GetComponent<DebuffBehaviour>() != null)
                    collider.gameObject.GetComponent<DebuffBehaviour>().ExecuteDebuff(debuffTime);
            }
        }
    }

    // Show the explosion range if the bullet is selected in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
