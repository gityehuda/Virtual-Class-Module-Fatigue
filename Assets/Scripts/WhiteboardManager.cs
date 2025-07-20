using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using static CustomClass;
using System.Linq;

public class WhiteboardManager : MonoBehaviour
{
    public static WhiteboardManager instance;
    private bool isDrawing = false;
    private Vector2 lastMousePos;
    private Vector3 lastMousePosition;
    private Color lineColor;

    public GameObject whiteboardCanvas;
    public float brushSize = 20f;
    float brushSizeOri = 20f;
    public bool isWhiteboardActive = false;

    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] RectTransform rt;
    public RawImage ri;
    [HideInInspector] public Vector3 bottomLeft = Vector3.zero;
    [HideInInspector] public Vector3 topRight = Vector3.zero;
    [HideInInspector] public Texture2D canvas;
    [HideInInspector] public MyWhiteboard whiteBoardNow;
    [HideInInspector] public List<MyWhiteboard> whiteboards;

    [SerializeField] GetWhiteboardCanvasCorner getWhiteboardCanvasCorner;

    int width = 0;
    int height = 0;
    List<CustomClass.WhiteboardDataModels> data;
    [HideInInspector] public Dictionary<Guid, WhiteboardDataModels[]> allDataWhiteboards = new Dictionary<Guid, WhiteboardDataModels[]>();

    [Header("All Buttons in Canvas")]
    [SerializeField] Button buttonCloseWhiteboard;
    [SerializeField] Button buttonUndoWhiteboard;
    [SerializeField] Button buttonEraserWhiteboard;
    [SerializeField] Button buttonClearWhiteboard;

    List<UndoModels> myData;

    bool isEraser = false;

    void Awake()
    {
        instance = this;
        getWhiteboardCanvasCorner.GetWorldCorners();
    }

    private void Start()
    {
        myData = new List<UndoModels>();
        buttonCloseWhiteboard.onClick.AddListener(delegate
        {
            CloseWhiteboards();
        });

        buttonUndoWhiteboard.onClick.AddListener(delegate
        {
            UndoWhiteboards();
        });

        buttonEraserWhiteboard.onClick.AddListener(delegate
        {
            EraserWhiteboards();
        });
    }

    void Update()
    {
        if(isWhiteboardActive && whiteboardCanvas.activeSelf)
        {
            getWhiteboardCanvasCorner.GetWorldCorners();
            if (IsMouseOverCanvas())
            {
                if (rt != null && ri != null)
                {
                    HandleInput();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (data != null)
                {
                    if (data.Count != 0)
                    {
                        var idData = Guid.NewGuid();

                        if(!isEraser) myData.Add(new UndoModels(idData,whiteBoardNow.whichWhiteboard, data));

                        PhotonManager.instance.SendData((byte)CustomClass.TypeData.Whiteboard, new DataWhiteboard(idData, data.ToArray()), Photon.Realtime.ReceiverGroup.Others, Photon.Realtime.EventCaching.DoNotCache);

                        allDataWhiteboards.Add(idData, data.ToArray());

                        data = new List<WhiteboardDataModels>();

                    }
                }
                isDrawing = false;
                return;
            }
        }
    }

    bool IsMouseOverCanvas()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        if (mousePosition.x >= corners[0].x && mousePosition.x <= corners[2].x &&
            mousePosition.y >= corners[0].y && mousePosition.y <= corners[2].y)
        {
            return true;
        }

        return false;
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            data = new List<CustomClass.WhiteboardDataModels>();
            isDrawing = true;
            lastMousePosition = Input.mousePosition;
            lastMousePos = ConvertMousePosition(Input.mousePosition);

            lineColor = isEraser ? Color.white : Color.black;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(data != null)
            {
                if (data.Count != 0)
                {
                    var idData = Guid.NewGuid();

                    if (!isEraser)
                    {
                        myData.Add(new UndoModels(idData, whiteBoardNow.whichWhiteboard, data));
                    }

                    allDataWhiteboards.Add(idData, data.ToArray());

                    PhotonManager.instance.SendData((byte)CustomClass.TypeData.Whiteboard, new DataWhiteboard(idData, data.ToArray()), Photon.Realtime.ReceiverGroup.Others, Photon.Realtime.EventCaching.DoNotCache);

                    data = new List<WhiteboardDataModels>();
                }
            }
            isDrawing = false;

            return;
        }

        if (isDrawing)
        {
            Vector2 mousePos = ConvertMousePosition(Input.mousePosition);

            float mouseDeadZone = 0.1f;

            if (Vector2.Distance(lastMousePos, mousePos) >= mouseDeadZone)
            {
                var whiteboardData = new CustomClass.WhiteboardDataModels(lastMousePosition.x / Screen.width, lastMousePosition.y / Screen.height, Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, lineColor.r, lineColor.g, lineColor.b, lineColor.a, bottomLeft.x, bottomLeft.y, bottomLeft.z, topRight.x, topRight.y, topRight.z, whiteBoardNow.whichWhiteboard, isEraser);

                data.Add(whiteboardData);

                DrawLine(lastMousePos, mousePos, lineColor, whiteBoardNow.whichWhiteboard, brushSize);
                lastMousePos = mousePos;
                lastMousePosition = Input.mousePosition;
            }
        }
    }

    public void HandleWhiteBoard(CustomClass.DataWhiteboard data)
    {
        List<int> tempWhiteboard = new List<int>();
        foreach (var whiteboard in data.whiteboardData)
        {
            if(tempWhiteboard.Contains(whiteboard.whichWhiteboard) == false)tempWhiteboard.Add(whiteboard.whichWhiteboard);
            WhiteboardManager.instance.DrawLineFromOnline(new Vector2(whiteboard.startX, whiteboard.startY), new Vector2(whiteboard.endX, whiteboard.endY), new Color(whiteboard.colorR, whiteboard.colorG, whiteboard.colorB, whiteboard.colorA), whiteboard.whichWhiteboard, whiteboard.isEraser);
        }
        foreach (var whiteboard in tempWhiteboard)
        {
            whiteboards[whiteboard - 1].myTexture.Apply();
        }
        allDataWhiteboards.Add(data.idData, data.whiteboardData.ToArray());
    }

    public void HandleSyncWhiteboard(KeyValuePair<Guid, WhiteboardDataModels[]> data)
    {
        if (allDataWhiteboards.ContainsKey(data.Key)) return;
        List<int> tempWhiteboard = new List<int>();
        foreach (var whiteboard in data.Value)
        {
            if (tempWhiteboard.Contains(whiteboard.whichWhiteboard) == false) tempWhiteboard.Add(whiteboard.whichWhiteboard);
            WhiteboardManager.instance.DrawLineFromOnline(new Vector2(whiteboard.startX, whiteboard.startY), new Vector2(whiteboard.endX, whiteboard.endY), new Color(whiteboard.colorR, whiteboard.colorG, whiteboard.colorB, whiteboard.colorA), whiteboard.whichWhiteboard, whiteboard.isEraser);
        }
        foreach (var whiteboard in tempWhiteboard)
        {
            whiteboards[whiteboard - 1].myTexture.Apply();
        }
    }

    public void DrawLineFromOnline(Vector2 lastMouse, Vector2 nowMouse, Color color, int whichWhiteboard, bool tempIsEraser)
    {
        var start = ConvertMousePosition(new Vector2(lastMouse.x * Screen.width, lastMouse.y * Screen.height));
        var end = ConvertMousePosition(new Vector2(nowMouse.x * Screen.width, nowMouse.y * Screen.height));
        var tempBrushSize = tempIsEraser ? brushSizeOri * 4 : brushSizeOri;
        DrawLine(start, end, color, whichWhiteboard, tempBrushSize, true);
    }

    public void DrawLine(Vector2 start, Vector2 end, Color color, int whichWhiteboard, float tempBrushSize, bool isOnline = false)
    {
        canvas = whiteboards[whichWhiteboard - 1].myTexture;
        int halfBrushSize = Mathf.RoundToInt(tempBrushSize / 2f);
        Vector2 direction = end - start;
        int steps = Mathf.Max(Mathf.FloorToInt(Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y))), 1);

        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            int x = Mathf.FloorToInt(Mathf.Lerp(start.x, end.x, t));
            int y = Mathf.FloorToInt(Mathf.Lerp(start.y, end.y, t));

            for (int dx = -halfBrushSize; dx <= halfBrushSize; dx++)
            {
                for (int dy = -halfBrushSize; dy <= halfBrushSize; dy++)
                {
                    int pixelX = x + dx;
                    int pixelY = y + dy;

                    if (pixelX >= 0 && pixelX < width && pixelY >= 0 && pixelY < height)
                        canvas.SetPixel(pixelX, pixelY, color);
                }
            }
        }
        if(isOnline == false) canvas.Apply();
    }

    public void ClearCanvas()
    {
        Color[] clearColors = new Color[width * height];
        for (int i = 0; i < clearColors.Length; i++)
            clearColors[i] = Color.white;

        canvas.SetPixels(clearColors);
        canvas.Apply();
    }
        
    Vector2 ConvertMousePosition(Vector2 mousePos)
    {
        var mouseOut = new Vector2();

        mouseOut.x = (mousePos.x - bottomLeft.x) * (width / (topRight.x - bottomLeft.x));
        mouseOut.y = (mousePos.y - bottomLeft.y) * (height / (topRight.y - bottomLeft.y));

        return mouseOut;
    }

    public void CreateTexture2D(MyWhiteboard myWhiteboard)
    {
        width = Mathf.FloorToInt(topRight.x - bottomLeft.x);
        height = Mathf.FloorToInt(topRight.y - bottomLeft.y);

        float maxBrushSize = Mathf.Min(width, height) * 0.5f;
        brushSize = Mathf.Clamp(brushSize, 1f, maxBrushSize);
        brushSizeOri = brushSize;

        canvas = new Texture2D(width, height);
        canvas.name = myWhiteboard.name;

        Color[] clearColorArray = new Color[width * height];
        for (int i = 0; i < clearColorArray.Length; i++)
            clearColorArray[i] = Color.white;

        canvas.SetPixels(clearColorArray);
        canvas.Apply();

        //ri.texture = canvas;
        myWhiteboard.GetComponentInChildren<RawImage>().texture = canvas;
        myWhiteboard.myTexture = canvas;
    }

    void GetWorldCorners()
    {
        if (rt != null)
        {
            bottomLeft = new Vector3(2.40f, -2.40f, 28.01f);
            topRight = new Vector3(1922.40f, 1077.60f, 28.01f);
        }
    }

    public void CloseWhiteboards()
    {
        // Nonaktifkan game object WhiteBoard
        isWhiteboardActive = false;
        GameplayManager.instance.DeActivateWindow();
        whiteboardCanvas.SetActive(false);
    }

    public void UndoWhiteboards()
    {
        if (myData.Count < 1) return;

        var idData = Guid.NewGuid();

        var myWhiteboardData = myData.Where(x => x.whichWhiteboard == whiteBoardNow.whichWhiteboard).ToList();
        var tempMyData = myWhiteboardData[myWhiteboardData.Count - 1].whiteboardDataModels;

        myData.Remove(myWhiteboardData[myWhiteboardData.Count - 1]);

        foreach (var child in tempMyData)
        {
            child.colorR = 255f;
            child.colorG = 255f;
            child.colorB = 255f;
            child.colorA = 255f;
        }
        PhotonManager.instance.SendData((byte)CustomClass.TypeData.Whiteboard, new DataWhiteboard(idData, tempMyData.ToArray()), Photon.Realtime.ReceiverGroup.Others, Photon.Realtime.EventCaching.DoNotCache);
        foreach (var whiteboard in tempMyData)
        {
            DrawLineFromOnline(new Vector2(whiteboard.startX, whiteboard.startY), new Vector2(whiteboard.endX, whiteboard.endY), new Color(whiteboard.colorR, whiteboard.colorG, whiteboard.colorB, whiteboard.colorA), whiteboard.whichWhiteboard, false);
        }
        whiteboards[whiteBoardNow.whichWhiteboard - 1].myTexture.Apply();

        allDataWhiteboards.Add(idData, tempMyData.ToArray());
    }

    public void EraserWhiteboards()
    {
        isEraser = !isEraser;
        brushSize = isEraser ? brushSizeOri * 4 : brushSizeOri;
        buttonEraserWhiteboard.GetComponent<Image>().color = isEraser ? new Color32(164, 164, 164, 255) : Color.white;
    }

    public void IsWhiteboardActive(bool isActive)
    {
        isWhiteboardActive = isActive;
    }

    public class UndoModels
    {
        public Guid idData;
        public int whichWhiteboard;
        public List<CustomClass.WhiteboardDataModels> whiteboardDataModels;

        public UndoModels(Guid idData,int whichWhiteboard, List<WhiteboardDataModels> whiteboardDataModels)
        {
            this.idData = idData;
            this.whichWhiteboard = whichWhiteboard;
            this.whiteboardDataModels = whiteboardDataModels;
        }
    }
}
