using UnityEngine;

//Stores data needed for the Grid_Tile prefab.
namespace UCursesInclude
{
    public class GridTile : MonoBehaviour
    {
        [SerializeField] private GameObject _charTileObject;
        [SerializeField] private GameObject _underlineTileObject;
        private float _blinkRate = 0;



        void Start()
        {
            this.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            _charTileObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            _underlineTileObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        }

        public Vector2 SizeDelta
        {
            set
            {
                this.GetComponent<RectTransform>().sizeDelta = value;
                _charTileObject.GetComponent<RectTransform>().sizeDelta = value;
                _underlineTileObject.GetComponent<RectTransform>().sizeDelta = value;
            }
        }

        public float BlinkRate
        {
            get { return _blinkRate; }
            set { _blinkRate = value; }
        }

        public bool UnderlineEnable
        {
            get { return _underlineTileObject.activeSelf; }
            set
            {
                _underlineTileObject.SetActive(value);
                _underlineTileObject.GetComponent<UnityEngine.UI.Image>().color = _charTileObject.GetComponent<UnityEngine.UI.Image>().color;
            }
        }

        public Sprite CharacterSprite
        {
            get { return _charTileObject.GetComponent<UnityEngine.UI.Image>().sprite; }
        }

        public Color32 CharacterColor
        {
            get { return _charTileObject.GetComponent<UnityEngine.UI.Image>().color; }
            set { _charTileObject.GetComponent<UnityEngine.UI.Image>().color = value; }
        }

        public Color32 BackgroundColor
        {
            get { return this.GetComponent<UnityEngine.UI.Image>().color; }
            set { this.GetComponent<UnityEngine.UI.Image>().color = value; }
        }

        public bool BlinkEnable
        {
            get { return IsInvoking(nameof(runBlink)); }
            set { InvokeRepeating(nameof(runBlink), _blinkRate, _blinkRate); }
        }

        public void setCharacterSprite(Sprite charSprite, Vector2Int characterSize, bool offsetLine, bool isUI)
        {
            _charTileObject.GetComponent<UnityEngine.UI.Image>().sprite = charSprite;
            if (offsetLine == true)
            {
                if (isUI == true)
                {
                    _charTileObject.GetComponent<RectTransform>().sizeDelta = new Vector2(characterSize.x, characterSize.y);
                }
                else
                {
                    _charTileObject.GetComponent<RectTransform>().sizeDelta = new Vector2(characterSize.x - 1, characterSize.y);
                }
            }
        }


        private void runBlink()
        {
            if (_charTileObject.activeSelf == true)
            {
                _charTileObject.SetActive(false);
            }
            else
            {
                _charTileObject.SetActive(true);
            }
        }
    }
}
