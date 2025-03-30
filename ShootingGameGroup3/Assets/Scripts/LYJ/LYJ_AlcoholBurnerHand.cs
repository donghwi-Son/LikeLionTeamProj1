using UnityEngine;

public class LYJ_AlcoholBurnerHand : MonoBehaviour
{
    [SerializeField]
    GameObject burner;

    public void ThrowBurner()
    {
        LYJ_PoolManager.Instance.GetGameObject(burner);
    }

}
