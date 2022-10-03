using UnityEngine;
using Random = UnityEngine.Random;

public enum UpgradeTypes{
    HealthIncrease, TripleShoot, DoubleJump, HealthDecrease, Default
}

public class UpgradeBox : MonoBehaviour
{
    [SerializeField] private float boxDestroyTime;

    public UpgradeTypes currentState;

    private void Awake()
    {
        int rand = Random.Range(1, 5);
        
        switch (rand)
        {
            case 1:
                currentState = UpgradeTypes.HealthIncrease;
                break;
            case 2:
                currentState = UpgradeTypes.TripleShoot;
                break;
            case 3:
                currentState = UpgradeTypes.DoubleJump;
                break;
            case 4:
                currentState = UpgradeTypes.HealthDecrease;
                break;
            default:
                currentState = UpgradeTypes.Default;
                break;
        }
    }
    
    public void DestroyBox()
    {
        Destroy(gameObject, boxDestroyTime);
    }
}
