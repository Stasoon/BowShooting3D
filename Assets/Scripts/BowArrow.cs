using System.Collections;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    [SerializeField] float mass = 4f;
    const float G = 9.81f;

    float timeToLive = 50f;

    bool isStopped = false;

    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] AudioClip hitSound;

    Transform _arrowBody;

    void Start() {
        if (trailRenderer == null) {
            TryGetComponent<TrailRenderer>(out trailRenderer);
        }
        trailRenderer.enabled = false;

        _arrowBody = transform.GetChild(0).GetComponent<Transform>();
    }

    public void Shoot(float velocity) {
        trailRenderer.enabled = true;
        Vector3 startPos = transform.position;
        StartCoroutine(SimulateArrowMotion(velocity, startPos));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") {
            isStopped = true;

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.pitch = Random.Range(0.95f, 1.1f);
            audioSource.PlayOneShot(hitSound);
        }
    }

    IEnumerator SimulateArrowMotion(float velocity, Vector3 initialPosition)
    {
        transform.parent = null;
        float time = 0f;

        float y0 = initialPosition.y; // Начальная высота стрелы
        float v0y = transform.forward.y * velocity;

        while (time < timeToLive && isStopped == false)
        {
            float x = initialPosition.x + transform.forward.x * velocity * time;
            float y = y0 + v0y * time - 0.5f * G * time * time;
            float z = initialPosition.z + transform.forward.z * velocity * time;

            transform.localPosition = new Vector3(x, y, z);
            //_arrowBody.rotation = Quaternion.Euler(new Vector3(0, y, 0));

            // Увеличиваем время
            time += Time.deltaTime;

            yield return null;
        }

        if (!isStopped)
            Destroy(gameObject);
    }
}
