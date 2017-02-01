using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MazeCore;

namespace WPF_Client
{
	public partial class MainWindow : Window
	{
		// General
		private Maze maze;
		private ElementPosition startPos;
		private ElementPosition finishPos;
		private UIscales UIscaleStatus;
		private Panels activePanel;
		private Panels lastActivePanel;
		private SolveMethod solveMethod;
		private DisplayMethod displayMethod;
		private string labelsNameFormat = "L_{0}_{1}"; // Do not change name format!!! otherwise apply suitable changes to getLabelRow() and getLabelCol() methods.
		private string stepNumberFormat = "{0} of {1}";
		private Stopwatch stopwatch = new Stopwatch();
		private Thread mazeThread = new Thread(() => { });
		private Grid leavingPanel;
		private string currentPointingLabelName;
		private short currentStep;

		// Scales
		private double UIscale = 1;
		private double elementSize = 10;
		private double mazeBorderThickness = 3;
		private double menuSeparatorThickness = 0.5;
		private double gridsThickness = 0.3;
		private double textBoxBorderThickness = 1;
		private double windowBorderThickness = 7;
		private double buttonBorderThickness = 0.2;
		private double panelIconsScale = 1.5;
		private double buttonContentPadding = 0.5;
		private short mazeContainerMargin = 20;
		private short windowTitleLeftMargin = 12;
		private short panelMenuContentsMargin = 25;
		private short panelMenuItemsMargin = 10;
		private short spaceMarginRatio = 2;
		private short windowTitleFontSize = 12;
		private short menuBarHeight = 30;
		private short maximizedPanelWidth = 150;
		private short minimizedPanelWidth = 45;
		private short activePanelIndicatorWidth = 5;
		private short fullscreenPanelTravelDistance = 50;

		// Font sizes
		private short simpleTextFontSize = 12;
		private short headerFontSize = 14;
		private short buttonsFontSize = 14;
		private short panelTitleFontSize = 16;
		private short panelMenuTitleFontSize = 28;

		// Color scheme
		private ColorScheme colorScheme = ColorSchemes.Blue;

		// Animations
		DoubleAnimation splashScreenFadeOutAnimation;
		DoubleAnimation notSolvableMessageFadeInAnimation;
		DoubleAnimation notSolvableMessageFadeOutAnimation;
		DoubleAnimation panelExpandingAnimation;
		DoubleAnimation panelCollapsingAnimation;
		ThicknessAnimation containerRightAnimation;
		ThicknessAnimation containerLeftAnimation;
		ThicknessAnimation fullscreenPanelRightAnimation;
		ThicknessAnimation fullscreenPanelLeftAnimation;
		DoubleAnimation fullscreenPanelFadeInAnimation;
		DoubleAnimation fullscreenPanelFadeOutAnimation;
		DoubleAnimation fullscreenPanelUnBlurAnimation;
		DoubleAnimation fullscreenPanelBlurAnimation;
		ThicknessAnimation panelMenuOutAnimation;
		ThicknessAnimation panelMenuInAnimation;
		DoubleAnimation panelMenuFadeInAnimation;
		DoubleAnimation panelMenuFadeOutAnimation;
		DoubleAnimation panelMenuUnBlurAnimation;
		DoubleAnimation panelMenuBlurAnimation;

		// Animation durations in milliseconds and animation speed
		private double solutionAnimationSpeedRatio = 1;
		private double elementPerSecond = 20;
		private double UIanimationSpeedRatio = 1;
		private ushort splashScreenTime = 3000;
		private ushort splashScreenFadeOutTime = 200;
		private ushort notSolvableMessageTime = 2000;
		private ushort notSolvableMessageFadeTime = 200;
		private ushort panelExpandingTime = 200;
		private ushort panelCollapsingTime = 200;
		private ushort fullscreenPanelExpandingTime = 150;
		private ushort fullscreenPanelCollapsingTime = 150;
		private ushort panelMenuSwitchingTime = 150;
		private ushort panelMenuSwitchingDelayTime = 0;
		private ushort solutionStepsDelayTime;

