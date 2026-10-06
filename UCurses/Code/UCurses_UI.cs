using UnityEngine;
using UnityEditor;

//Editor script used to set options and auto deploy the rest of UCurses objects.
namespace UCursesInclude
{
    public class UCurses_UI : EditorWindow
    {
        private Vector2Int _gridSize;
        private Vector2Int _dosScreenResolution;
        private float _aspectRatio;
        private ScreenAlignment _screenAlignment;
        private float _alignmentOffsetSelection;
        private Vector2Int _characterSize;
        private bool _offsetLine;
        private FilterMode _filterModeCharacters;
        private FilterMode _filterModeScreen;

        private int _gridSizePopupSelection = 0;
        private int _aspectRatioPopupSelection = 0;
        private int _screenAlignmentSelection = 0;
        private int _filterModeCharactersSelection = 0;
        private int _filterModeScreenSelection = 0;


        private string[] _textureFilterModeDropdown = new string[] { "Point", "Bilinear", "Trilinear" };
        private string[] _gridSizeDropdown = new string[] { "40×25 (320×200)", "80×25 (640×200)", "80×50 (640×400)", "80×60 (640×480)", "80×30 (640×480)", "80×25 (720×400)", "Custom" };
        private string[] _aspectRatioDropdown = new string[] { "Original", "3:2", "4:3", "5:4", "16:9", "16:10" };
        private string[] _screenAlignmentDropdown = new string[] { "Middle", "Left", "Right"};




        [MenuItem("Window/UCurses Setup")]
        public static void ShowWindow()
        {
            GetWindow<UCurses_UI>("UCurses Setup");
        }

        void OnEnable()
        {
            _gridSizePopupSelection = EditorPrefs.GetInt("GridPopupSelection");
            _aspectRatioPopupSelection = EditorPrefs.GetInt("AspectRatioSelection");
            _screenAlignmentSelection = EditorPrefs.GetInt("ScreenAlignmentSelection");
            _alignmentOffsetSelection = EditorPrefs.GetFloat("ScreenOffsetSelection");

            _filterModeCharactersSelection = EditorPrefs.GetInt("FilterModeCharacters");
            _filterModeScreenSelection = EditorPrefs.GetInt("FilterModeScreen");
        }

