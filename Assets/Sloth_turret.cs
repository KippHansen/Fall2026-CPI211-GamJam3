using UnityEngine;

public class Sloth_turret : MonoBehaviour
{
    //the thing were shooting out
    public GameObject projectile;
    //where
    public Transform aim;
    //how often
    public float firerate = 3.0f;
    // how fast
    public float projectileSpeed = 1f;

    public float cooldown=0;

    void Update()
    {
        
       if (Time.time >= cooldown)
        {
            Shoot();
            //reset fire time
            Debug.Log("cooldown on");
            cooldown = Time.time + firerate;
        }


    }

    void Shoot()
    {
        // creates bullet with prefab, the shooting point and direction
    GameObject bullet = Instantiate(projectile, aim.position,aim.rotation);

    Rigidbody rb= bullet.GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.Log("No RigidBody");

        }
        rb.useGravity = false;
        rb.isKinematic=false;

            Debug.Log("Applying speed!");
            rb.linearVelocity=aim.forward*projectileSpeed;
                
    }

}