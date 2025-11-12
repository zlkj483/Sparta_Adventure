using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance; // 실제 금고
    public static CharacterManager Instance // 금고 열쇠(외부접근용)
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }
    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    public UIConditions UIConditions;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
            if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UIConditions = FindObjectOfType<UIConditions>();
        if(UIConditions != null && Player != null)
        {
            Player.conditions.uiCon = UIConditions;
            Debug.Log("연결 성공");
        }
        else
        {
            Debug.LogError("연결실패");
        }
    }
}
