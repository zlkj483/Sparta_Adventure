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
                _instance = new CharacterManager();
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

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
            if (_instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
