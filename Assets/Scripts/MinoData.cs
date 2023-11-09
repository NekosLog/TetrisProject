public class MinoData
{
    public enum E_MinoColor { cyan = 0, purple = 1, red = 2, green = 3, yellow = 4, orange = 5, blue = 6, empty = 7 }

    public struct minoState
    {
        public minoState(int row,int column,E_MinoColor minoColor)
        {
            Row = row;
            Column = column;
            this.minoColor = minoColor;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public E_MinoColor minoColor { get; set; }

        public const int COLUMN = 0;

        public const int ROW = 1;
    }

    public const int COLUMN = 0;

    public const int ROW = 1;
}
