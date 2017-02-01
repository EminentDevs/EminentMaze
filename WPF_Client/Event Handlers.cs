using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shell;

namespace WPF_Client
{
	public partial class MainWindow : Window
	{
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			abortActiveThreads();
			Close();
		}

		private void fullscreenButton_Click(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Normal)
			{
				WindowState = WindowState.Maximized;
			}
			else
			{
				WindowState = WindowState.Normal;
			}
		}

		private void minimizeButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void HandlerThatCloseWindow(object obSender, ExecutedRoutedEventArgs e)
		{
			abortActiveThreads();
			Close();
		}

		private void HandlerThatMinimizeWindow(object obSender, ExecutedRoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (solutionMode)
			{
				solutionSpeedSlider.Value += e.Delta / 120 * solutionSpeedSlider.SmallChange;
			}
		}

		// Handles keyboard key down
		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.IsRepeat)
			{
				if (e.Key.Equals(Key.Escape) && Keyboard.Modifiers == ModifierKeys.None)
				{
					if (fullscreenPanelOpened)
					{
						closeFullscreenPanel();
					}
				}
				else if (e.Key.Equals(Key.Space) &&
					Keyboard.Modifiers == ModifierKeys.None
					)
				{
					if (solutionMode)
					{
						if (activePanel == Panels.NONE)
						{
							playButtonClick();
						}
					}
				}
				else if (e.Key.Equals(blockHotkey) &&
					!Keyboard.GetKeyStates(emptyHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(startHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(finishHotkey).Equals(KeyStates.Down) &&
					Keyboard.Modifiers == ModifierKeys.None
					)
				{
					activeHotkey = Hotkeys.BLOCK;
					if (!string.IsNullOrEmpty(currentPointingLabelName))
					{
						setBlock(getLabel(getLabelPosition(currentPointingLabelName)));
					}
				}
				else if (e.Key.Equals(emptyHotkey) &&
					!Keyboard.GetKeyStates(blockHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(startHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(finishHotkey).Equals(KeyStates.Down) &&
					Keyboard.Modifiers == ModifierKeys.None
					)
				{
					activeHotkey = Hotkeys.EMPTY;
					if (!string.IsNullOrEmpty(currentPointingLabelName))
					{
						setEmpty(getLabel(getLabelPosition(currentPointingLabelName)));
					}
				}
				else if (e.Key.Equals(startHotkey) &&
					!Keyboard.GetKeyStates(blockHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(emptyHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(finishHotkey).Equals(KeyStates.Down) &&
					Keyboard.Modifiers == ModifierKeys.None
					)
				{
					activeHotkey = Hotkeys.START;
					if (!string.IsNullOrEmpty(currentPointingLabelName))
					{
						setStart(getLabel(getLabelPosition(currentPointingLabelName)));
					}
				}
				else if (e.Key.Equals(finishHotkey) &&
					!Keyboard.GetKeyStates(blockHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(emptyHotkey).Equals(KeyStates.Down) &&
					!Keyboard.GetKeyStates(startHotkey).Equals(KeyStates.Down) &&
					Keyboard.Modifiers == ModifierKeys.None
					)
				{
					activeHotkey = Hotkeys.FINISH;
					if (!string.IsNullOrEmpty(currentPointingLabelName))
					{
						setFinish(getLabel(getLabelPosition(currentPointingLabelName)));
					}
				}
			}
		}

		private void MainWindow_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key.Equals(blockHotkey) || e.Key.Equals(emptyHotkey) || e.Key.Equals(startHotkey) || e.Key.Equals(finishHotkey))
			{
				activeHotkey = Hotkeys.NONE;
			}
		}

		private void MainWindow_StateChanged(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				WindowChrome.SetWindowChrome(this, new WindowChrome() { CaptionHeight = 0, ResizeBorderThickness = new Thickness(0) });
				Visibility = Visibility.Collapsed;
				ResizeMode = ResizeMode.NoResize;
				fullscreenButton.Visibility = Visibility.Collapsed;
				resizeWindowButton.Visibility = Visibility.Visible;
				Visibility = Visibility.Visible;
			}
			else
			{
				WindowChrome.SetWindowChrome(this, new WindowChrome() { CaptionHeight = 0, ResizeBorderThickness = new Thickness(windowBorderThickness) });
				ResizeMode = ResizeMode.CanResize;
				fullscreenButton.Visibility = Visibility.Visible;
				resizeWindowButton.Visibility = Visibility.Collapsed;
			}
			updateActivePanelIndicator();
		}

		private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			updateActivePanelIndicator();
		}

		private void menuBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				DragMove();
			}
		}

		private void menuBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				if (WindowState == WindowState.Normal)
				{
					WindowState = WindowState.Maximized;
				}
				else
				{
					WindowState = WindowState.Normal;
				}
			}
		}

		private void disposeMazeButton_Click(object sender, RoutedEventArgs e)
		{
			if (maze != null)
			{
				ExitSolutionMode();
				disposeMaze();
				closeFullscreenPanel();
			}
		}

		private void randomBlocksByPercentButton_Click(object sender, RoutedEventArgs e)
		{
			if (validatePercent(blockPercentTextBox.Text))
			{
				if (maze != null)
				{
					ExitSolutionMode();
					maze.RandomizeMazeBlocksByPercent(byte.Parse(blockPercentTextBox.Text));
					updateMazeGrid();
					closeFullscreenPanel();
				}
			}
			else
			{
				invalidPercentTextBlock.Visibility = Visibility.Visible;
			}
		}

		private void randomBlocksUsingDepthFirstAlgButton_Click(object sender, RoutedEventArgs e)
		{
			if (maze != null)
			{
				ExitSolutionMode();
				maze.RandomizeBlocksUsingDepthFirstSearchAlg();
				updateMazeGrid();
				closeFullscreenPanel();
			}
		}

		private void MainWindow_MouseLeave(object sender, MouseEventArgs e)
		{
			MouseLeave -= MainWindow_MouseLeave;
			MouseUp -= MainWindow_MouseUp;
		}

		private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MouseLeave -= MainWindow_MouseLeave;
			MouseUp -= MainWindow_MouseUp;
		}

		private void MazeGrid_MouseLeave(object sender, MouseEventArgs e)
		{
			currentPointingLabelName = null;
		}

		private void element_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (mazeModificationEnabled)
			{
				MouseUp += MainWindow_MouseUp;
				MouseLeave += MainWindow_MouseLeave;

				if (Mouse.LeftButton == MouseButtonState.Pressed)
				{
					if (Mouse.RightButton != MouseButtonState.Pressed)
					{
						setBlock(sender);
					}
				}
				else if (Mouse.RightButton == MouseButtonState.Pressed)
				{
					if (Mouse.LeftButton != MouseButtonState.Pressed)
					{
						setEmpty(sender);
					}
				}
			}
		}

		private void element_MouseEnter(object sender, MouseEventArgs e)
		{
			if (mazeModificationEnabled)
			{
				currentPointingLabelName = ((Label)sender).Name;

				if (Mouse.LeftButton == MouseButtonState.Pressed)
				{
					if (Mouse.RightButton == MouseButtonState.Released)
					{
						setBlock(sender);
					}
				}
				if (Mouse.RightButton == MouseButtonState.Pressed)
				{
					if (Mouse.LeftButton == MouseButtonState.Released)
					{
						setEmpty(sender);
					}
				}
				else
				{
					switch (activeHotkey)
					{
						case Hotkeys.BLOCK:
							setBlock(sender);
							break;
						case Hotkeys.EMPTY:
							setEmpty(sender);
							break;
						case Hotkeys.START:
							if (maze.StartPos != null)
							{
								if (maze.StartPos != getLabelPosition((sender as Label).Name))
								{
									setStart(sender);
								}
							}
							else
							{
								setStart(sender);
							}
							break;
						case Hotkeys.FINISH:
							if (maze.FinishPos != null)
							{
								if (maze.FinishPos != getLabelPosition((sender as Label).Name))
								{
									setFinish(sender);
								}
							}
							else
							{
								setFinish(sender);
							}
							break;
						default:
							break;
					}
				}
			}
		}

		private void clearMazeButton_Click(object sender, RoutedEventArgs e)
		{
			if (maze != null)
			{
				maze.ClearMaze();
				ExitSolutionMode();
				closeFullscreenPanel();
			}
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
			menuBarItems.Opacity = 0.5;
			currentPointingLabelName = null;
		}

		private void Window_Activated(object sender, EventArgs e)
		{
			menuBarItems.Opacity = 1;
		}

		private void inputBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBox t = (TextBox)sender;
			if (!string.IsNullOrEmpty(t.Text))
			{
				t.SelectionStart = 0;
				t.SelectionLength = t.Text.Length;
			}
		}

		private void widthInputBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!validateMazeWidth((sender as TextBox).Text))
			{
				if ((sender as TextBox).Name == "widthInputTextBox")
				{
					invalidWidthInputTextBlock.Visibility = Visibility.Visible;
				}
				else
				{
					invalidHeightInputTextBlock.Visibility = Visibility.Visible;
				}
			}
			else
			{
				if ((sender as TextBox).Name == "widthInputTextBox")
				{
					invalidWidthInputTextBlock.Visibility = Visibility.Collapsed;
				}
				else
				{
					invalidHeightInputTextBlock.Visibility = Visibility.Collapsed;
				}
			}
		}

		private void solutionSpeedSlider_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Mouse.RightButton == MouseButtonState.Pressed && Mouse.LeftButton == MouseButtonState.Released)
			{
				solutionSpeedSlider.Value = 0;
			}
		}

		private void resizeSolutionViewerButton_Click(object sender, RoutedEventArgs e)
		{
			if (solutionViewerMaximized)
			{
				solutionViewerControlGrid.Margin = new Thickness(0, 0, 0, -solutionViewerControlGrid.Height + resizeSolutionViewerButton.Height);
				((ImageBrush)resizeSolutionViewerButton.Background).RelativeTransform = new RotateTransform(180, 0.5, 0.5);
				solutionViewerMaximized = false;
			}
			else
			{
				solutionViewerControlGrid.Margin = new Thickness(0);
				((ImageBrush)resizeSolutionViewerButton.Background).RelativeTransform = new RotateTransform(0, 0.5, 0.5);
				solutionViewerMaximized = true;
			}
		}

		private void blockPercentTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!validatePercent(blockPercentTextBox.Text))
			{
				invalidPercentTextBlock.Visibility = Visibility.Visible;
			}
			else
			{
				invalidPercentTextBlock.Visibility = Visibility.Collapsed;
			}
		}

		private void solveUsingMouseAlgButton_Click(object sender, RoutedEventArgs e)
		{
			if (solutionMode)
			{
				ExitSolutionMode();
			}
			if (maze != null)
			{
				if (maze.StartPos != null && maze.FinishPos != null)
				{
					solveMethod = SolveMethod.MOUSE;
					// display solution path
					if (mouseAlgSolutionPathRadioButton.IsChecked.Value)
					{
						if (maze.SolveUsingMouseAlg(useRandomDirectionCheckBox.IsChecked.Value))
						{
							closeFullscreenPanel();
							displayMethod = DisplayMethod.SOLUTION;
							initializeSolutionViewer((short)maze.SolutionPath.Length);
						}
						else
						{
							closeFullscreenPanel();
							displayNotSolvableMessage();
						}
					}
					// display solution procedure
					else
					{
						maze.SolveUsingMouseAlg(useRandomDirectionCheckBox.IsChecked.Value);
						closeFullscreenPanel();
						displayMethod = DisplayMethod.PROCEDURE;
						initializeSolutionViewer((short)maze.MouseAlgSolutionProcedure.Count);
					}
				}
			}
		}

		private void solveUsingFluidButton_Click(object sender, RoutedEventArgs e)
		{
			if (solutionMode)
			{
				ExitSolutionMode();
			}
			if (maze != null)
			{
				if (maze.StartPos != null && maze.FinishPos != null)
				{
					solveMethod = SolveMethod.FLUID;
					// display solution path
					if (fluidAlgSolutionPathRadioButton.IsChecked.Value)
					{
						if (maze.SolveUsingFluidSimulationAlg())
						{
							closeFullscreenPanel();
							displayMethod = DisplayMethod.SOLUTION;
							initializeSolutionViewer((short)maze.SolutionPath.Length);
						}
						else
						{
							closeFullscreenPanel();
							displayNotSolvableMessage();
						}
					}
					// display solution procedure
					else
					{
						maze.SolveUsingFluidSimulationAlg();
						closeFullscreenPanel();
						displayMethod = DisplayMethod.PROCEDURE;
						initializeSolutionViewer((short)maze.FluidAlgSolutionProcedure.Count);
					}
				}
			}
		}

		private void closeFullscreenPanelLabel_Click(object sender, RoutedEventArgs e)
		{
			closeFullscreenPanel();
		}

		private void expandPanelButton_Click(object sender, RoutedEventArgs e)
		{
			if (!panelMaximized)
			{
				if (!fullscreenPanelAnimationInProgress)
				{
					panelAnimationInProgress = true;
					panelMaximized = true;
					panelCollapsingStoryboard.Stop();
					panelExpandingStoryboard.Begin();
				}
			}
			else
			{
				if (!fullscreenPanelAnimationInProgress)
				{
					panelAnimationInProgress = true;
					panelMaximized = false;
					panelExpandingStoryboard.Stop();
					panelCollapsingStoryboard.Begin();
				}
			}
		}

		private void menuItem_Click(object sender, RoutedEventArgs e)
		{
			if (((Button)sender).Name == createMazeButton2.Name)
			{
				if (!fullscreenPanelOpened)
				{
					openFullscreenPanel(Panels.CREATE);
				}
				else
				{
					if (activePanel == Panels.CREATE)
					{
						closeFullscreenPanel();
					}
					else
					{
						switchToPanel(Panels.CREATE);
					}
				}
			}
			else if (((Button)sender).Name == editMazeButton.Name)
			{
				if (!fullscreenPanelOpened)
				{
					openFullscreenPanel(Panels.EDIT);
				}
				else
				{
					if (activePanel == Panels.EDIT)
					{
						closeFullscreenPanel();
					}
					else
					{
						switchToPanel(Panels.EDIT);
					}
				}
			}
			else if (((Button)sender).Name == solutionButton.Name)
			{
				if (!fullscreenPanelOpened)
				{
					openFullscreenPanel(Panels.SOLUTION);
				}
				else
				{
					if (activePanel == Panels.SOLUTION)
					{
						closeFullscreenPanel();
					}
					else
					{
						switchToPanel(Panels.SOLUTION);
					}
				}
			}
			else if (((Button)sender).Name == runTheMazeButton.Name)
			{
				if (!fullscreenPanelOpened)
				{
					openFullscreenPanel(Panels.RUN_THE_MAZE);
				}
				else
				{
					if (activePanel == Panels.RUN_THE_MAZE)
					{
						closeFullscreenPanel();
					}
					else
					{
						switchToPanel(Panels.RUN_THE_MAZE);
					}
				}
			}
			else if (((Button)sender).Name == settingsButton.Name)
			{
				if (!fullscreenPanelOpened)
				{
					openFullscreenPanel(Panels.SETTINGS);
				}
				else
				{
					if (activePanel == Panels.SETTINGS)
					{
						closeFullscreenPanel();
					}
					else
					{
						switchToPanel(Panels.SETTINGS);
					}
				}
			}
			else if (((Button)sender).Name == aboutButton.Name)
			{
				if (!fullscreenPanelOpened)
				{
					openFullscreenPanel(Panels.ABOUT);
				}
				else
				{
					if (activePanel == Panels.ABOUT)
					{
						closeFullscreenPanel();
					}
					else
					{
						switchToPanel(Panels.ABOUT);
					}
				}
			}
		}

		private void solutionSeekerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (maze != null)
			{
				updateStepNumber();
				if (solveMethod == SolveMethod.MOUSE)
				{
					// Update solution
					if (displayMethod == DisplayMethod.SOLUTION)
					{
						updateSolutionFrame();
					}
					// Update solve procedure
					else
					{
						updateMouseAlgSolutionProcedureFrame();
					}
				}
				else if (solveMethod == SolveMethod.FLUID)
				{
					// Update solution
					if (displayMethod == DisplayMethod.SOLUTION)
					{
						updateSolutionFrame();
					}
					// Update solve procedure
					else
					{
						updateFluidAlgSolutionProcedureFrame();
					}
				}
			}
			if (solutionSeekerSlider.Value == solutionSeekerSlider.Maximum)
			{
				pause = true;
				pauseSolutionButton.Visibility = Visibility.Collapsed;
				playSolutionButton.Visibility = Visibility.Visible;
			}
		}

		private void solutionSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			solutionAnimationSpeedRatio = Math.Pow(20, solutionSpeedSlider.Value);
			updateSolutionStepsDelayTime();
		}

		private void playSolutionButton_Click(object sender, RoutedEventArgs e)
		{
			playButtonClick();
		}

		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}

		private void showGridsCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			showGrids();
			displayMazeGrids = true;
		}

		private void showGridsCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			hideGrids();
			displayMazeGrids = false;
		}

		// UI scale buttons event handlers
		private void UI80Button_Click(object sender, RoutedEventArgs e)
		{
			if (UIscaleStatus != UIscales._80)
			{
				solutionSeekerSlider.Value = 0; pause = true;
				UIscale = 0.8;
				UIscaleStatus = UIscales._80;
				UI80Button.FontWeight = FontWeights.Black;
				UI100Button.FontWeight = FontWeights.Normal;
				UI120Button.FontWeight = FontWeights.Normal;
				UI140Button.FontWeight = FontWeights.Normal;
				resetUIProperties();
				closeFullscreenPanel();
				collapseLeftPanel();
				if (WindowState == WindowState.Maximized)
				{
					WindowState = WindowState.Normal;
					WindowState = WindowState.Maximized;
				}
			}
		}

		private void UI100Button_Click(object sender, RoutedEventArgs e)
		{
			if (UIscaleStatus != UIscales._100)
			{
				solutionSeekerSlider.Value = 0; pause = true;
				UIscale = 1;
				UIscaleStatus = UIscales._100;
				UI80Button.FontWeight = FontWeights.Normal;
				UI100Button.FontWeight = FontWeights.Black;
				UI120Button.FontWeight = FontWeights.Normal;
				UI140Button.FontWeight = FontWeights.Normal;
				resetUIProperties();
				closeFullscreenPanel();
				collapseLeftPanel();
				if (WindowState == WindowState.Maximized)
				{
					WindowState = WindowState.Normal;
					WindowState = WindowState.Maximized;
				}
			}
		}

		private void UI120Button_Click(object sender, RoutedEventArgs e)
		{
			if (UIscaleStatus != UIscales._120)
			{
				StopSolution();
				UIscale = 1.20;
				UIscaleStatus = UIscales._120;
				UI100Button.FontWeight = FontWeights.Normal;
				UI120Button.FontWeight = FontWeights.Black;
				UI140Button.FontWeight = FontWeights.Normal;
				resetUIProperties();
				closeFullscreenPanel();
				collapseLeftPanel();
				if (WindowState == WindowState.Maximized)
				{
					WindowState = WindowState.Normal;
					WindowState = WindowState.Maximized;
				}
			}
		}

		private void UI140Button_Click(object sender, RoutedEventArgs e)
		{
			if (UIscaleStatus != UIscales._140)
			{
				StopSolution();
				UIscale = 1.4;
				UIscaleStatus = UIscales._140;
				UI80Button.FontWeight = FontWeights.Normal;
				UI100Button.FontWeight = FontWeights.Normal;
				UI120Button.FontWeight = FontWeights.Normal;
				UI140Button.FontWeight = FontWeights.Black;
				resetUIProperties();
				closeFullscreenPanel();
				collapseLeftPanel();
				if (WindowState == WindowState.Maximized)
				{
					WindowState = WindowState.Normal;
					WindowState = WindowState.Maximized;
				}
			}
		}

		// Color scheme buttons event handlers
		private void blueColorSchemeButton_Click(object sender, RoutedEventArgs e)
		{
			if (colorScheme != ColorSchemes.Blue)
			{
				StopSolution();
				colorScheme = ColorSchemes.Blue;
				resetUIProperties();
				closeFullscreenPanel();
			}
		}

		private void greenColorSchemeButton_Click(object sender, RoutedEventArgs e)
		{
			if (colorScheme != ColorSchemes.Green)
			{
				StopSolution();
				colorScheme = ColorSchemes.Green;
				resetUIProperties();
				closeFullscreenPanel();
			}
		}

		private void redColorSchemeButton_Click(object sender, RoutedEventArgs e)
		{
			if (colorScheme != ColorSchemes.Red)
			{
				StopSolution();
				colorScheme = ColorSchemes.Red;
				resetUIProperties();
				closeFullscreenPanel();
			}
		}

		private void yellowColorSchemeButton_Click(object sender, RoutedEventArgs e)
		{
			if (colorScheme != ColorSchemes.Yellow)
			{
				StopSolution();
				colorScheme = ColorSchemes.Yellow;
				resetUIProperties();
				closeFullscreenPanel();
			}
		}

		private void pinkColorSchemeButton_Click(object sender, RoutedEventArgs e)
		{
			if (colorScheme != ColorSchemes.Pink)
			{
				StopSolution();
				colorScheme = ColorSchemes.Pink;
				resetUIProperties();
				closeFullscreenPanel();
			}
		}

		private void grayColorSchemeButton_Click(object sender, RoutedEventArgs e)
		{
			if (colorScheme != ColorSchemes.Gray)
			{
				StopSolution();
				colorScheme = ColorSchemes.Gray;
				resetUIProperties();
				closeFullscreenPanel();
			}
		}
	}
}