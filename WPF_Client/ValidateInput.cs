using MazeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Client
{
	public partial class MainWindow : Window
	{
		private bool validateMazeWidth(string input)
		{
			input = input.Trim();
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			else if (!containsOnlyDigits(input))
			{
				return false;
			}
			else if (short.Parse(input) > Maze.MaxWidth)
			{
				return false;
			}
			else if (short.Parse(input) < Maze.MinWidth)
			{
				return false;
			}
			return true;
		}

		private bool validatePercent(string input)
		{
			input = input.Trim();
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			else if (!containsOnlyDigits(input))
			{
				return false;
			}
			else if (short.Parse(input) > 100)
			{
				return false;
			}
			else if (short.Parse(input) < 0)
			{
				return false;
			}
			return true;
		}

		private bool containsOnlyDigits(string s)
		{
			for (short i = 0; i < s.Length; i++)
			{
				if (!char.IsDigit(s[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
