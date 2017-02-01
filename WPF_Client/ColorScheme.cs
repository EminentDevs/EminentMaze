using System.Windows.Media;

namespace WPF_Client
{
	/// <summary>
	/// Holds the colors used in the UI
	/// </summary>
	class ColorScheme
	{
		public Color LightTextColor { get; set; }
		public Color DarkTextColor { get; set; }
		public Color TextSelectionColor { get; set; }
		public Color MenuBarColor { get; set; }
		public Color PanelColor { get; set; }
		public Color ActivePanelIndicatorColor { get; set; }
		public Color MainGridColor { get; set; }
		public Color FullscreenPanelColor { get; set; }
		public Color MazeGridsColor { get; set; }
		public Color MazeBackgroundColor { get; set; }
		public Color BlockColor { get; set; }
		public Color StartPositionColor { get; set; }
		public Color FinishPositionColor { get; set; }
		public Color SolutionPathColor { get; set; }
		public Color AlternateSolutionPathColor { get; set; }
	}

	/// <summary>
	/// Pre-defined color sets 
	/// </summary>
	static class ColorSchemes
	{
		public static ColorScheme Blue { get; } = new ColorScheme()
		{
			LightTextColor = Color.FromRgb(238, 238, 238),
			DarkTextColor = Color.FromRgb(12, 30, 42),
			TextSelectionColor = Color.FromRgb(39, 98, 156),
			MenuBarColor = Color.FromRgb(20, 62, 87),
			PanelColor = Color.FromRgb(39, 92, 122),
			ActivePanelIndicatorColor = Color.FromRgb(112, 184, 214),
			MainGridColor = Color.FromRgb(150, 190, 216),
			FullscreenPanelColor = Color.FromRgb(22, 52, 66),
			MazeGridsColor = Color.FromRgb(0, 0, 0),
			MazeBackgroundColor = Color.FromRgb(255, 255, 255),
			BlockColor = Color.FromRgb(22, 52, 66),
			StartPositionColor = Color.FromRgb(20, 120, 200),
			FinishPositionColor = Color.FromRgb(210, 30, 20),
			SolutionPathColor = Color.FromRgb(17, 88, 132),
			AlternateSolutionPathColor = Color.FromRgb(115, 164, 196)
		};
		public static ColorScheme Green { get; } = new ColorScheme()
		{
			LightTextColor = Color.FromRgb(238, 238, 238),
			DarkTextColor = Color.FromRgb(30, 42, 12),
			TextSelectionColor = Color.FromRgb(39, 156, 98),
			MenuBarColor = Color.FromRgb(20, 87, 62),
			PanelColor = Color.FromRgb(39, 122, 92),
			ActivePanelIndicatorColor = Color.FromRgb(112, 214, 184),
			MainGridColor = Color.FromRgb(195, 237, 221),
			FullscreenPanelColor = Color.FromRgb(22, 66, 52),
			MazeGridsColor = Color.FromRgb(0, 0, 0),
			MazeBackgroundColor = Color.FromRgb(255, 255, 255),
			BlockColor = Color.FromRgb(22, 66, 52),
			StartPositionColor = Color.FromRgb(34, 180, 55),
			FinishPositionColor = Color.FromRgb(210, 30, 20),
			SolutionPathColor = Color.FromRgb(15, 125, 60),
			AlternateSolutionPathColor = Color.FromRgb(119, 194, 164)
		};
		public static ColorScheme Red { get; } = new ColorScheme()
		{
			LightTextColor = Color.FromRgb(238, 238, 238),
			DarkTextColor = Color.FromRgb(42, 12, 12),
			TextSelectionColor = Color.FromRgb(156, 58, 39),
			MenuBarColor = Color.FromRgb(87, 36, 20),
			PanelColor = Color.FromRgb(118, 45, 45),
			ActivePanelIndicatorColor = Color.FromRgb(214, 112, 112),
			MainGridColor = Color.FromRgb(240, 221, 217),
			FullscreenPanelColor = Color.FromRgb(66, 31, 22),
			MazeGridsColor = Color.FromRgb(0, 0, 0),
			MazeBackgroundColor = Color.FromRgb(255, 255, 255),
			BlockColor = Color.FromRgb(66, 31, 22),
			StartPositionColor = Color.FromRgb(200, 20, 37),
			FinishPositionColor = Color.FromRgb(210, 30, 20),
			SolutionPathColor = Color.FromRgb(132, 31, 17),
			AlternateSolutionPathColor = Color.FromRgb(208, 145, 145)
		};
		public static ColorScheme Yellow { get; } = new ColorScheme()
		{
			LightTextColor = Color.FromRgb(238, 238, 238),
			DarkTextColor = Color.FromRgb(42, 39, 12),
			TextSelectionColor = Color.FromRgb(156, 152, 39),
			MenuBarColor = Color.FromRgb(87, 85, 20),
			PanelColor = Color.FromRgb(122, 119, 39),
			ActivePanelIndicatorColor = Color.FromRgb(214, 210, 112),
			MainGridColor = Color.FromRgb(235, 235, 206),
			FullscreenPanelColor = Color.FromRgb(66, 52, 22),
			MazeGridsColor = Color.FromRgb(0, 0, 0),
			MazeBackgroundColor = Color.FromRgb(255, 255, 255),
			BlockColor = Color.FromRgb(66, 52, 22),
			StartPositionColor = Color.FromRgb(200, 180, 20),
			FinishPositionColor = Color.FromRgb(210, 30, 20),
			SolutionPathColor = Color.FromRgb(132, 117, 17),
			AlternateSolutionPathColor = Color.FromRgb(197, 199, 150)
		};
		public static ColorScheme Pink { get; } = new ColorScheme()
		{
			LightTextColor = Color.FromRgb(238, 238, 238),
			DarkTextColor = Color.FromRgb(42, 12, 38),
			TextSelectionColor = Color.FromRgb(168, 84, 161),
			MenuBarColor = Color.FromRgb(87, 20, 75),
			PanelColor = Color.FromRgb(126, 58, 129),
			ActivePanelIndicatorColor = Color.FromRgb(190, 117, 181),
			MainGridColor = Color.FromRgb(235, 210, 235),
			FullscreenPanelColor = Color.FromRgb(66, 22, 66),
			MazeGridsColor = Color.FromRgb(0, 0, 0),
			MazeBackgroundColor = Color.FromRgb(255, 255, 255),
			BlockColor = Color.FromRgb(66, 22, 66),
			StartPositionColor = Color.FromRgb(200, 20, 180),
			FinishPositionColor = Color.FromRgb(210, 30, 20),
			SolutionPathColor = Color.FromRgb(132, 17, 117),
			AlternateSolutionPathColor = Color.FromRgb(199, 121, 182)
		};
		public static ColorScheme Gray { get; } = new ColorScheme()
		{
			LightTextColor = Color.FromRgb(238, 238, 238),
			DarkTextColor = Color.FromRgb(40, 40, 40),
			TextSelectionColor = Color.FromRgb(125, 125, 125),
			MenuBarColor = Color.FromRgb(85, 85, 85),
			PanelColor = Color.FromRgb(122, 122, 122),
			ActivePanelIndicatorColor = Color.FromRgb(190, 190, 190),
			MainGridColor = Color.FromRgb(215, 215, 215),
			FullscreenPanelColor = Color.FromRgb(60, 60, 60),
			MazeGridsColor = Color.FromRgb(0, 0, 0),
			MazeBackgroundColor = Color.FromRgb(255, 255, 255),
			BlockColor = Color.FromRgb(60, 60, 60),
			StartPositionColor = Color.FromRgb(150, 150, 150),
			FinishPositionColor = Color.FromRgb(210, 30, 20),
			SolutionPathColor = Color.FromRgb(110, 110, 110),
			AlternateSolutionPathColor = Color.FromRgb(195, 195, 195)
		};
	}
}
