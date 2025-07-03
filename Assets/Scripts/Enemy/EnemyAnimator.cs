using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator Enemyanim_Controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("EnemyAnimator Start");
        Enemyanim_Controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