		// Storyboards
		private Storyboard splashScreenFadeOutStoryboard;
		private Storyboard notSolvableMessageStoryboard;
		private Storyboard panelExpandingStoryboard;
		private Storyboard panelCollapsingStoryboard;
		private Storyboard fullscreenPanelExpandingStoryboard;
		private Storyboard fullscreenPanelCollapsingStoryboard;
		private Storyboard panelMenuSwitchingOutStoryboard;
		private Storyboard panelMenuSwitchingInStoryboard;

		// Animation easing
		private ExponentialEase splashScreenFadeOutEase = new ExponentialEase() { Exponent = 1, EasingMode = EasingMode.EaseIn };
		private ExponentialEase panelExpandingEase = new ExponentialEase() { Exponent = 4, EasingMode = EasingMode.EaseOut };
		private ExponentialEase panelCollapsingEase = new ExponentialEase() { Exponent = 4, EasingMode = EasingMode.EaseOut };
		private ExponentialEase containerRightEase = new ExponentialEase() { Exponent = 2.5, EasingMode = EasingMode.EaseOut };
		private ExponentialEase containerLeftEase = new ExponentialEase() { Exponent = 2.5, EasingMode = EasingMode.EaseOut };
		private ExponentialEase fullscreenPanelExpandingEase = new ExponentialEase() { Exponent = 2, EasingMode = EasingMode.EaseOut };
		private ExponentialEase fullscreenPanelCollapsingEase = new ExponentialEase() { Exponent = 2, EasingMode = EasingMode.EaseOut };
		private ExponentialEase panelMenuSwitchingEase = new ExponentialEase() { Exponent = 2, EasingMode = EasingMode.EaseOut };

		// Blur effect
		private double animationBlurRadius = 15;

		// Hotkeys
		private Hotkeys activeHotkey = Hotkeys.NONE;
		private Key blockHotkey = Key.B;
		private Key emptyHotkey = Key.E;
		private Key startHotkey = Key.S;
		private Key finishHotkey = Key.F;

		// Switches
		private bool mazeModificationEnabled = true;
		private bool displayMazeGrids = false;
		private bool solutionMode = false;
		private bool pause = true;
		private bool panelMaximized = false;
		private bool solutionViewerMaximized = true;
		private bool fullscreenPanelOpened = false;
		private bool panelAnimationInProgress = false;
		private bool fullscreenPanelAnimationInProgress = false;
		private bool panelSwitchingInProgress = false;


		public MainWindow()
		{
			InitializeComponent();

			// First initializations
			Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 60 });// Set animations fps
			splashScreenGrid.Visibility = Visibility.Visible;
			UIscaleStatus = UIscales._100;
			UI100Button.FontWeight = FontWeights.Black;
			resetUIProperties();
			Height = MinHeight + 260 * UIscale;
			Width = Height * 1.4;
			changeActivePanel(Panels.NONE);
			hideMaze();
			hideGrids();
			invalidWidthInputTextBlock.Text = Maze.MinWidth + " - " + Maze.MaxWidth;
			invalidHeightInputTextBlock.Text = Maze.MinWidth + " - " + Maze.MaxWidth;
			invalidPercentTextBlock.Text = "0 - 100";
			invalidWidthInputTextBlock.Visibility = Visibility.Collapsed;
			invalidHeightInputTextBlock.Visibility = Visibility.Collapsed;
			invalidPercentTextBlock.Visibility = Visibility.Collapsed;
			splashScreenFadeOutStoryboard.Begin();
			solutionViewerControlGrid.Visibility = Visibility.Collapsed;
			solutionViewerControlGrid.Margin = new Thickness(0, 0, 0, -solutionViewerControlGrid.Height);

			// General Events
			StateChanged += MainWindow_StateChanged;
			SizeChanged += MainWindow_SizeChanged;
			KeyDown += MainWindow_KeyDown;
			KeyUp += MainWindow_KeyUp;
			mazeGrid.MouseLeave += MazeGrid_MouseLeave;

			// Ctrl+W Close Hotkey:
			InputBinding clsoeInputBinding = new InputBinding(SystemCommands.CloseWindowCommand, new KeyGesture(Key.W, ModifierKeys.Control));
			InputBindings.Add(clsoeInputBinding);
			CommandBinding closeCommandBinding = new CommandBinding(SystemCommands.CloseWindowCommand);
			closeCommandBinding.Executed += new ExecutedRoutedEventHandler(HandlerThatCloseWindow);
			CommandBindings.Add(closeCommandBinding);

