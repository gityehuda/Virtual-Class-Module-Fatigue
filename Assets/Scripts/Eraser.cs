using UnityEngine;
using UnityEngine.UI;

public class Eraser : MonoBehaviour
{
    [SerializeField] WhiteboardManager whiteboardManager; // Field public untuk komponen Whiteboard

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Tambahkan fungsi pada tombol saat diklik
            button.onClick.AddListener(OnEraserButtonClick);
        }
    }

    private void OnEraserButtonClick()
    {
        // Panggil fungsi penghapus pada script Whiteboard
        if (whiteboardManager != null)
        {
            whiteboardManager.ClearCanvas();
        }
    }
}
