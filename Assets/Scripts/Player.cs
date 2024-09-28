using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public GameManager manager; // Reference to the GameManager script
    public Material normalMat;
    public Material phasedMat; 

    [Header("Gameplay")]
    public float bounds = 3f; // Movement bounds for the player
    public float strafeSpeed = 4f; 
    public float phaseCooldown = 2f;

    Renderer mesh; // Reference to the player's mesh renderer
    Collider collision; 
    bool canPhase = true;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>(); // Access the mesh renderer component
        collision = GetComponent<Collider>(); // Access the collider component
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate horizontal movement based on user input
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * strafeSpeed;

        // Update the player's position within movement bounds
        Vector3 position = transform.position;
        position.x += xMove;
        position.x = Mathf.Clamp(position.x, -bounds, bounds); // Clamp the position within the bounds
        transform.position = position;

        // Activate phase ability when Jump button is pressed and it's available
        if (Input.GetButtonDown("Jump") && canPhase)
        {
            canPhase = false; // Prevent further immediate phase activations
            mesh.material = phasedMat; // Set the player's material to the phased material
            collision.enabled = false; // Disable collision for the player
            Invoke("PhaseIn", phaseCooldown); // Invoke the method to phase back in after the cooldown
        }
    }

    // Method to phase the player back in after a cooldown
    void PhaseIn()
    {
        canPhase = true; // Allow the player to phase again
        mesh.material = normalMat; // Set the player's material back to normal
        collision.enabled = true; // Enable collision for the player
    }
}
