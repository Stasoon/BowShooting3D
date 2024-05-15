using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rotationPlayer;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float sensivity = 2.0f;
    float maxYAngle = 90.0f;

    float rotationX, rotationY;
    bool isJump;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Move() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //transform.Rotate(Vector3.up * mouseX * sensivity);
        
        rotationX -= mouseY * sensivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        rotationY += mouseX * sensivity;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        //transform.rotation = Quaternion.Euler(0f, Input.GetAxis("Mouse Y"), 0.0f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

        ///transform.localRotation = Quaternion.Euler(0f, Input.GetAxis("Mouse Y"), 0.0f);
        //Debug.Log(rotationPlayer.rotation);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, z);
        float velocity = moveSpeed * Time.deltaTime;

        transform.Translate(direction * velocity);
    }
    IEnumerator Jump(){
        isJump = true;
        float hightJump = 1.5f;
        for (int i = 0; i < 20; i++){
            transform.position = new Vector3(transform.position.x, transform.position.y + hightJump / 20, transform.position.z);
            yield return new WaitForSeconds(0.2f / 20);
        }
        yield return new WaitForSeconds(0.2f / 15);
        for (int i = 0; i < 20; i++){
            transform.position = new Vector3(transform.position.x, transform.position.y - hightJump / 20, transform.position.z);
            yield return new WaitForSeconds(0.15f / 20);
        }
        
        
        isJump = false;

        yield return null;
    }
    // Update is called once per frame
    void Update() {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) & !isJump){
            StartCoroutine(Jump());
            
        }
    }
}