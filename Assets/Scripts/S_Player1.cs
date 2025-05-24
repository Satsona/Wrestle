using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f; // Use this for smooth rotation
    public Transform target;         // The target the player should look at

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Get input
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.W)) vertical = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;

        // Apply movement
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.SimpleMove(move * moveSpeed);

        // Rotate toward target (optional)
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Keep rotation only on Y axis

            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player1 collided with Player2 — loading next scene.");

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No next scene in build settings.");
            }
        }
    }
}
