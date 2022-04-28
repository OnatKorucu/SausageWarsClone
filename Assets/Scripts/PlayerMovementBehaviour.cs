using UnityEngine;
        
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] [Range(0f, 1f)] private float speedModifier;
    [SerializeField] [Range(0f, 100f)] private float maxVelocity;

    private Rigidbody rigidbody;

    private Touch touch;
    private Vector3 touchStartPosition;
    private Vector2 differenceFromBeginning;

    private Vector3 unclampedVelocity;
    #endregion
    
    #region LifecycleMethods
    //start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
            
    // Update is called once per frame
    void Update()
    {
        ReceiveTouchInputs();
    }
    
    private void FixedUpdate()
    {
        SetPlayerVelocity();
    }
    #endregion
    
    #region OtherMethods
    private void ReceiveTouchInputs()
    {
        if (Input.touchCount <= 0) return;
        
        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
        }

        if (touch.phase == TouchPhase.Stationary)
        {
            differenceFromBeginning.x = touch.position.x - touchStartPosition.x;
            differenceFromBeginning.y = touch.position.y - touchStartPosition.y;
            
            unclampedVelocity.x = differenceFromBeginning.x * speedModifier;
            unclampedVelocity.z = differenceFromBeginning.y * speedModifier;
        }
    }

    private void SetPlayerVelocity()
    {
        rigidbody.velocity = unclampedVelocity.normalized * maxVelocity;
        
        if (!(rigidbody.velocity.magnitude > 0.01f)) return;
        
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
    }
    #endregion
}