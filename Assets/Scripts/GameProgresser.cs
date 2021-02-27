using UnityEngine;

public class GameProgresser : MonoBehaviour
{
    public CharacterController controller;

    [Range(1f, 30)]
    public float horizontalSpeed = 4f;
    public float gravity = 10f;
    public bool shouldMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            float verticalSpeed = controller.isGrounded ? 0f : gravity;
            controller.Move(new Vector2(horizontalSpeed, -verticalSpeed) * Time.fixedDeltaTime);
        }
    }
}