			// Ctrl+M Minimize Hotkey:
			InputBinding minimizeInputBinding = new InputBinding(SystemCommands.MinimizeWindowCommand, new KeyGesture(Key.M, ModifierKeys.Control));
			InputBindings.Add(minimizeInputBinding);
			CommandBinding minimizeCommandBinding = new CommandBinding(SystemCommands.MinimizeWindowCommand);
			minimizeCommandBinding.Executed += new ExecutedRoutedEventHandler(HandlerThatMinimizeWindow);
			CommandBindings.Add(minimizeCommandBinding);
		}

		private void resetUIProperties()
		{
			// Initializing UI elements:
			initWindow();
			initMenuBar();
			initPanel();
			initContainer();
			initsolutionViewerControl();
			initFullscreenPanel();
			initAnimations();
		}

		/// <summary>
		/// Updates the whole visual maze
		/// </summary>
		private void updateMazeGrid()
		{
			if (maze != null)
			{
				for (byte i = 0; i < maze.Height; i++)
				{
					for (byte j = 0; j < maze.Width; j++)
					{
						// Check whether an Element is block or empty
						getLabel(i, j).Background = (maze.Map[i, j].Status == ElementStatus.BLOCK) ? new SolidColorBrush(colorScheme.BlockColor) : new SolidColorBrush(colorScheme.MazeBackgroundColor);
					}
				}
				// Update start and finish positions values
				startPos = maze.StartPos;
				finishPos = maze.FinishPos;
				// Display new start and positions
				if (startPos != null)
				{
					getLabel(startPos).Background = new SolidColorBrush(colorScheme.StartPositionColor);
				}
				if (finishPos != null)
				{
					getLabel(finishPos).Background = new SolidColorBrush(colorScheme.FinishPositionColor);
				}
			}
		}

		// Create new empty maze
		private void createMazeButton_Click(object sender, RoutedEventArgs e)
		{
			if (validateMazeWidth(heightInputTextBox.Text) && validateMazeWidth(widthInputTextBox.Text))
			{
				ExitSolutionMode();
				disposeMaze();
				maze = new Maze(byte.Parse(heightInputTextBox.Text), byte.Parse(widthInputTextBox.Text));
				Label tempLabel;
				for (byte i = 0; i < maze.Height; i++)
				{
					for (byte j = 0; j < maze.Width; j++)
					{
						// Initializing maze element Labels
						tempLabel = new Label();
						tempLabel.Name = labelName(i, j);
						tempLabel.Width = elementSize * UIscale;
						tempLabel.Height = elementSize * UIscale;
						tempLabel.VerticalAlignment = VerticalAlignment.Top;
						tempLabel.HorizontalAlignment = HorizontalAlignment.Left;
						tempLabel.Margin = new Thickness(j * elementSize * UIscale, i * elementSize * UIscale, 0, 0);
						tempLabel.Padding = new Thickness(0);
						tempLabel.Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
						tempLabel.MouseEnter += element_MouseEnter;
						tempLabel.MouseDown += element_MouseDown;
						mazeGrid.Children.Add(tempLabel);
					}
				}
				tempLabel = null;
				initMazeContainer();
				initMazeGrids();
				showMaze();
				updateMazeGrid();
				closeFullscreenPanel();
			}
			else
			{
				if (!validateMazeWidth(widthInputTextBox.Text))
				{
					invalidWidthInputTextBlock.Visibility = Visibility.Visible;
				}
				if (!validateMazeWidth(heightInputTextBox.Text))
				{
					invalidHeightInputTextBlock.Visibility = Visibility.Visible;
				}
			}
		}

		/// <summary>
		/// Set state of an element to Block
		/// </summary>
		/// <param name="object"></param>
		private void setBlock(object o)
		{
			if (solutionMode)
			{
				ExitSolutionMode();
			}
			Label label = (Label)o;
			if (maze.AddBlock(getLabelPosition(label.Name)))
			{
				label.Background = new SolidColorBrush(colorScheme.BlockColor);
				startPos = maze.StartPos;
				finishPos = maze.FinishPos;
			}
		}

		/// <summary>
		/// Set state of an element to Empty
		/// </summary>
		/// <param name="object"></param>
		private void setEmpty(object o)
		{
			if (solutionMode)
			{
				ExitSolutionMode();
			}
			Label label = (Label)o;
			if (maze.RemoveBlock(getLabelPosition(label.Name)))
			{
				label.Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
			}
		}

		/// <summary>
		/// Set state of an element to Start
		/// </summary>
		/// <param name="object"></param>
		private void setStart(object o)
		{
			if (solutionMode)
			{
				ExitSolutionMode();
			}
			Label label = (Label)o;
			if (maze.SetStartPosition(getLabelPosition(label.Name)))
			{
				if (startPos != null)
				{
					getLabel(startPos).Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
					getLabel(startPos).Content = null;
				}
				startPos = maze.StartPos;
				finishPos = maze.FinishPos;
				if (startPos != null)
				{
					label.Background = new SolidColorBrush(colorScheme.StartPositionColor);
				}
				else
				{
					label.Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
				}
			}
		}

		/// <summary>
		/// Set state of an element to Finish
		/// </summary>
		/// <param name="object"></param>
		private void setFinish(object o)
		{
			if (solutionMode)
			{
				ExitSolutionMode();
			}
			Label label = (Label)o;
			if (maze.SetFinishPosition(getLabelPosition(label.Name)))
			{
				if (finishPos != null)
				{
					getLabel(finishPos).Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
					getLabel(finishPos).Content = null;
				}
				startPos = maze.StartPos;
				finishPos = maze.FinishPos;
				if (finishPos != null)
				{
					label.Background = new SolidColorBrush(colorScheme.FinishPositionColor);
				}
				else
				{
					label.Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
				}
			}
		}

		/// <summary>
		/// Gets position of a label by giving it's name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private ElementPosition getLabelPosition(string name)
		{
			return new ElementPosition(getLabelRow(name), getLabelCol(name));
		}

		/// <summary>
		/// Gets the Label with the desired position in the maze
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Label getLabel(ElementPosition pos)
		{
			return (Label)mazeGrid.Children[pos.Row * maze.Width + pos.Col];
		}

		/// <summary>
		/// Gets the Label with the desired row number and column number in the maze
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		private Label getLabel(byte row, byte col)
		{
			return (Label)mazeGrid.Children[row * maze.Width + col];
		}

		/// <summary>
		/// Gets row number from label name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private byte getLabelRow(string name)
		{
			return byte.Parse(name.Substring(2, 2));
		}

		/// <summary>
		/// Gets column numbe from label name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private byte getLabelCol(string name)
		{
			return byte.Parse(name.Substring(5, 2));
		}

		/// <summary>
		/// Generates label name using row & column number
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		private string labelName(byte row, byte col)
		{
			return string.Format(labelsNameFormat, (row > 9) ? row.ToString() : string.Format("0{0}", row), (col > 9) ? col.ToString() : string.Format("0{0}", col));
		}

		/// <summary>
		/// Prepares maze container
		/// </summary>
		private void initMazeContainer()
		{
			mazeContainer.Width = elementSize * maze.Width * UIscale + mazeBorderThickness * UIscale * 2;
			mazeContainer.Height = elementSize * maze.Height * UIscale + mazeBorderThickness * UIscale * 2;
			mazeContainer.Background = new SolidColorBrush(colorScheme.BlockColor);

			mazeGrid.Width = elementSize * maze.Width * UIscale;
			mazeGrid.Height = elementSize * maze.Height * UIscale;
			mazeGrid.Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);

			gridsCanvas.Width = mazeGrid.Width;
			gridsCanvas.Height = mazeGrid.Height;
		}

		/// <summary>
		/// Prepares maze grids
		/// </summary>
		private void initMazeGrids()
		{
			gridsCanvas.Children.Clear();
			Line tempLine;
			//Vertical lines:
			for (byte i = 0; i < maze.Width - 1; i++)
			{
				tempLine = new Line();
				tempLine.StrokeThickness = gridsThickness * UIscale;
				tempLine.Stroke = new SolidColorBrush(colorScheme.MazeGridsColor);
				tempLine.X1 = elementSize * UIscale * (i + 1);
				tempLine.Y1 = 0;
				tempLine.X2 = elementSize * UIscale * (i + 1);
				tempLine.Y2 = mazeGrid.Height;
				gridsCanvas.Children.Add(tempLine);
			}
			//Horizontal lines
			for (byte i = 0; i < maze.Height - 1; i++)
			{
				tempLine = new Line();
				tempLine.StrokeThickness = gridsThickness * UIscale;
				tempLine.Stroke = new SolidColorBrush(colorScheme.MazeGridsColor);
				tempLine.X1 = 0;
				tempLine.Y1 = elementSize * UIscale * (i + 1);
				tempLine.X2 = mazeGrid.Width;
				tempLine.Y2 = elementSize * UIscale * (i + 1);
				gridsCanvas.Children.Add(tempLine);
			}
		}

		/// <summary>
		/// Enables changes to maze
		/// </summary>
		private void enableMazeModification()
		{
			if (!mazeModificationEnabled)
			{
				mazeModificationEnabled = true;
			}
		}

		/// <summary>
		/// Disables changes to maze
		/// </summary>
		private void disableMazeModification()
		{
			if (mazeModificationEnabled)
			{
				mazeModificationEnabled = false;
			}
		}

		/// <summary>
		/// Clears data related to the current maze
		/// </summary>
		private void disposeMaze()
		{
			if (maze != null)
			{
				hideMaze();
				mazeGrid.Children.Clear();
				gridsCanvas.Children.Clear();
				maze = null;
				startPos = null;
				finishPos = null;
			}
		}

		private void showMaze()
		{
			mazeContainer.Visibility = Visibility.Visible;
		}

		private void hideMaze()
		{
			mazeContainer.Visibility = Visibility.Collapsed;
		}

		private void showGrids()
		{
			gridsCanvas.Visibility = Visibility.Visible;
		}

		private void hideGrids()
		{
			gridsCanvas.Visibility = Visibility.Collapsed;
		}

		private void abortActiveThreads()
		{
			if (mazeThread != null)
			{
				mazeThread.Abort();
			}
		}

		private double getActivePanelTopMargin()
		{
			double topMargin = 0;
			switch (activePanel)
			{
				case Panels.NONE:
					topMargin = -minimizedPanelWidth * UIscale;
					break;
				case Panels.CREATE:
					topMargin = createMazeButton2.Margin.Top;
					break;
				case Panels.EDIT:
					topMargin = editMazeButton.Margin.Top;
					break;
				case Panels.SOLUTION:
					topMargin = solutionButton.Margin.Top;
					break;
				case Panels.RUN_THE_MAZE:
					topMargin = runTheMazeButton.Margin.Top;
					break;
				case Panels.SETTINGS:
					topMargin = Height - menuBar.Height - settingsButton.Margin.Bottom - settingsButton.Height;
					break;
				case Panels.ABOUT:
					topMargin = (Height - menuBar.Height) - aboutButton.Margin.Bottom - aboutButton.Height;
					break;
				default:
					break;
			}
			return topMargin;
		}

		/// <summary>
		/// Updates active panel indicator position
		/// </summary>
		private void updateActivePanelIndicator()
		{
			activePanelIndicator.Margin = new Thickness(0, getActivePanelTopMargin(), 0, 0);
		}

		private void openFullscreenPanel(Panels p)
		{
			if (!panelSwitchingInProgress)
			{
				if (!fullscreenPanelAnimationInProgress)
				{
					if (!panelAnimationInProgress)
					{
						fullscreenPanelAnimationInProgress = true;
						changeActivePanel(p);
						fullscreenPanelsGrid.Children[(byte)activePanel - 1].Visibility = Visibility.Visible;
						fullscreenPanelsGrid.Children[(byte)activePanel - 1].Opacity = 1;
						fullscreenPanel.Visibility = Visibility.Visible;
						fullscreenPanelExpandingStoryboard.Begin();
					}
				}
			}
		}

		private void closeFullscreenPanel()
		{
			if (!panelSwitchingInProgress)
			{
				if (!fullscreenPanelAnimationInProgress)
				{
					if (!panelAnimationInProgress)
					{
						fullscreenPanelAnimationInProgress = true;
						lastActivePanel = activePanel;
						changeActivePanel(Panels.NONE);
						coverLabel.Visibility = Visibility.Collapsed;
						coverLabel2.Visibility = Visibility.Collapsed;
						fullscreenPanelCollapsingStoryboard.Begin();
					}
				}
			}
		}

		private void collapseLeftPanel()
		{
			panelMaximized = false;
			panelExpandingStoryboard.Stop();
			panelCollapsingStoryboard.Begin();
		}

		// Prepare switching panels animations
		private void setSwitchingPanelAnimationsTargets(Grid entering, Grid leaving)
		{
			Storyboard.SetTarget(panelMenuInAnimation, entering);
			Storyboard.SetTarget(panelMenuFadeInAnimation, entering);
			Storyboard.SetTarget(panelMenuUnBlurAnimation, entering);
			Storyboard.SetTarget(panelMenuOutAnimation, leaving);
			Storyboard.SetTarget(panelMenuFadeOutAnimation, leaving);
			Storyboard.SetTarget(panelMenuBlurAnimation, leaving);
		}

		private void switchToPanel(Panels entering)
		{
			if (!panelSwitchingInProgress)
			{
				if (!fullscreenPanelAnimationInProgress)
				{
					panelSwitchingInProgress = true;
					//resetFocus();
					Grid enteringPanel = (Grid)fullscreenPanelsGrid.Children[(byte)entering - 1];
					leavingPanel = (Grid)fullscreenPanelsGrid.Children[(byte)activePanel - 1];
					setSwitchingPanelAnimationsTargets(enteringPanel, leavingPanel);
					enteringPanel.Opacity = 0;
					enteringPanel.Visibility = Visibility.Visible;
					lastActivePanel = activePanel;
					changeActivePanel(entering);
					panelMenuSwitchingOutStoryboard.Begin();
					panelMenuSwitchingInStoryboard.Begin();
				}
			}
		}

		private void changeActivePanel(Panels p)
		{
			activePanel = p;
			updateActivePanelIndicator();
		}

		/// <summary>
		/// Displays not solvable maze message on the screen
		/// </summary>
		private void displayNotSolvableMessage()
		{
			notSolvableMessageStoryboard.Stop();
			notSolvableToastGrid.Opacity = 0;
			notSolvableToastGrid.Visibility = Visibility.Visible;
			notSolvableMessageStoryboard.Begin();
		}

		/// <summary>
		/// Prepare animation seek bar
		/// </summary>
		/// <param name="max"></param>
		private void initializeSolutionViewer(short max)
		{
			solutionSeekerSlider.IsEnabled = true;
			currentStep = (short)solutionSeekerSlider.Minimum;
			solutionSeekerSlider.Maximum = max - 1;
			solutionSeekerSlider.Value = solutionSeekerSlider.Minimum;
			if (solutionSeekerSlider.Maximum <= 10)
			{
				solutionSeekerSlider.TickFrequency = 1;
			}
			else if (solutionSeekerSlider.Maximum <= 100)
			{
				solutionSeekerSlider.TickFrequency = 10;
			}
			else if (solutionSeekerSlider.Maximum <= 1000)
			{
				solutionSeekerSlider.TickFrequency = 100;
			}
			else
			{
				solutionSeekerSlider.TickFrequency = 1000;
			}
			solutionViewerControlGrid.Visibility = Visibility.Visible;
			solutionViewerControlGrid.Margin = new Thickness(0);
			solutionSeekerSlider.Focus();
			updateStepNumber();
			solutionMode = true;
		}

		/// <summary>
		/// Ends to solution animation and hides solution viewer
		/// </summary>
		private void ExitSolutionMode()
		{
			pause = true;
			updateMazeGrid();
			solutionViewerControlGrid.Margin = new Thickness(0, 0, 0, -solutionViewerControlGrid.Height);
			solutionViewerControlGrid.Visibility = Visibility.Collapsed;
			solutionSeekerSlider.IsEnabled = false;
			// Clear previous solutions
			if (maze != null)
			{
				maze.ClearSolutions();
			}
			solutionMode = false;
		}

		/// <summary>
		/// Updates solution step number as the seek bar moves
		/// </summary>
		private void updateStepNumber()
		{
			stepNumberTextBlock.Text = string.Format(stepNumberFormat, (short)solutionSeekerSlider.Value, solutionSeekerSlider.Maximum);
		}

		/// <summary>
		/// Updates animation steps delay time
		/// </summary>
		private void updateSolutionStepsDelayTime()
		{
			solutionStepsDelayTime = (ushort)(1000 / elementPerSecond / solutionAnimationSpeedRatio);
		}

		/// <summary>
		/// Change naimation play state
		/// </summary>
		private void playButtonClick()
		{
			if (!pause)
			{
				pause = true;
				pauseSolutionButton.Visibility = Visibility.Collapsed;
				playSolutionButton.Visibility = Visibility.Visible;
			}
			else
			{
				PlaySolution();
				playSolutionButton.Visibility = Visibility.Collapsed;
				pauseSolutionButton.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Stop playing animation
		/// </summary>
		private void StopSolution()
		{
			pause = true;
			solutionSeekerSlider.Value = 0;
			pauseSolutionButton.Visibility = Visibility.Collapsed;
			playSolutionButton.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Starts playing animation
		/// </summary>
		private void PlaySolution()
		{
			pause = false;
			disableMazeModification();
			// return to beginning
			if (solutionSeekerSlider.Value == solutionSeekerSlider.Maximum)
			{
				solutionSeekerSlider.Value = solutionSeekerSlider.Minimum;
			}
			// initialize solution steps delay time
			updateSolutionStepsDelayTime();

			mazeThread = new Thread(() =>
			{
				do
				{
					Dispatcher.Invoke(() =>
					{
						solutionSeekerSlider.Value++;
					});
					stopwatch.Reset();
					stopwatch.Start();
					while (stopwatch.Elapsed.TotalMilliseconds < solutionStepsDelayTime)
					{
						Thread.Sleep(1);
					}
				} while (!pause);
				enableMazeModification();
			});
			mazeThread.IsBackground = true;
			mazeThread.Start();
		}

		/// <summary>
		/// Renders wanted frame in the solution process
		/// </summary>
		private void updateSolutionFrame()
		{
			// A frame forward
			if (currentStep < (short)solutionSeekerSlider.Value)
			{
				for (short i = currentStep; i < (short)solutionSeekerSlider.Value; i++)
				{
					if (maze.SolutionPath[i + 1] == finishPos)
					{
						getLabel(maze.SolutionPath[i + 1]).Background = new SolidColorBrush(colorScheme.StartPositionColor);
					}
					else
					{
						getLabel(maze.SolutionPath[i + 1]).Background = new SolidColorBrush(colorScheme.SolutionPathColor);
					}
					currentStep++;
				}
			}
			// A frame backward
			else if (currentStep > (short)solutionSeekerSlider.Value)
			{
				for (short i = currentStep; i > (short)solutionSeekerSlider.Value; i--)
				{
					if (maze.SolutionPath[i] == finishPos)
					{
						getLabel(maze.SolutionPath[i]).Background = new SolidColorBrush(colorScheme.FinishPositionColor);
					}
					else
					{
						getLabel(maze.SolutionPath[i]).Background = new SolidColorBrush(colorScheme.MazeBackgroundColor);
					}
					currentStep--;
				}
			}
		}

		/// <summary>
		/// Renders a frame in the solution process of mouse solving alg
		/// </summary>
		private void updateMouseAlgSolutionProcedureFrame()
		{
			// A frame forward
			if (currentStep < (short)solutionSeekerSlider.Value)
			{
				for (short i = currentStep; i < (short)solutionSeekerSlider.Value; i++)
				{
					if (maze.MouseAlgSolutionProcedure[i + 1] == finishPos)
					{
						getLabel(maze.MouseAlgSolutionProcedure[i + 1]).Background = new SolidColorBrush(colorScheme.StartPositionColor);
					}
					else if (maze.MouseAlgSolutionProcedure[i + 1] == startPos)
					{
						((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i]).Background).Color = colorScheme.AlternateSolutionPathColor;
					}
					else if (((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i + 1]).Background).Color == colorScheme.SolutionPathColor)
					{
						if (maze.MouseAlgSolutionProcedure[i] != startPos)
						{
							((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i]).Background).Color = colorScheme.AlternateSolutionPathColor;
						}
					}
					else
					{
						getLabel(maze.MouseAlgSolutionProcedure[i + 1]).Background = new SolidColorBrush(colorScheme.SolutionPathColor);
					}
					currentStep++;
				}
			}
			// A frame backward
			else if (currentStep > (short)solutionSeekerSlider.Value)
			{
				for (short i = currentStep; i > (short)solutionSeekerSlider.Value; i--)
				{
					if (maze.MouseAlgSolutionProcedure[i] == finishPos)
					{
						((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i]).Background).Color = colorScheme.FinishPositionColor;
					}
					else if (maze.MouseAlgSolutionProcedure[i - 1] == startPos)
					{
						((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i]).Background).Color = colorScheme.MazeBackgroundColor;
						((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i - 1]).Background).Color = colorScheme.StartPositionColor;
					}
					else if (((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i - 1]).Background).Color == colorScheme.AlternateSolutionPathColor)
					{
						((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i - 1]).Background).Color = colorScheme.SolutionPathColor;
					}
					else
					{
						((SolidColorBrush)getLabel(maze.MouseAlgSolutionProcedure[i]).Background).Color = colorScheme.MazeBackgroundColor;
					}
					currentStep--;
				}
			}
		}

		/// <summary>
		/// Renders a frame in the solution process of fluid simulation alg
		/// </summary>
		private void updateFluidAlgSolutionProcedureFrame()
		{
			// A frame forward
			if (currentStep < (short)solutionSeekerSlider.Value)
			{
				for (short i = currentStep; i < (short)solutionSeekerSlider.Value; i++)
				{
					ElementPosition[] edgeElements = maze.FluidAlgSolutionProcedure[i + 1];
					for (short j = 0; j < edgeElements.Length; j++)
					{
						foreach (ElementPosition item in edgeElements)
						{
							((SolidColorBrush)getLabel(item).Background).Color = colorScheme.SolutionPathColor;
						}
					}
					if (i > 0)
					{
						ElementPosition[] lastEdgeElements = maze.FluidAlgSolutionProcedure[i];
						foreach (ElementPosition item in lastEdgeElements)
						{
							((SolidColorBrush)getLabel(item).Background).Color = colorScheme.AlternateSolutionPathColor;
						}
					}
					currentStep++;
					if (currentStep == (short)solutionSeekerSlider.Maximum)
					{
						if (contains(maze.FluidAlgSolutionProcedure[currentStep], finishPos))
						{
							((SolidColorBrush)getLabel(finishPos).Background).Color = colorScheme.StartPositionColor;
						}
					}
				}
			}
			// A frame backward
			else if (currentStep > (short)solutionSeekerSlider.Value)
			{
				for (short i = currentStep; i > (short)solutionSeekerSlider.Value; i--)
				{
					ElementPosition[] edgeElements = maze.FluidAlgSolutionProcedure[i - 1];
					for (short j = 0; j < edgeElements.Length; j++)
					{
						foreach (ElementPosition item in edgeElements)
						{
							if (item != startPos)
							{
								((SolidColorBrush)getLabel(item).Background).Color = colorScheme.SolutionPathColor;
							}
						}
					}
					ElementPosition[] lastEdgeElements = maze.FluidAlgSolutionProcedure[i];
					foreach (ElementPosition item in lastEdgeElements)
					{
						((SolidColorBrush)getLabel(item).Background).Color = colorScheme.MazeBackgroundColor;
					}
					currentStep--;
					if (currentStep != (short)solutionSeekerSlider.Maximum)
					{
						((SolidColorBrush)getLabel(finishPos).Background).Color = colorScheme.FinishPositionColor;
					}
				}
			}
		}

		/// <summary>
		/// Check if an aray contains a specific element
		/// </summary>
		/// <param name="array"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		private bool contains(ElementPosition[] array, ElementPosition pos)
		{
			foreach (ElementPosition item in array)
			{
				if (item == pos)
				{
					return true;
				}
			}
			return false;
		}
	}
}