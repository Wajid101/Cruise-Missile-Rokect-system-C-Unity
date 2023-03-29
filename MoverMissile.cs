using UnityEngine;

public class MoverMissile : MonoBehaviour
{
    public float Damping = 3;
    public float Speed = 80;
    public float SpeedMax = 80;
    public float SpeedMult = 1;
    public Vector3 Noise = new Vector3(20, 20, 20);
    public float TargetLockDirection = 0.5f;
    public float timeCount = 0;
    public Rigidbody Obj;
    public New_Weapon instance;


    private void Start()
    {
        timeCount = 0;
        //Destroy (gameObject, LifeTime);
    }

    private void FixedUpdate()
    {

        Obj.velocity = new Vector3(transform.forward.x * Speed * Time.fixedDeltaTime, transform.forward.y * Speed * Time.fixedDeltaTime, transform.forward.z * Speed * Time.fixedDeltaTime);
        Obj.velocity += new Vector3(Random.Range(-Noise.x, Noise.x), Random.Range(-Noise.y, Noise.y), Random.Range(-Noise.z, Noise.z));

        if (Speed < SpeedMax)
        {
            Speed += SpeedMult * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        timeCount = timeCount + Time.deltaTime;

        if (instance.closest && timeCount > 3f)
        {
            Quaternion rotation = Quaternion.LookRotation(instance.closest.transform.position - transform.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
            Vector3 dir = (instance.closest.transform.position - transform.position).normalized;
            float direction = Vector3.Dot(dir, transform.forward);
            if (direction < TargetLockDirection)
            {
                instance.closest = null;
            }
        }

       
    }

}
