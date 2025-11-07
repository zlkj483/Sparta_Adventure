using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    private float camCurYRot;
    public float lookSensitivitiy;
    private Vector2 mouseDelta;
     

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
        mouseDelta = Vector2.zero;
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;
    }

    void CameraLook()
    {
        // x(위,아래)
        camCurXRot -= mouseDelta.y * lookSensitivitiy;   // 마우스 Y 입력을 X축 회전값(camCurXRot) 에 반영
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(camCurXRot, 0, 0);

        // y(좌,우)
        camCurYRot += mouseDelta.x * lookSensitivitiy;
        transform.eulerAngles = new Vector3(0, camCurYRot, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Vector3[] rays = new Vector3[4]
        {
            transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f),
            transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f),
            transform.position + (transform.right * 0.2f) + (transform.up * 0.1f),
            transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f)
        };
        Vector3 dir = (Vector3.down + transform.forward * 0.15f).normalized;

        for (int i = 0; i < rays.Length; i++)
        {
            
            if (Physics.Raycast(rays[i], dir * 0.2f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
