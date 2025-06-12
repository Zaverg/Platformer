using UnityEngine;

public class Wallet : MonoBehaviour
{
   [SerializeField] private int _money;

    public void TakeMoney()
    {
        _money += 1;
    }
}
