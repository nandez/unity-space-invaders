using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Bullet bulletPrefab;


    private bool bulletSpawned = false;

    private void Update()
    {
        var xMovement = Input.GetAxisRaw("Horizontal");

        if (xMovement > 0)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (xMovement < 0)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }


        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    protected void FireBullet()
    {
        if (!bulletSpawned)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.onDestroy = () => { bulletSpawned = false; };

            bulletSpawned = true;
        }
    }
}