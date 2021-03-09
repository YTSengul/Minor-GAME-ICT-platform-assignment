using System.Collections;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private float RgbRotationSpeed = 3f;
    private float ChaseSpeed = 3f;
    [SerializeField] private DistanceToPlayer distanceFromPlayer;
    private int lockOnPlayerTime = 5;

    private GameObject player;
    private Transform localTrans;
    private Transform TargetTrans; 
    private Vector3 targetPos;
    private Rigidbody localRgb;
    private float distanceBetween;
    private bool targetLock = false;
    private GameObject newColliderGamer;

    // Start is called before the first frame update
    void Start()
    {
        localRgb = GetComponent<Rigidbody>();
        localTrans = GetComponent<Transform>();

        player = GameObject.Find("player");
        TargetTrans = player.GetComponent<Transform>();
        
        newColliderGamer = new GameObject();
        newColliderGamer.SetActive(false);
        newColliderGamer.AddComponent(typeof(CapsuleCollider));
        newColliderGamer.GetComponent<Collider>().isTrigger = true;
        newColliderGamer.gameObject.tag = "playerGravityInversion";
        newColliderGamer.transform.localScale = new Vector3(9, 9, 9);
    }

    private void FixedUpdate()
    {
        if(targetLock == false)
        {
            Chase(TargetTrans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLock == false)
        {
            if (enemyGotCloseEnough())
            {
                newColliderGamer.SetActive(true);
                newColliderGamer.transform.position = player.transform.position;
                StartCoroutine(coroutineLockedPlayer());
            }
            else
            {
                if (newColliderGamer.active == true)
                {
                    newColliderGamer.SetActive(false);
                }
            }
        }
    }

    IEnumerator coroutineLockedPlayer()
    {
        targetLock = true;
        yield return new WaitForSeconds(lockOnPlayerTime);
        newColliderGamer.SetActive(false);
        targetLock = false;
    }

    void Chase(Transform _target)
    {
        var speed = ChaseSpeed;

        targetPos = _target.position;
        targetPos.y = localTrans.position.y;
        
        RotateRgb(_target);
        localRgb.MovePosition(localRgb.position + localTrans.forward * speed * Time.deltaTime);
    }


    private void RotateRgb(Transform _target)
    {
        Vector3 localTarget = localTrans.InverseTransformPoint(_target.position);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime * RgbRotationSpeed);
        localRgb.MoveRotation(localRgb.rotation * deltaRotation);
    }

    private bool enemyGotCloseEnough()
    {
        distanceBetween = Vector3.Distance(this.transform.position, player.transform.position);
        if (distanceBetween < distanceFromPlayer.distance)
        {
            return true;
        }
        return false;
    }
}