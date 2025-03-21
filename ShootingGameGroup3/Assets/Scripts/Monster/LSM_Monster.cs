using UnityEngine;

public class LSM_Monster : MonoBehaviour
{
    public int health = 100;

    public bool invincibility = false;

    void Start() { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invincibility = true;
            Debug.Log("무적상태 활성화");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            invincibility = false;
            Debug.Log("무적상태 비활성화");
        }
    }

    public void Damage(int a)
    {
        if (invincibility == true)
        {
            a = 0;
        }
        health -= a;
        Debug.Log("남은체력은:" + health);
    }
}
