using UnityEngine;

public class BowArrow : MonoBehaviour
{
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] AudioClip hitSound;

    float Vyct;  // Установившаяся скорость
    Vector3 V;  // Вектор скорости
    Preferences pref;

    bool isLaunched = false;  // Выпущена ли стрела
    bool isStopped = false;  // Остановлена ли стрела

    float timeToLive = 10;
    float lifeTime = 0;

    void Start() {
        trailRenderer.enabled = false;

        pref = Camera.main.GetComponent<Preferences>();
        Vyct = pref.ArrowSteadyStateSpeed;
    }

    public void Shoot(float V0) {
        transform.parent = null;  // Отделяем стрелу от лука в иерархии

        isLaunched = true;
        trailRenderer.enabled = true;  // Включаем отрисовку пути

        V = transform.forward * V0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isLaunched && collision.gameObject.tag == "Ground") {
            isStopped = true;

            // Проигрываем звук столкновения
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.pitch = Random.Range(0.95f, 1.1f);
            audioSource.PlayOneShot(hitSound);
        }
    }


    void Update() {
        if (isLaunched && !isStopped) {
            MoveArrow();
        }
    }

    void MoveArrow() {
        // Ускорение стрелы
        Vector3 U = V - pref.windForce;
        Vector3 a = pref.g + (-pref.G * U.magnitude * U) / (Vyct*Vyct);

        // Изменение скорости стрелы
        V += a * Time.deltaTime;
 
        // Изменение позиции стрелы
        transform.position += V * Time.deltaTime;

        // Направление стрелы по вектору скорости
        transform.rotation = Quaternion.LookRotation(V);

        // Визуализация сил
        Debug.DrawRay(transform.position, pref.windForce, Color.blue);
        Debug.DrawRay(transform.position, V, Color.red);

        // Уничтожение стрелы
        if (lifeTime > timeToLive)
            Destroy(gameObject);
        else 
            lifeTime += Time.deltaTime;
    }
}
