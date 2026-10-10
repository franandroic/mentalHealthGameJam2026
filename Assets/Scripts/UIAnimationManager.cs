using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
    private GameObject Scroll;
    [SerializeField] Animator animator;
    public bool isManager = false;

    private bool isOpen = false;

    private void Awake()
    {
        Scroll = GameObject.Find("Wrapper");
        if (isManager)
        {
            Debug.Log("closedUI");
            Scroll.SetActive(false);
        }
    }

    public void OpenScroll()
    {
        isOpen = true;
        Scroll.SetActive(true);
        animator.SetBool("Start", true);
    }



    public void DisableScroll()
    {
        animator.SetBool("Start", false);
        Scroll.SetActive(false);
        isOpen = false;
    }




    private void Update()
    {
        // OpenScroll()
    }
}
