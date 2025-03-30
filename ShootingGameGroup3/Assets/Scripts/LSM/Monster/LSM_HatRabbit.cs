using UnityEngine;

public class LSM_HatRabbit : MonoBehaviour
{
    public bool hideTime = true;
    private LSM_Monster monsterScript;

    void Awake()
    {
        monsterScript = GetComponent<LSM_Monster>();
    }

    void Start()
    {
        monsterScript.SetInvincibility(true);
        monsterScript.isTracking = false;
        monsterScript.isSmells = false;
    }

    void Update() { }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("SpecialBullet"))
        {
            HatOff();
            Debug.Log("모자 벗기기 성공");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpecialBullet"))
        {
            HatOff();
            Debug.Log("모자 벗기기 성공");
        }
    }

    void HatOff()
    {
        monsterScript.SetInvincibility(false);
        monsterScript.isTracking = true;
    }
}
