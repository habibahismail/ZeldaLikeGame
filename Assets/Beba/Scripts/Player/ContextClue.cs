using UnityEngine;

namespace bebaSpace { 
public class ContextClue : MonoBehaviour
{
    [SerializeField] private GameObject contextClue;
    
    private bool contextActive = false;

    public void ChangeContext()
    {
        contextActive = !contextActive;

        if (contextActive)
        {
            contextClue.SetActive(true);
        }
        else
        {
            contextClue.SetActive(false);
        }
    }
  
}
}
