using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0, z);
        float velocity = moveSpeed * Time.deltaTime;

        transform.Translate(direction * velocity);
    }

    // Update is called once per frame
    void Update() {
        Move();
    }
}
