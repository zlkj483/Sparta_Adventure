# Sparta_Adventure 개인 프로젝트

---

## 🎮 게임 소개
-**WASD로 이동 및 Space Bar로 점프 가능(2단점프까지)**
-**마우스 방향에 따라 카메라가 움직임)**
-**맵에 있는 점프대(선풍기)를 밟으면 위로 높이 튀어오름**
-**HP는 일정 시간마다 줄어들며 맵 곳곳에 있는 햄버거에 닿을 경우 30의 HP를 회복하고 20초 후에 햄버거가 재생성됨**

<br>

---

## 🧩 주요 기능 코드
<br>

### 이동 및 점프
<br>

```
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public int jumpCount;
    public int maxJumpCount;
}    
```
<br>

```
    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;
    }
```
<br>

```
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
```
<br>

```
    public void OnJump(InputAction.CallbackContext context)
    {
        //if(context.phase == InputActionPhase.Started && IsGrounded())
        if (context.phase == InputActionPhase.Started && maxJumpCount > jumpCount)
        {
            jumpCount++;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        }
    }
```
<br>

```
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
```
<br>

```
    void FixedUpdate()
    {
        Move();
        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }
```
<br>

---

### 카메라 무빙(플레이어의 시선)
<br>

```
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    private float camCurYRot;
    public float lookSensitivitiy;
    private Vector2 mouseDelta;
```
<br>

```
    void CameraLook()
    {
        // x(위,아래)
        camCurXRot -= mouseDelta.y * lookSensitivitiy;   // 마우스 Y 입력을 X축 회전값(camCurXRot) 에 반영
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(camCurXRot, 0, 0);

        // y(좌,우)
        camCurYRot += mouseDelta.x * lookSensitivitiy;
        transform.eulerAngles = new Vector3(0, camCurYRot, 0);
```
<br>

```
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
```
<br>

```
    private void LateUpdate()
    {
        CameraLook();
        mouseDelta = Vector2.zero;
    }
```
<br>

---

### 회복 로직과 햄버거로 인한 체력 회복과 일정시간 후 햄버거 재생성
<br>

```
    public void Heal(float amount)
    {
        if (Hp != null)
        {
            Hp.Add(amount);
        }
    }
```
<br>

```
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerConditions playerConditions = other.GetComponent<PlayerConditions>();
            if (playerConditions != null)
            {
                playerConditions.Heal(healAmount);

                StartCoroutine(ResTimeCoroutine());
            }
            else
            {
                Debug.LogError("컴퍼넌트 로드 실패");
            }

        }
    }
```
<br>

```
    private IEnumerator ResTimeCoroutine()
    {
        col.enabled = false;
        meshRen.enabled = false;
        yield return new WaitForSeconds(resTime);
        col.enabled = true;
        meshRen.enabled = true;
    }
```
<br>

---

### 아이템 감지 및 아이템 정보 UI띄우기
<br>

```
public class Interaction : MonoBehaviour
{
    private IInteractable curInteractable;
    public GameObject curInteractGameObject;

    //public Transform rayStartPoint;
    public float interDistance = 3f;
    public LayerMask itemLayer;
    //public GameObject potionInfoUI;
    public TextMeshProUGUI promptText;

    void Update()
    {
        CheckForItem();
    }

    void CheckForItem()
    {
        Camera mainCam = Camera.main;
        if(promptText == null || mainCam == null) return;

        RaycastHit rayHit;
        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;
        Vector3 boxExtent = new Vector3(0.7f, 0.7f, 0.7f);
        Quaternion orientation = mainCam.transform.rotation;
        if (Physics.BoxCast(origin, boxExtent, direction, out rayHit, orientation, interDistance, itemLayer)) // BoxCast 실행: 상자(boxExtents)를 방향(direction)으로 쏘기
        {
            if (rayHit.collider.gameObject != curInteractGameObject)
            {
                // 감지됨
                curInteractGameObject = rayHit.collider.gameObject;
                curInteractable = rayHit.collider.GetComponent<IInteractable>();
                /*if(potionInfoUI != null)
                {
                  potionInfoUI.SetActive(true);
                }*/
                SetPromptText();
            }
        }
        else
        {
            //감지 X
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
            // potionInfoUI.SetActive(false) ;
        }
        Debug.DrawRay(origin, direction * interDistance, Color.yellow);
    }
    private void SetPromptText()
    {
        if (curInteractable != null && promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
        else
        {
            // BoxCast는 성공했으나 IInteractable이 없는 경우
            promptText.gameObject.SetActive(false);
        }
    }
}
```
<br>

```
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;

}
```
<br>

```
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string info = $"{data.itemName}\n {data.description}";
        return info ;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
    }
}
```
<br>

### 점프대 로직
<br>

```
public class JumpPad : MonoBehaviour
{
    public float jumpPadForce = 170f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(Vector3.up *  jumpPadForce, ForceMode.Impulse);
            }
        }
    }
}
```

---

## ⚙️ 개발 환경 및 기술 스택
- **엔진** : Unity 2022.3.62f2
- **언어** : C#
- **관리** : Git / GitHub
<br>
<br>














