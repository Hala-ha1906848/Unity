using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;

    [SerializeField] float xRotation;
    [SerializeField] float yRotation;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    [SerializeField] Animator playerAnim;
    [SerializeField] Rigidbody playerRb;

    //[SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject pooledProjectile;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        ApplyAnimation();

        FeedAnimal();
    }

    void MovePlayer() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        //playerRb.MovePosition(movementDirection);
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void ApplyAnimation() 
    {
        if ((horizontalInput != 0) || (verticalInput != 0))
        {
            playerRb.constraints = RigidbodyConstraints.None;
            playerAnim.SetBool("Static_b", false);
            playerAnim.SetFloat("Speed_f", speed);
        }
        else if ((horizontalInput == 0) || (verticalInput == 0))
        {
            playerRb.constraints = RigidbodyConstraints.FreezePosition;
            playerAnim.SetBool("Static_b", true);
            playerAnim.SetFloat("Speed_f", 0);
        }
    }

    void FeedAnimal() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            }
        }
    }
}
