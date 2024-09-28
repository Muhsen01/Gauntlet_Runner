using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public GameManager manager; // Reference to the GameManager script
    public float moveSpeed = 20f; // Speed at which the object moves
    public float timeAmount = 1.5f; // Amount of time to adjust when collided

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object along the Z-axis based on moveSpeed
        transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
    }

    // OnTriggerEnter is called when the object collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.tag == "Player")
        {
            // Adjust the game time using the GameManager's AdjustTime method
            manager.AdjustTime(timeAmount);
            Destroy(gameObject); // Destroy the collidable object after collision
        }
    }
}