        private void OnGUI()
        {
            _gridSizePopupSelection = EditorGUILayout.Popup("Grid Size", _gridSizePopupSelection, _gridSizeDropdown);
            switch (_gridSizePopupSelection)
            {
                //40×25	8×8	320×200 (Mode 0, 1)
                case 0:
                    _gridSize = new Vector2Int(40, 25);
                    _dosScreenResolution = new Vector2Int(320, 200);
                    _characterSize = new Vector2Int(8, 8);
                    _offsetLine = false;
                    break;
                //80×25	8×8	640×200 (Mode 6)
                case 1:
                    _gridSize = new Vector2Int(80, 25);
                    _dosScreenResolution = new Vector2Int(640, 200);
                    _characterSize = new Vector2Int(8, 8);
                    _offsetLine = false;
                    break;
                //80×50	8×8	640×400 (Mode 102)
                case 2:
                    _gridSize = new Vector2Int(80, 50);
                    _dosScreenResolution = new Vector2Int(640, 400);
                    _characterSize = new Vector2Int(8, 8);
                    _offsetLine = false;
                    break;
                //80×60	8×8	640×480 (Mode 38, 67, 82, 264)
                case 3:
                    _gridSize = new Vector2Int(80, 60);
                    _dosScreenResolution = new Vector2Int(640, 480);
                    _characterSize = new Vector2Int(8, 8);
                    _offsetLine = false;
                    break;
                //80×30	8×16 640×480 (Mode 38, 67, 82, 264)
                case 4:
                    _gridSize = new Vector2Int(80, 30);
                    _dosScreenResolution = new Vector2Int(640, 480);
                    _characterSize = new Vector2Int(8, 16);
                    _offsetLine = false;
                    break;
                //80×25	9×16 720×400 (Mode 2, 3)
                case 5:
                    _gridSize = new Vector2Int(80, 25);
                    _dosScreenResolution = new Vector2Int(720, 400);
                    _characterSize = new Vector2Int(9, 16);
                    _offsetLine = true;
                    break;
                case 6:
                    EditorGUILayout.Vector2IntField("Size of Grid", _gridSize);
                    EditorGUILayout.Vector2IntField("Dos Screen Resolution", _dosScreenResolution);
                    EditorGUILayout.Vector2IntField("Character Size", _characterSize);
                    EditorGUILayout.Toggle("Offset Line", _offsetLine);
                    break;
            }

            _aspectRatioPopupSelection = EditorGUILayout.Popup("Aspect Ratio", _aspectRatioPopupSelection, _aspectRatioDropdown);
            switch (_aspectRatioPopupSelection)
            {
                //Original
                case 0:
                    _aspectRatio = (float)_dosScreenResolution.x / (float)_dosScreenResolution.y;
                    break;
                //"3:2"
                case 1:
                    _aspectRatio = 1.5f;
                    break;
                //"4:3"
                case 2:
                    _aspectRatio = 1.3333f;
                    break;
                //5:4
                case 3:
                    _aspectRatio = 1.25f;
                    break;
                //16:9
                case 4:
                    _aspectRatio = 1.7777f;
                    break;
                //16:10
                case 5:
                    _aspectRatio = 1.6f;
                    break;
            }

            _screenAlignmentSelection = EditorGUILayout.Popup("Screen Alignment", _screenAlignmentSelection, _screenAlignmentDropdown);
            switch (_screenAlignmentSelection)
            {
                //Middle
                case 0:
                    _screenAlignment = ScreenAlignment.Middle;
                    break;
                //Left
                case 1:
                    _screenAlignment = ScreenAlignment.Left;
                    break;
                //Right
                case 2:
                    _screenAlignment = ScreenAlignment.Right;
                    break;
            }

            _alignmentOffsetSelection = EditorGUILayout.FloatField("Alignment Offset", _alignmentOffsetSelection);

            _filterModeCharactersSelection = EditorGUILayout.Popup("Character Filter", _filterModeCharactersSelection, _textureFilterModeDropdown);
            switch (_filterModeCharactersSelection)
            {
                //Point
                case 0:
                    _filterModeCharacters = FilterMode.Point;
                    break;
                //Bilinear
                case 1:
                    _filterModeCharacters = FilterMode.Bilinear;
                    break;
                //Trilinear
                case 2:
                    _filterModeCharacters = FilterMode.Trilinear;
                    break;
            }

            _filterModeScreenSelection = EditorGUILayout.Popup("Screen Filter", _filterModeScreenSelection, _textureFilterModeDropdown);
            switch (_filterModeScreenSelection)
            {
                //Point
                case 0:
                    _filterModeScreen = FilterMode.Point;
                    break;
                //Bilinear
                case 1:
                    _filterModeScreen = FilterMode.Bilinear;
                    break;
                //Trilinear
                case 2:
                    _filterModeScreen = FilterMode.Trilinear;
                    break;
            }


            if (GUILayout.Button("Set Grid"))
            {
                GridSetup setup = new GridSetup();

                setup.generateGrid(_gridSize, _dosScreenResolution, _aspectRatio, _screenAlignment, _alignmentOffsetSelection, _characterSize, _offsetLine, _filterModeScreen, _filterModeCharacters);

                EditorPrefs.SetInt("GridPopupSelection", _gridSizePopupSelection);
                EditorPrefs.SetInt("AspectRatioSelection", _aspectRatioPopupSelection);
                EditorPrefs.SetInt("ScreenAlignmentSelection", _screenAlignmentSelection);
                EditorPrefs.SetFloat("ScreenOffsetSelection", _alignmentOffsetSelection);

                EditorPrefs.SetInt("FilterModeCharacters", _filterModeCharactersSelection);
                EditorPrefs.SetInt("FilterModeScreen", _filterModeScreenSelection);
            }
        }
    }
}
