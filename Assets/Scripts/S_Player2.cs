using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]
public class S_Player2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f; // Smooth rotation speed
    public Transform target;         // The target to look at

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Arrow key input
        if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) vertical = -1f;

        // Apply movement
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.SimpleMove(move * moveSpeed);

        // Rotate toward target
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f;

            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player1"))
        {
            Debug.Log("CharacterController collided with Player2");

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
