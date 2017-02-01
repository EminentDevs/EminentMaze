using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace WPF_Client
{
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Initializes UI items in the main window
		/// </summary>
		private void initWindow()
		{
			if (WindowState != WindowState.Maximized)
			{
				WindowChrome.SetWindowChrome(this, new WindowChrome() { CaptionHeight = 0, ResizeBorderThickness = new Thickness(windowBorderThickness * UIscale) });
			}
			MinHeight = menuBarHeight * UIscale + panelButtonsGrid.Children.Count * minimizedPanelWidth * UIscale;
			MinWidth = MinHeight * 1.4;
			// Spash Screen
			((Image)splashScreenGrid.Children[0]).Width = 120 * UIscale;
			((Image)splashScreenGrid.Children[1]).Width = 40 * UIscale;
			((Image)splashScreenGrid.Children[1]).Margin = new Thickness(0, 0, 0, 25 * UIscale);
			((TextBlock)splashScreenGrid.Children[2]).FontSize = 11 * UIscale;
			((TextBlock)splashScreenGrid.Children[2]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)splashScreenGrid.Children[2]).Margin = new Thickness(0, 0, 0, 7 * UIscale);
			mainGrid.Background = new SolidColorBrush(colorScheme.MainGridColor);
			coverLabel.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			coverLabel2.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
		}

		/// <summary>
		/// Initializes UI items in the menu bar
		/// </summary>
		private void initMenuBar()
		{
			menuBar.Height = menuBarHeight * UIscale;
			menuBar.Background = new SolidColorBrush(colorScheme.MenuBarColor);
			windowTitleTextBlock.FontSize = windowTitleFontSize * UIscale;
			windowTitleTextBlock.Margin = new Thickness(windowTitleLeftMargin * UIscale, 0, 0, 0);
			foreach (object item in menuBarItems.Children)
			{
				if (item is Button)
				{
					((Button)item).Height = menuBar.Height;
					((Button)item).Width = menuBar.Height;
					((ImageBrush)((Button)(item)).Background).RelativeTransform = new ScaleTransform(UIscale, UIscale, 0.5, 0.5);
				}
			}
			fullscreenButton.Margin = new Thickness(0, 0, menuBar.Height, 0);
			resizeWindowButton.Margin = new Thickness(0, 0, menuBar.Height, 0);
			minimizeButton.Margin = new Thickness(0, 0, 2 * menuBar.Height, 0);
		}

		/// <summary>
		/// Initializes UI items in the left panel
		/// </summary>
		private void initPanel()
		{
			panel.Visibility = Visibility.Visible;
			panel.Margin = new Thickness(0, menuBar.Height, 0, 0);
			panel.Width = minimizedPanelWidth * UIscale;
			panel.Background = new SolidColorBrush(colorScheme.PanelColor);
			for (byte i = 0; i < panelButtonsGrid.Children.Count; i++)
			{
				object item = panelButtonsGrid.Children[i];
				if (item is Button)
				{
					Button button = (Button)item;
					button.Height = minimizedPanelWidth * UIscale;
					button.Width = maximizedPanelWidth * UIscale;
					((Grid)(button.Content)).Width = maximizedPanelWidth * UIscale;
					if (button.VerticalAlignment == VerticalAlignment.Top)
					{
						button.Margin = new Thickness(0, i * minimizedPanelWidth * UIscale, 0, 0);
					}
					else
					{
						button.Margin = new Thickness(0, 0, 0, (panelButtonsGrid.Children.Count - i - 1) * minimizedPanelWidth * UIscale);
					}
					foreach (object x in ((Grid)(button.Content)).Children)
					{
						if (x is Grid)
						{
							((TextBlock)((Grid)x).Children[0]).FontSize = ((item == panelButtonsGrid.Children[0]) ? panelTitleFontSize : buttonsFontSize) * UIscale;
							((TextBlock)((Grid)x).Children[0]).Width = (maximizedPanelWidth - minimizedPanelWidth) * UIscale;
							((TextBlock)((Grid)x).Children[0]).Margin = new Thickness((minimizedPanelWidth + 5) * UIscale, 0, 0, 0);
						}
						else if (x is Label)
						{
							if (((Label)x).Background != null)
							{
								if (((Label)x).Background is ImageBrush)
								{
									((Label)x).Height = minimizedPanelWidth * UIscale;
									((Label)x).Width = minimizedPanelWidth * UIscale;
									((ImageBrush)(((Label)x).Background)).RelativeTransform = new ScaleTransform(panelIconsScale * UIscale, panelIconsScale * UIscale, 0.5, 0.5);
								}
							}
							else
							{
								((Label)x).Margin = new Thickness(0);
								((Label)x).Width = maximizedPanelWidth * UIscale;
								((Label)x).Height = minimizedPanelWidth * UIscale;

							}
						}
						else if (x is Line)
						{
							if (button.Name == expandPanelButton.Name)
							{
								((Line)x).X1 = 0;
								((Line)x).X2 = maximizedPanelWidth * UIscale;
								((Line)x).Y1 = minimizedPanelWidth * UIscale - menuSeparatorThickness * UIscale / 2;
								((Line)x).Y2 = minimizedPanelWidth * UIscale - menuSeparatorThickness * UIscale / 2;
								((Line)x).StrokeThickness = menuSeparatorThickness * UIscale;
							}
							else if (button.Name == settingsButton.Name)
							{
								if (button.Name == settingsButton.Name)
								{
									((Line)x).X1 = 0;
									((Line)x).X2 = maximizedPanelWidth * UIscale;
									((Line)x).Y1 = menuSeparatorThickness * UIscale / 2;
									((Line)x).Y2 = menuSeparatorThickness * UIscale / 2;
									((Line)x).StrokeThickness = menuSeparatorThickness * UIscale;
								}
							}
						}
					}
				}
			}
			activePanelIndicator.Height = minimizedPanelWidth * UIscale;
			activePanelIndicator.Width = activePanelIndicatorWidth * UIscale;
			activePanelIndicator.Background = new SolidColorBrush(colorScheme.ActivePanelIndicatorColor);
		}

		/// <summary>
		/// Initializes UI items in the container
		/// </summary>
		private void initContainer()
		{
			container.Margin = new Thickness(panel.Width, menuBar.Height, 0, 0);
			mazeContainer.Margin = new Thickness(mazeContainerMargin * UIscale);
			if (maze != null)
			{
				initMazeContainer();
				updateMazeGrid();
				initMazeGrids();
			}
			foreach (Label label in mazeGrid.Children)
			{
				label.Width = elementSize * UIscale;
				label.Height = elementSize * UIscale;
				label.Margin = new Thickness(getLabelCol(label.Name) * elementSize * UIscale, getLabelRow(label.Name) * elementSize * UIscale, 0, 0);
			}
			// Not solvable message
			notSolvableToastGrid.Height = 40 * UIscale;
			notSolvableToastGrid.Width = 170 * UIscale;
			notSolvableToastGrid.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((TextBlock)notSolvableToastGrid.Children[0]).FontSize = 18 * UIscale;
			((TextBlock)notSolvableToastGrid.Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);

			coverLabel.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			coverLabel2.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			fullscreenPanel.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			fullscreenPanel.Visibility = Visibility.Collapsed;
			fullscreenPanel.Opacity = 0;
			fullscreenPanel.Margin = new Thickness(-fullscreenPanelTravelDistance * UIscale, 0, fullscreenPanelTravelDistance * UIscale, 0);
		}

		/// <summary>
		/// Initializes UI items in the solution viewer
		/// </summary>
		private void initsolutionViewerControl()
		{
			solutionViewerControlGrid.Width = 180 * UIscale;
			solutionViewerControlGrid.Height = 90 * UIscale;
			solutionViewerControlGrid.Background = new SolidColorBrush(new Color() { A = 160, R = colorScheme.FullscreenPanelColor.R, G = colorScheme.FullscreenPanelColor.G, B = colorScheme.FullscreenPanelColor.B });
			resizeSolutionViewerButton.Height = 20 * UIscale;
			resizeSolutionViewerButton.Width = solutionViewerControlGrid.Width;
			playSolutionButton.Width = 30 * UIscale;
			playSolutionButton.Height = 30 * UIscale;
			playSolutionButton.Margin = new Thickness(20 * UIscale);
			pauseSolutionButton.Width = 30 * UIscale;
			pauseSolutionButton.Height = 30 * UIscale;
			pauseSolutionButton.Margin = new Thickness(20 * UIscale);
			solutionSpeedSlider.Width = 75 * UIscale;
			solutionSpeedSlider.Margin = new Thickness(0, 0, 15 * UIscale, 50 * UIscale);
			solutionSeekerSlider.Width = 150 * UIscale;
			solutionSeekerSlider.Margin = new Thickness(0, 0, 0, 10 * UIscale);
			stepNumberTextBlock.FontSize = 8 * UIscale;
			stepNumberTextBlock.Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			stepNumberTextBlock.Margin = new Thickness(0, 0, 0, 35 * UIscale);
			((TextBlock)solutionViewerControlGrid.Children[6]).FontSize = 8 * UIscale;
			((TextBlock)solutionViewerControlGrid.Children[6]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)solutionViewerControlGrid.Children[6]).Margin = new Thickness(67 * UIscale, 25 * UIscale, 0, 0);
		}

		/// <summary>
		/// Initializes UI items in the fullscreen panel
		/// </summary>
		private void initFullscreenPanel()
		{
			fullscreenPanel.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			fullscreenPanel.Effect = new BlurEffect() { Radius = 0, KernelType = KernelType.Gaussian };

			initCreateMenu();
			initEditMenu();
			initSolutionMenu();
			initRunTheMazeMenu();
			initSettingsMenu();
			initAboutMenu();

			for (int i = 0; i < fullscreenPanelsGrid.Children.Count; i++)
			{
				Grid item = (Grid)fullscreenPanelsGrid.Children[i];
				item.Effect = new BlurEffect() { Radius = 0, KernelType = KernelType.Gaussian };
			}
			// Back button
			((Button)fullscreenPanel.Children[1]).Margin = new Thickness(panelMenuContentsMargin * UIscale, 0, 0, panelMenuContentsMargin / 2 * UIscale);
			((Button)fullscreenPanel.Children[1]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)fullscreenPanel.Children[1]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)fullscreenPanel.Children[1]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)fullscreenPanel.Children[1]).Content).Content).FontSize = 16 * UIscale;
			((TextBlock)((Label)((Button)fullscreenPanel.Children[1]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
		}

		/// <summary>
		/// Initializes UI animations properties
		/// </summary>
		private void initAnimations()
		{
			// Animating splash screen:
			// ---------------------------------------------------------------------------
			splashScreenFadeOutAnimation = new DoubleAnimation();
			Storyboard.SetTarget(splashScreenFadeOutAnimation, splashScreenGrid);
			Storyboard.SetTargetProperty(splashScreenFadeOutAnimation, new PropertyPath(OpacityProperty));
			splashScreenFadeOutAnimation.To = 0;
			splashScreenFadeOutAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(splashScreenFadeOutTime / UIanimationSpeedRatio));
			splashScreenFadeOutAnimation.BeginTime = TimeSpan.FromMilliseconds(splashScreenTime);
			splashScreenFadeOutAnimation.EasingFunction = splashScreenFadeOutEase;

			splashScreenFadeOutStoryboard = new Storyboard();
			splashScreenFadeOutStoryboard.Children.Add(splashScreenFadeOutAnimation);

			splashScreenFadeOutAnimation.Completed += (sender, e) =>
			{
				splashScreenGrid.Visibility = Visibility.Collapsed;
			};

			// Animating not solvable message:
			// ---------------------------------------------------------------------------
			notSolvableMessageFadeInAnimation = new DoubleAnimation();
			Storyboard.SetTarget(notSolvableMessageFadeInAnimation, notSolvableToastGrid);
			Storyboard.SetTargetProperty(notSolvableMessageFadeInAnimation, new PropertyPath(OpacityProperty));
			notSolvableMessageFadeInAnimation.To = 1;
			notSolvableMessageFadeInAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(notSolvableMessageFadeTime / UIanimationSpeedRatio));
			notSolvableMessageFadeInAnimation.EasingFunction = splashScreenFadeOutEase;

			notSolvableMessageFadeOutAnimation = new DoubleAnimation();
			Storyboard.SetTarget(notSolvableMessageFadeOutAnimation, notSolvableToastGrid);
			Storyboard.SetTargetProperty(notSolvableMessageFadeOutAnimation, new PropertyPath(OpacityProperty));
			notSolvableMessageFadeOutAnimation.To = 0;
			notSolvableMessageFadeOutAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(notSolvableMessageFadeTime / UIanimationSpeedRatio));
			notSolvableMessageFadeOutAnimation.BeginTime = TimeSpan.FromMilliseconds(notSolvableMessageTime);
			notSolvableMessageFadeOutAnimation.EasingFunction = splashScreenFadeOutEase;

			notSolvableMessageStoryboard = new Storyboard();
			notSolvableMessageStoryboard.Children.Add(notSolvableMessageFadeInAnimation);
			notSolvableMessageStoryboard.Children.Add(notSolvableMessageFadeOutAnimation);
			notSolvableMessageStoryboard.BeginTime = TimeSpan.FromMilliseconds(fullscreenPanelCollapsingTime / UIanimationSpeedRatio);

			notSolvableMessageStoryboard.Completed += (sender, e) =>
			{
				notSolvableToastGrid.Visibility = Visibility.Collapsed;
			};

			// Animating panel:
			// ---------------------------------------------------------------------------
			panelExpandingAnimation = new DoubleAnimation();
			Storyboard.SetTarget(panelExpandingAnimation, panel);
			Storyboard.SetTargetProperty(panelExpandingAnimation, new PropertyPath(WidthProperty));
			panelExpandingAnimation.To = maximizedPanelWidth * UIscale;
			panelExpandingAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelExpandingTime / UIanimationSpeedRatio));
			panelExpandingAnimation.EasingFunction = panelExpandingEase;

			panelCollapsingAnimation = new DoubleAnimation();
			Storyboard.SetTarget(panelCollapsingAnimation, panel);
			Storyboard.SetTargetProperty(panelCollapsingAnimation, new PropertyPath(WidthProperty));
			panelCollapsingAnimation.To = minimizedPanelWidth * UIscale;
			panelCollapsingAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelCollapsingTime / UIanimationSpeedRatio));
			panelCollapsingAnimation.EasingFunction = panelCollapsingEase;

			containerRightAnimation = new ThicknessAnimation();
			Storyboard.SetTarget(containerRightAnimation, container);
			Storyboard.SetTargetProperty(containerRightAnimation, new PropertyPath(MarginProperty));
			containerRightAnimation.To = new Thickness(maximizedPanelWidth * UIscale, menuBar.Height, 0, 0);
			containerRightAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelExpandingTime / UIanimationSpeedRatio));
			containerRightAnimation.EasingFunction = containerRightEase;

			containerLeftAnimation = new ThicknessAnimation();
			Storyboard.SetTarget(containerLeftAnimation, container);
			Storyboard.SetTargetProperty(containerLeftAnimation, new PropertyPath(MarginProperty));
			containerLeftAnimation.To = new Thickness(minimizedPanelWidth * UIscale, menuBar.Height, 0, 0);
			containerLeftAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelCollapsingTime / UIanimationSpeedRatio));
			containerLeftAnimation.EasingFunction = containerLeftEase;

			panelExpandingStoryboard = new Storyboard();
			panelExpandingStoryboard.Children.Add(panelExpandingAnimation);
			panelExpandingStoryboard.Children.Add(containerRightAnimation);

			panelCollapsingStoryboard = new Storyboard();
			panelCollapsingStoryboard.Children.Add(panelCollapsingAnimation);
			panelCollapsingStoryboard.Children.Add(containerLeftAnimation);

			panelExpandingStoryboard.Completed += (sender, e) =>
			{
				panelAnimationInProgress = false;
			};
			panelCollapsingStoryboard.Completed += (sender, e) =>
			{
				panelAnimationInProgress = false;
			};
			// ------------------------------------------------------------------------------------

			// Animating fullscreen panel
			// ------------------------------------------------------------------------------------
			fullscreenPanelRightAnimation = new ThicknessAnimation();
			Storyboard.SetTarget(fullscreenPanelRightAnimation, fullscreenPanel);
			Storyboard.SetTargetProperty(fullscreenPanelRightAnimation, new PropertyPath(MarginProperty));
			fullscreenPanelRightAnimation.To = new Thickness(0, 0, 0, 0);
			fullscreenPanelRightAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(fullscreenPanelExpandingTime / UIanimationSpeedRatio));
			fullscreenPanelRightAnimation.EasingFunction = fullscreenPanelExpandingEase;

			fullscreenPanelLeftAnimation = new ThicknessAnimation();
			Storyboard.SetTarget(fullscreenPanelLeftAnimation, fullscreenPanel);
			Storyboard.SetTargetProperty(fullscreenPanelLeftAnimation, new PropertyPath(MarginProperty));
			fullscreenPanelLeftAnimation.To = new Thickness(-fullscreenPanelTravelDistance * UIscale, 0, fullscreenPanelTravelDistance * UIscale, 0);
			fullscreenPanelLeftAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(fullscreenPanelCollapsingTime / UIanimationSpeedRatio));
			fullscreenPanelLeftAnimation.EasingFunction = fullscreenPanelCollapsingEase;

			fullscreenPanelFadeInAnimation = new DoubleAnimation();
			Storyboard.SetTarget(fullscreenPanelFadeInAnimation, fullscreenPanel);
			Storyboard.SetTargetProperty(fullscreenPanelFadeInAnimation, new PropertyPath(OpacityProperty));
			fullscreenPanelFadeInAnimation.To = 1;
			fullscreenPanelFadeInAnimation.FillBehavior = FillBehavior.Stop;
			fullscreenPanelFadeInAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(fullscreenPanelExpandingTime / UIanimationSpeedRatio));
			fullscreenPanelFadeInAnimation.EasingFunction = fullscreenPanelExpandingEase;

			fullscreenPanelFadeOutAnimation = new DoubleAnimation();
			Storyboard.SetTarget(fullscreenPanelFadeOutAnimation, fullscreenPanel);
			Storyboard.SetTargetProperty(fullscreenPanelFadeOutAnimation, new PropertyPath(OpacityProperty));
			fullscreenPanelFadeOutAnimation.To = 0;
			fullscreenPanelFadeOutAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(fullscreenPanelCollapsingTime / UIanimationSpeedRatio));
			fullscreenPanelFadeOutAnimation.EasingFunction = fullscreenPanelCollapsingEase;

			fullscreenPanelUnBlurAnimation = new DoubleAnimation();
			Storyboard.SetTarget(fullscreenPanelUnBlurAnimation, fullscreenPanel);
			Storyboard.SetTargetProperty(fullscreenPanelUnBlurAnimation, new PropertyPath("Effect.Radius"));
			fullscreenPanelUnBlurAnimation.To = 0;
			fullscreenPanelUnBlurAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(fullscreenPanelExpandingTime / UIanimationSpeedRatio));
			fullscreenPanelUnBlurAnimation.EasingFunction = fullscreenPanelExpandingEase;

			fullscreenPanelBlurAnimation = new DoubleAnimation();
			Storyboard.SetTarget(fullscreenPanelBlurAnimation, fullscreenPanel);
			Storyboard.SetTargetProperty(fullscreenPanelBlurAnimation, new PropertyPath("Effect.Radius"));
			fullscreenPanelBlurAnimation.To = animationBlurRadius * UIscale;
			fullscreenPanelBlurAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(fullscreenPanelCollapsingTime / UIanimationSpeedRatio));
			fullscreenPanelBlurAnimation.EasingFunction = fullscreenPanelCollapsingEase;

			fullscreenPanelExpandingStoryboard = new Storyboard();
			fullscreenPanelExpandingStoryboard.Children.Add(fullscreenPanelRightAnimation);
			fullscreenPanelExpandingStoryboard.Children.Add(fullscreenPanelFadeInAnimation);
			fullscreenPanelExpandingStoryboard.Children.Add(fullscreenPanelUnBlurAnimation);

			fullscreenPanelCollapsingStoryboard = new Storyboard();
			fullscreenPanelCollapsingStoryboard.Children.Add(fullscreenPanelLeftAnimation);
			fullscreenPanelCollapsingStoryboard.Children.Add(fullscreenPanelFadeOutAnimation);
			fullscreenPanelCollapsingStoryboard.Children.Add(fullscreenPanelBlurAnimation);

			fullscreenPanelExpandingStoryboard.Completed += (sender, e) =>
			{
				fullscreenPanel.Opacity = 1;
				coverLabel.Visibility = Visibility.Visible;
				coverLabel2.Visibility = Visibility.Visible;
				fullscreenPanelOpened = true;
				fullscreenPanelAnimationInProgress = false;
			};
			fullscreenPanelCollapsingStoryboard.Completed += (sender, e) =>
			{
				fullscreenPanel.Visibility = Visibility.Collapsed;
				fullscreenPanelsGrid.Children[(byte)lastActivePanel - 1].Visibility = Visibility.Collapsed;
				fullscreenPanelsGrid.Children[(byte)lastActivePanel - 1].Opacity = 0;
				fullscreenPanelOpened = false;
				fullscreenPanelAnimationInProgress = false;
			};
			// -------------------------------------------------------------------------------------------

			// Animating panel menu switching
			// -------------------------------------------------------------------------------------------
			panelMenuInAnimation = new ThicknessAnimation();
			Storyboard.SetTargetProperty(panelMenuInAnimation, new PropertyPath(MarginProperty));
			panelMenuInAnimation.From = new Thickness(-fullscreenPanelTravelDistance * UIscale, 0, 0, 0);
			panelMenuInAnimation.To = new Thickness(0, 0, 0, 0);
			panelMenuInAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelMenuSwitchingTime / UIanimationSpeedRatio));
			panelMenuInAnimation.EasingFunction = panelMenuSwitchingEase;

			panelMenuOutAnimation = new ThicknessAnimation();
			Storyboard.SetTargetProperty(panelMenuOutAnimation, new PropertyPath(MarginProperty));
			panelMenuOutAnimation.To = new Thickness(fullscreenPanelTravelDistance * UIscale, 0, 0, 0);
			panelMenuOutAnimation.FillBehavior = FillBehavior.Stop;
			panelMenuOutAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelMenuSwitchingTime / UIanimationSpeedRatio));
			panelMenuOutAnimation.EasingFunction = panelMenuSwitchingEase;

			panelMenuFadeInAnimation = new DoubleAnimation();
			Storyboard.SetTargetProperty(panelMenuFadeInAnimation, new PropertyPath(OpacityProperty));
			panelMenuFadeInAnimation.To = 1;
			panelMenuFadeInAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelMenuSwitchingTime / UIanimationSpeedRatio));
			panelMenuFadeInAnimation.EasingFunction = panelMenuSwitchingEase;

			panelMenuFadeOutAnimation = new DoubleAnimation();
			Storyboard.SetTargetProperty(panelMenuFadeOutAnimation, new PropertyPath(OpacityProperty));
			panelMenuFadeOutAnimation.To = 0;
			panelMenuFadeOutAnimation.FillBehavior = FillBehavior.Stop;
			panelMenuFadeOutAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelMenuSwitchingTime / UIanimationSpeedRatio));
			panelMenuFadeOutAnimation.EasingFunction = panelMenuSwitchingEase;

			panelMenuUnBlurAnimation = new DoubleAnimation();
			Storyboard.SetTargetProperty(panelMenuUnBlurAnimation, new PropertyPath("Effect.Radius"));
			panelMenuUnBlurAnimation.From = animationBlurRadius * UIscale;
			panelMenuUnBlurAnimation.To = 0;
			panelMenuUnBlurAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelMenuSwitchingTime / UIanimationSpeedRatio));
			panelMenuUnBlurAnimation.EasingFunction = panelMenuSwitchingEase;

			panelMenuBlurAnimation = new DoubleAnimation();
			Storyboard.SetTargetProperty(panelMenuBlurAnimation, new PropertyPath("Effect.Radius"));
			panelMenuBlurAnimation.To = animationBlurRadius * UIscale;
			panelMenuBlurAnimation.FillBehavior = FillBehavior.Stop;
			panelMenuBlurAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(panelMenuSwitchingTime / UIanimationSpeedRatio));
			panelMenuBlurAnimation.EasingFunction = panelMenuSwitchingEase;

			panelMenuSwitchingInStoryboard = new Storyboard();
			panelMenuSwitchingInStoryboard.Children.Add(panelMenuInAnimation);
			panelMenuSwitchingInStoryboard.Children.Add(panelMenuFadeInAnimation);
			panelMenuSwitchingInStoryboard.Children.Add(panelMenuUnBlurAnimation);
			panelMenuSwitchingInStoryboard.BeginTime = TimeSpan.FromMilliseconds(panelMenuSwitchingDelayTime / UIanimationSpeedRatio);

			panelMenuSwitchingOutStoryboard = new Storyboard();
			panelMenuSwitchingOutStoryboard.Children.Add(panelMenuOutAnimation);
			panelMenuSwitchingOutStoryboard.Children.Add(panelMenuFadeOutAnimation);
			panelMenuSwitchingOutStoryboard.Children.Add(panelMenuBlurAnimation);
			// -------------------------------------------------------------------------------------------
			panelMenuSwitchingInStoryboard.Completed += (sender, e) =>
			{
				panelSwitchingInProgress = false;
			};
			panelMenuSwitchingOutStoryboard.Completed += (sender, e) =>
			{
				//Restore leaving panel properties to normal conditions
				leavingPanel.Visibility = Visibility.Collapsed;
				leavingPanel.Opacity = 0;
				leavingPanel.Margin = new Thickness(0);
				((BlurEffect)leavingPanel.Effect).Radius = 0;
			};
		}
	}
}