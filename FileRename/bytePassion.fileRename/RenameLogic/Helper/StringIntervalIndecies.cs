namespace bytePassion.FileRename.RenameLogic.Helper
{
    public class StringIntervalIndecies
    {
        public StringIntervalIndecies(int startIndex, int endIndex)
        {
            StartIndex = startIndex;
            EndIndex   = endIndex;
        }

        public int StartIndex { get; }
        public int EndIndex   { get; }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:                        return false;
                case StringIntervalIndecies item: return Equals(item);
                default:                          return false;
            }
        }

        protected bool Equals(StringIntervalIndecies other)
        {
            return StartIndex == other.StartIndex && EndIndex == other.EndIndex;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StartIndex * 397) ^ EndIndex;
            }
        }
    }
}