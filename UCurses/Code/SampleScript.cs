using UnityEngine;

//Basic test script running through all the main features of the Ucurses class.
namespace UCursesInclude
{
    public class SampleScript : MonoBehaviour
    {
        private int _currentColor = 0;
        private int _currentAscii = 1;
        private Vector2Int _currentPos = new Vector2Int(0, 1);
        private UnityEngine.Color[] _colorSet = new UnityEngine.Color[] { Color.red, Color.green, Color.blue, Color.yellow };



        void Start()
        {
            UCurses.Instance.printBackgroundRectangle(new Vector2Int(0, 0), Color.gray, UCurses.Instance.GridSize.x, 1);
            UCurses.Instance.printString(new Vector2Int(1, 0), "Mouse Position: X", Color.black);
            UCurses.Instance.characterUnderlined(new Vector2Int(1, 0), true, 15);
            UCurses.Instance.printCharacter(new Vector2Int(21, 0), 'Y', Color.black);
            UCurses.Instance.printCharacter(new Vector2Int(UCurses.Instance.GridSize.x - 1, 0), '@', Color.green);
            UCurses.Instance.characterBlink(new Vector2Int(UCurses.Instance.GridSize.x - 1, 0), true, 0.5f);
            InvokeRepeating(nameof(drawTest), 0.01f, 0.01f);
        }

        private void Update()
        {
            UCurses.Instance.printNumberInt(new Vector2Int(18, 0), UCurses.Instance.mousePosition().x, Color.white, 2, false);
            UCurses.Instance.printNumberInt(new Vector2Int(22, 0), UCurses.Instance.mousePosition().y, Color.white, 2, false);
        }

        //Runs the main test functionality.
        private void drawTest()
        {
            UCurses.Instance.printBackground(_currentPos, _colorSet[_currentColor]);
            UCurses.Instance.printBackground(nextGridSpot(_currentPos), Color.white);
            UCurses.Instance.printCharacter(_currentPos, UCurses.Instance.asciiToChar(_currentAscii), _colorSet[nextColor(_currentColor)]);
            _currentPos = nextGridSpot(_currentPos);
            _currentAscii = nextAscii(_currentAscii);
        }

        //Changes to the next color in the squence.
        private int nextColor(int colorValue)
        {
            colorValue++;
            if (colorValue == _colorSet.Length)
            {
                colorValue = 0;
            }
            return colorValue;
        }

        //Returns the next ascii value looping through all characters.
        private int nextAscii(int asciiValue)
        {
            asciiValue++;
            if (asciiValue == 245)
            {
                asciiValue = 1;
                _currentColor = nextColor(_currentColor);
            }
            return asciiValue;
        }

        //Returns the next character in the grid moving from left to right.
        private Vector2Int nextGridSpot(Vector2Int current)
        {
            current.x++;
            if (current.x == UCurses.Instance.GridSize.x)
            {
                current.x = 0;
                current.y++;
                if (current.y == UCurses.Instance.GridSize.y)
                {
                    current.x = 0;
                    current.y = 1;
                }
            }
            return current;
        }
    }
}
