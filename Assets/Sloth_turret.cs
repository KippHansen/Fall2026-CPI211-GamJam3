using UnityEngine;

public class Sloth_turret : MonoBehaviour
{
    //the thing were shooting out
    public GameObject bullet;
    //where
    public Transform aim;
    //how often
    public float firerate = 3.0f;
    // how fast
    public float projectileSpeed = 20f;

    public float cooldown;

    void Update()
    {
        
       if (Time.time >= cooldown)
        {
            Shoot();
            //reset fire time
            cooldown = Time.time + firerate;
        }


    }

    void Shoot()
    {
    //GameObject bullet = Instantiate(bullet, aim.position,aim.rotation);

    Rigidbody rb= bullet.GetComponent<Rigidbody>();
    if(rb!=null)
        {
            rb.linearVelocity=aim.forward*projectileSpeed;
        }        
    }

}