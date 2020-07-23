public class DontDestroyOnLoad : BaseMonoBehaviour
{
    void Start()
    {
		DontDestroyOnLoad(gameObject);
    }
}
