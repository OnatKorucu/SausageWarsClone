using UnityEngine;
        
public class PlayerMovementBehaviour : MonoBehaviour
{
    private Touch touch;
    private float speedModifier;
    
    //start is called before the first frame update
    void Start()
    {
        speedModifier = 0.1f;
        Debug.Log("here 2");
    }
            
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount <= 0) return;
        
        Debug.Log("here 3");

        touch = Input.GetTouch(0);
        
        Debug.Log("here 4");

        
        if (touch.phase != TouchPhase.Moved) return;

        Debug.Log("here 1");
        
        transform.position = new Vector3(
            transform.position.x + touch.deltaPosition.x * speedModifier
            , transform.position.y
            ,transform.position.z + touch.deltaPosition.y * speedModifier
        );
    }
}