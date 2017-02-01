namespace MazeCore
{
	public class ElementPosition
	{
		public byte Col { get; set; }
		public byte Row { get; set; }

		public bool IsNextTo(ElementPosition ElementPosition)
		{
			if (ElementPosition.Col >= Col - 1 &&
				ElementPosition.Col <= Col + 1 &&
				ElementPosition.Row >= Row - 1 &&
				ElementPosition.Row <= Row + 1)
			{
				return true;
			}
			return false;
		}

		public ElementPosition()
		{
		}

		public ElementPosition(byte row, byte col)
		{
			Row = row;
			Col = col;
		}

		public static bool operator ==(ElementPosition a, ElementPosition b)
		{
			if ((object)a == null && (object)b != null)
			{
				return false;
			}
			if ((object)a != null && (object)b == null)
			{
				return false;
			}
			if ((object)a == null && (object)b == null)
			{
				return true;
			}
			if ((object)a != null && (object)b != null)
			{
				return (a.Row == b.Row && a.Col == b.Col);
			}
			return false;
		}

		public static bool operator !=(ElementPosition a, ElementPosition b)
		{
			if ((object)a == null && (object)b != null)
			{
				return true;
			}
			if ((object)a != null && (object)b == null)
			{
				return true;
			}
			if ((object)a == null && (object)b == null)
			{
				return false;
			}
			if ((object)a != null && (object)b != null)
			{
				return (a.Row != b.Row || a.Col != b.Col);
			}
			return true;
		}
	}
}