using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Conts.WheatTypes.GoldWheat))
        {
            other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();
        }

        if (other.CompareTag(Conts.WheatTypes.HolyWheat))
        {
            other.gameObject?.GetComponent<HolyWheatCollectible>().Collect();
        }

        if (other.CompareTag(Conts.WheatTypes.RottenWheat))
        {
            other.gameObject?.GetComponent<RottenWheatCollectible>().Collect();
        }
    }
}
