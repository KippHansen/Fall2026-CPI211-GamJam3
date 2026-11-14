using UnityEngine;

public class Sloth_Conveyor_belt : MonoBehaviour
{
    //sets speed of the push
    public float pushSpeed = 4.0f;

//pushes backwards
    public Vector3 pushDirection = new Vector3(0,0,-1);
// on trigger push back 
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            if(cc != null)
            {
                cc.Move(pushDirection.normalized*pushSpeed*Time.deltaTime);
            }
        }
    }

}
