using TMPro;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI interactText; 

    
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position,transform.forward, out var rayCastHit, 10))
        {
            var hitItem = rayCastHit.transform.GetComponent<ItemInteraction>();
            if (!hitItem)
            {
                HideOption();
                return;
            }

            ShowDisplayOption(hitItem);
            if(Input.GetKeyDown(KeyCode.E))
                hitItem.Execute();
        }
    }

    private void HideOption()
    {
        interactText.text = "";
        interactText.gameObject.SetActive(false);
    }

    private void ShowDisplayOption(ItemInteraction hitItemInteraction)
    {
        interactText.text = hitItemInteraction.interactionName;
        interactText.gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward*10);
    }
}
