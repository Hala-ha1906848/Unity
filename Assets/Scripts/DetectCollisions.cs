using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] Animator animalAnim;

    // Start is called before the first frame update
    void Start()
    {
        animalAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animalAnim.SetFloat("Speed_f", 0);
    }
    // Check for collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            animalAnim.SetBool("Eat_b", true);
            //animalAnim.SetFloat("Speed_f", 0);

            other.gameObject.SetActive(false);

            StartCoroutine(StopEatAnim());
        }
    }
    // Stop Eat Animation
    private IEnumerator StopEatAnim()
    {
        yield return new WaitForSeconds(5);

        animalAnim.SetBool("Eat_b", false);
    }
}
