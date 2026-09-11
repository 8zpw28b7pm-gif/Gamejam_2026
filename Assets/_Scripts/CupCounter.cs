using TMPro;
using UnityEngine;

public class CupCounter : MonoBehaviour
{
    public static CupCounter Instance;
    [SerializeField] TextMeshProUGUI cupCountText;

    private int cupCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cupCountText.text = cupCount.ToString();
    }

    public void RegisterCup()
    {
        cupCount++;
        cupCountText.text = cupCount.ToString();
    }

    public void DeregisterCup()
    {
        cupCount--;
        cupCountText.text = cupCount.ToString();
    }
}
