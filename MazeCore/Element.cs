namespace MazeCore
{
	public class Element
	{
		public ElementStatus Status { get; internal set; }
		public ElementPosition Position { get; internal set; }
		public ElementPosition SourcePos { get; internal set; }
		public bool IsNextTo(Element Element)
		{
			if (Element.Position.Col >= Position.Col - 1 &&
				Element.Position.Col <= Position.Col + 1 &&
				Element.Position.Row >= Position.Row - 1 &&
				Element.Position.Row <= Position.Row + 1)
			{
				return true;
			}
			return false;
		}
		public Element()
		{
			Status = ElementStatus.EMPTY;
			Position = null;
			SourcePos = null;
		}

		public Element(byte row, byte col)
		{
			Status = ElementStatus.EMPTY;
			Position = new ElementPosition(row, col);
			SourcePos = null;
		}
	}
}