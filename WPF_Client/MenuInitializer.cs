using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Client
{
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Initializes create menu panel items
		/// </summary>
		private void initCreateMenu()
		{
			// Initialize menu title
			TextBlock title = ((TextBlock)((StackPanel)(createMazePanel.Children[0])).Children[0]);
			title.FontSize = panelMenuTitleFontSize * UIscale;
			title.Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Contents margin
			((StackPanel)createMazePanel.Children[0]).Margin = new Thickness(panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * 1.5 * UIscale);
			// Menu items
			for (int i = 1; i < ((StackPanel)createMazePanel.Children[0]).Children.Count - 1; i++)
			{
				StackPanel cur = ((StackPanel)(((StackPanel)createMazePanel.Children[0]).Children[i]));
				// Margin
				if (i == 1)
				{
					cur.Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
				}
				else
				{
					cur.Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
				}

				// inner items
				if (i != ((StackPanel)createMazePanel.Children[0]).Children.Count - 1)
				{
					// Width and height textBlocks
					((TextBlock)(cur.Children[0])).FontSize = simpleTextFontSize * UIscale;
					((TextBlock)(cur.Children[0])).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
					((TextBlock)(cur.Children[0])).Width = 35 * UIscale;
					// Width and height textBoxes
					((TextBox)(cur.Children[1])).Width = 35 * UIscale;
					((TextBox)(cur.Children[1])).Height = 18 * UIscale;
					((TextBox)(cur.Children[1])).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
					((TextBox)(cur.Children[1])).FontSize = simpleTextFontSize * UIscale;
					((TextBox)(cur.Children[1])).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
					((TextBox)(cur.Children[1])).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
					((TextBox)(cur.Children[1])).SelectionBrush = new SolidColorBrush(colorScheme.TextSelectionColor);
					((TextBox)(cur.Children[1])).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
					((TextBox)(cur.Children[1])).BorderThickness = new Thickness(textBoxBorderThickness * UIscale);
					// Invalid input message
					((TextBlock)(cur.Children[2])).FontSize = simpleTextFontSize * UIscale;
					((TextBlock)(cur.Children[2])).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
					((TextBlock)(cur.Children[2])).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
				}
			}
			// Create maze button
			Button create = (Button)((((StackPanel)createMazePanel.Children[0]).Children[((StackPanel)createMazePanel.Children[0]).Children.Count - 1]));
			create.Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			create.BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			create.BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)(create.Content)).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)(create.Content)).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)(create.Content)).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
		}

		/// <summary>
		/// Initializes create edit menu panel
		/// </summary>
		private void initEditMenu()
		{
			// Menu title
			((TextBlock)((StackPanel)(editMazePanel.Children[0])).Children[0]).FontSize = panelMenuTitleFontSize * UIscale;
			((TextBlock)((StackPanel)(editMazePanel.Children[0])).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Contents margin
			((StackPanel)editMazePanel.Children[0]).Margin = new Thickness(panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * 1.5 * UIscale);
			// Randomize maze section
			randomizeMazeStackPanel.Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// - Randomize maze section title
			((TextBlock)randomizeMazeStackPanel.Children[0]).FontSize = headerFontSize * UIscale;
			((TextBlock)randomizeMazeStackPanel.Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// - Randomize by percent StackPanel
			((StackPanel)randomizeMazeStackPanel.Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// -- Randomize by percent title
			((TextBlock)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// -- percent input horizental StackPanel
			((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// --- percent input title TextBlock
			((TextBlock)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// --- percent input TextBox
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).Width = 35 * UIscale;
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).Height = 20 * UIscale;
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).FontSize = simpleTextFontSize * UIscale;
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).SelectionBrush = new SolidColorBrush(colorScheme.TextSelectionColor);
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBox)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[1]).BorderThickness = new Thickness(textBoxBorderThickness * UIscale);
			// --- invalid percent TextBlock
			((TextBlock)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[2]).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
			((TextBlock)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[2]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)((StackPanel)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[1]).Children[2]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// -- Randomize by percent Button
			((Button)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[2]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			((Button)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[2]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[2]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[2]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[2]).Content).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)((Button)((StackPanel)randomizeMazeStackPanel.Children[1]).Children[2]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// - Randomize using depth-first search algorithm StackPanel
			((StackPanel)randomizeMazeStackPanel.Children[2]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * 2 * UIscale, 0, 0);
			// -- Randomize using depth-first search algorithm title
			((TextBlock)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// -- Randomize using depth-first search algorithm Button
			((Button)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			((Button)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[1]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[1]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[1]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[1]).Content).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)((Button)((StackPanel)randomizeMazeStackPanel.Children[2]).Children[1]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Clear maze Button
			((Button)((StackPanel)(editMazePanel.Children[0])).Children[2]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			((Button)((StackPanel)(editMazePanel.Children[0])).Children[2]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)((StackPanel)(editMazePanel.Children[0])).Children[2]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)((StackPanel)(editMazePanel.Children[0])).Children[2]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)((StackPanel)(editMazePanel.Children[0])).Children[2]).Content).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)((Button)((StackPanel)(editMazePanel.Children[0])).Children[2]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Dispose maze Button
			((Button)((StackPanel)(editMazePanel.Children[0])).Children[3]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			((Button)((StackPanel)(editMazePanel.Children[0])).Children[3]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)((StackPanel)(editMazePanel.Children[0])).Children[3]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)((StackPanel)(editMazePanel.Children[0])).Children[3]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)((StackPanel)(editMazePanel.Children[0])).Children[3]).Content).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)((Button)((StackPanel)(editMazePanel.Children[0])).Children[3]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
		}

		/// <summary>
		/// Initializes solution menu panel items
		/// </summary>
		private void initSolutionMenu()
		{
			// Menu title
			((TextBlock)((StackPanel)(solutionPanel.Children[0])).Children[0]).FontSize = panelMenuTitleFontSize * UIscale;
			((TextBlock)((StackPanel)(solutionPanel.Children[0])).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Contents margin
			((StackPanel)solutionPanel.Children[0]).Margin = new Thickness(panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * 1.5 * UIscale);
			// Maze solution section
			mazeSolutionStackPanel.Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// Solve using mouse algorithm title
			((TextBlock)mazeSolutionStackPanel.Children[0]).FontSize = headerFontSize * UIscale;
			((TextBlock)mazeSolutionStackPanel.Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// - Solve using mouse algorithm contents StackPanel
			((StackPanel)mazeSolutionStackPanel.Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// -- Use random directions CheckBox
			useRandomDirectionCheckBox.FontSize = simpleTextFontSize * UIscale;
			useRandomDirectionCheckBox.Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			useRandomDirectionCheckBox.Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			useRandomDirectionCheckBox.BorderThickness = new Thickness(1 * UIscale);
			useRandomDirectionCheckBox.BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			// -- Mouse solving alg display type StackPanel
			((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Margin = new Thickness(0, panelMenuItemsMargin * UIscale, 0, 0);
			((TextBlock)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[0]).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[0]).BorderThickness = new Thickness(1 * UIscale);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[0]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[1]).Margin = new Thickness(0, panelMenuItemsMargin / 2 * UIscale, 0, 0);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[1]).FontSize = simpleTextFontSize * UIscale;
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[1]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[1]).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[1]).BorderThickness = new Thickness(1 * UIscale);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[1]).Children[1]).Children[1]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			// -- Mosue alg solve Button
			((Button)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[2]).Margin = new Thickness(0, panelMenuItemsMargin * UIscale, 0, 0);
			((Button)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[2]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[2]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[2]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[2]).Content).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)((Button)((StackPanel)mazeSolutionStackPanel.Children[1]).Children[2]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Solve using fluid simulation algorithm title
			((TextBlock)mazeSolutionStackPanel.Children[2]).FontSize = headerFontSize * UIscale;
			((TextBlock)mazeSolutionStackPanel.Children[2]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// - Solve using fluid simulation algorithm contents StackPanel
			((StackPanel)mazeSolutionStackPanel.Children[3]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// -- Fluid simulation solving alg display type StackPanel
			((TextBlock)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[0]).FontSize = simpleTextFontSize * UIscale;
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[0]).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[0]).BorderThickness = new Thickness(1 * UIscale);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[0]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[1]).Margin = new Thickness(0, panelMenuItemsMargin / 2 * UIscale, 0, 0);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[1]).FontSize = simpleTextFontSize * UIscale;
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[1]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[1]).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[1]).BorderThickness = new Thickness(1 * UIscale);
			((RadioButton)((StackPanel)((StackPanel)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[0]).Children[1]).Children[1]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			// -- Fluid simulation alg solve Button
			((Button)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[1]).Margin = new Thickness(0, panelMenuItemsMargin * UIscale, 0, 0);
			((Button)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[1]).BorderThickness = new Thickness(0, 0, 0, buttonBorderThickness * UIscale);
			((Button)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[1]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((Label)((Button)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[1]).Content).Padding = new Thickness(buttonContentPadding * UIscale);
			((TextBlock)((Label)((Button)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[1]).Content).Content).FontSize = buttonsFontSize * UIscale;
			((TextBlock)((Label)((Button)((StackPanel)mazeSolutionStackPanel.Children[3]).Children[1]).Content).Content).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
		}

		/// <summary>
		/// Initializes run the maze menu panel items
		/// </summary>
		private void initRunTheMazeMenu()
		{
			// Menu title
			((TextBlock)((StackPanel)(runTheMazePanel.Children[0])).Children[0]).FontSize = panelMenuTitleFontSize * UIscale;
			((TextBlock)((StackPanel)(runTheMazePanel.Children[0])).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Contents margin
			((StackPanel)runTheMazePanel.Children[0]).Margin = new Thickness(panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * 1.5 * UIscale);
		}

		/// <summary>
		/// Initializes settings menu panel items
		/// </summary>
		private void initSettingsMenu()
		{
			// Menu title
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[0]).FontSize = panelMenuTitleFontSize * UIscale;
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Contents margin
			((StackPanel)settingsPanel.Children[0]).Margin = new Thickness(panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * 1.5 * UIscale);
			// Interface scale title
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[1]).FontSize = headerFontSize * UIscale;
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[1]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// Scales StackPanel
			StackPanel sp = ((StackPanel)((StackPanel)(settingsPanel.Children[0])).Children[2]);
			sp.Margin = new Thickness(panelMenuItemsMargin * spaceMarginRatio * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			((Button)sp.Children[0]).FontSize = simpleTextFontSize * UIscale;
			((Button)sp.Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((Button)sp.Children[0]).Margin = new Thickness(0);
			((Button)sp.Children[1]).FontSize = simpleTextFontSize * UIscale;
			((Button)sp.Children[1]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((Button)sp.Children[1]).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
			((Button)sp.Children[2]).FontSize = simpleTextFontSize * UIscale;
			((Button)sp.Children[2]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((Button)sp.Children[2]).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
			((Button)sp.Children[3]).FontSize = simpleTextFontSize * UIscale;
			((Button)sp.Children[3]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((Button)sp.Children[3]).Margin = new Thickness(panelMenuItemsMargin * UIscale, 0, 0, 0);
			// Maze grids title
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[3]).FontSize = headerFontSize * UIscale;
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[3]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[3]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// Show maze grids CheckBox
			((CheckBox)((StackPanel)(settingsPanel.Children[0])).Children[4]).FontSize = simpleTextFontSize * UIscale;
			((CheckBox)((StackPanel)(settingsPanel.Children[0])).Children[4]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((CheckBox)((StackPanel)(settingsPanel.Children[0])).Children[4]).Background = new SolidColorBrush(colorScheme.FullscreenPanelColor);
			((CheckBox)((StackPanel)(settingsPanel.Children[0])).Children[4]).BorderThickness = new Thickness(1 * UIscale);
			((CheckBox)((StackPanel)(settingsPanel.Children[0])).Children[4]).BorderBrush = new SolidColorBrush(colorScheme.LightTextColor);
			((CheckBox)((StackPanel)(settingsPanel.Children[0])).Children[4]).Margin = new Thickness(panelMenuItemsMargin * spaceMarginRatio * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// Color scheme title
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[5]).FontSize = headerFontSize * UIscale;
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[5]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)((StackPanel)(settingsPanel.Children[0])).Children[5]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// Color schemes Grid
			((Grid)((StackPanel)(settingsPanel.Children[0])).Children[6]).Margin = new Thickness(panelMenuItemsMargin * spaceMarginRatio * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			foreach (Button item in ((Grid)((StackPanel)(settingsPanel.Children[0])).Children[6]).Children)
			{
				item.Width = 30 * UIscale;
				item.Height = 30 * UIscale;
			}
			blueColorSchemeButton.Background = new SolidColorBrush(ColorSchemes.Blue.PanelColor);
			greenColorSchemeButton.Background = new SolidColorBrush(ColorSchemes.Green.PanelColor);
			redColorSchemeButton.Background = new SolidColorBrush(ColorSchemes.Red.PanelColor);
			yellowColorSchemeButton.Background = new SolidColorBrush(ColorSchemes.Yellow.PanelColor);
			pinkColorSchemeButton.Background = new SolidColorBrush(ColorSchemes.Pink.PanelColor);
			grayColorSchemeButton.Background = new SolidColorBrush(ColorSchemes.Gray.PanelColor);
			greenColorSchemeButton.Margin = new Thickness(greenColorSchemeButton.Width + panelMenuItemsMargin * UIscale / 2, 0, 0, 0);
			redColorSchemeButton.Margin = new Thickness(2 * greenColorSchemeButton.Width + 2 * panelMenuItemsMargin * UIscale / 2, 0, 0, 0);
			yellowColorSchemeButton.Margin = new Thickness(0, greenColorSchemeButton.Width + panelMenuItemsMargin * UIscale / 2, 0, 0);
			pinkColorSchemeButton.Margin = new Thickness(greenColorSchemeButton.Width + panelMenuItemsMargin * UIscale / 2, greenColorSchemeButton.Width + panelMenuItemsMargin * UIscale / 2, 0, 0);
			grayColorSchemeButton.Margin = new Thickness(2 * greenColorSchemeButton.Width + 2 * panelMenuItemsMargin * UIscale / 2, greenColorSchemeButton.Width + panelMenuItemsMargin * UIscale / 2, 0, 0);
		}

		/// <summary>
		/// Initializes about menu panel items
		/// </summary>
		private void initAboutMenu()
		{
			// Menu title
			((TextBlock)((StackPanel)(aboutPanel.Children[0])).Children[0]).FontSize = panelMenuTitleFontSize * UIscale;
			((TextBlock)((StackPanel)(aboutPanel.Children[0])).Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			// Contents margin
			((StackPanel)aboutPanel.Children[0]).Margin = new Thickness(panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * UIscale, panelMenuContentsMargin * 1.5 * UIscale);
			// How to use title
			((TextBlock)aboutContentStackPanel.Children[0]).FontSize = 18 * UIscale;
			((TextBlock)aboutContentStackPanel.Children[0]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)aboutContentStackPanel.Children[0]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// How to use text
			((TextBlock)aboutContentStackPanel.Children[1]).Width = 260 * UIscale;
			((TextBlock)aboutContentStackPanel.Children[1]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)aboutContentStackPanel.Children[1]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)aboutContentStackPanel.Children[1]).Margin = new Thickness(panelMenuItemsMargin * spaceMarginRatio * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// About developer title
			((TextBlock)aboutContentStackPanel.Children[2]).FontSize = 18 * UIscale;
			((TextBlock)aboutContentStackPanel.Children[2]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)aboutContentStackPanel.Children[2]).Margin = new Thickness(panelMenuItemsMargin * UIscale, panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0);
			// About developer text
			((TextBlock)aboutContentStackPanel.Children[3]).Width = 260 * UIscale;
			((TextBlock)aboutContentStackPanel.Children[3]).FontSize = simpleTextFontSize * UIscale;
			((TextBlock)aboutContentStackPanel.Children[3]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)aboutContentStackPanel.Children[3]).Margin = new Thickness(panelMenuItemsMargin * spaceMarginRatio * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// EminenetDevs logo
			((Image)aboutContentStackPanel.Children[4]).Width = 80 * UIscale;
			((Image)aboutContentStackPanel.Children[4]).Margin = new Thickness((panelMenuItemsMargin * spaceMarginRatio + 20) * UIscale, panelMenuItemsMargin * UIscale, 0, 0);
			// Copyright
			((TextBlock)aboutContentStackPanel.Children[5]).FontSize = 11 * UIscale;
			((TextBlock)aboutContentStackPanel.Children[5]).Foreground = new SolidColorBrush(colorScheme.LightTextColor);
			((TextBlock)aboutContentStackPanel.Children[5]).Margin = new Thickness(panelMenuItemsMargin * spaceMarginRatio * UIscale, 0, 0, 0);
		}
	}
}