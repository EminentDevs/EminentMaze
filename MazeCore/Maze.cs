using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MazeCore
{
	public class Maze
	{
		public ElementPosition StartPos { get; private set; }

		public ElementPosition FinishPos { get; private set; }

		public byte Width { get; private set; }

		public byte Height { get; private set; }

		public static byte MaxWidth { get; } = 100;

		public static byte MinWidth { get; } = 5;

		public ushort BlocksCount { get; private set; }

		public Element[,] Map { get; private set; }

		// Stores solution for the current maze
		public ElementPosition[] SolutionPath { get; private set; }

		// Stores solution procedure for the mouse solving algorithm
		public List<ElementPosition> MouseAlgSolutionProcedure { get; private set; }

		// Stores solution procedure for the fluid algorithm
		public List<ElementPosition[]> FluidAlgSolutionProcedure { get; private set; }

		private Random rnd = new Random();

		private ushort solutionLength;

		/// <summary>
		/// Maze constructor
		/// </summary>
		/// <param name="height"></param>
		/// <param name="width"></param>
		public Maze(byte height, byte width)
		{
			Height = ((height >= MinWidth && height <= MaxWidth) ? height : ((height < MinWidth) ? MinWidth : MaxWidth));
			Width = ((width >= MinWidth && width <= MaxWidth) ? width : ((width < MinWidth) ? MinWidth : MaxWidth));
			createEmptyMazeMap();
		}

		/// <summary>
		/// Initialize maze with empty elements
		/// </summary>
		private void createEmptyMazeMap()
		{
			Map = new Element[Height, Width];
			for (byte i = 0; i < Height; i++)
			{
				for (byte j = 0; j < Width; j++)
				{
					Map[i, j] = new Element(i, j);
				}
			}
		}

		/// <summary>
		/// Gets an element by its position
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public Element getElement(ElementPosition position)
		{
			return Map[position.Row, position.Col];
		}

		/// <summary>
		/// Gets an element by its row & column numbers
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public Element getElement(byte row, byte col)
		{
			return Map[row, col];
		}

		// Add a block to the maze and report success
		public bool AddBlock(ElementPosition position)
		{
			if (getElement(position).Status == ElementStatus.EMPTY)
			{
				getElement(position).Status = ElementStatus.BLOCK;
				BlocksCount++;
				if (position == StartPos)
				{
					StartPos = null;
				}
				if (position == FinishPos)
				{
					FinishPos = null;
				}
				return true;
			}
			return false;
		}

		// Remove a block from the maze and report success
		public bool RemoveBlock(ElementPosition position)
		{
			if (getElement(position).Status == ElementStatus.BLOCK)
			{
				getElement(position).Status = ElementStatus.EMPTY;
				BlocksCount--;
				return true;
			}
			return false;
		}

		// Set start position in the maze and report success
		public bool SetStartPosition(ElementPosition startPos)
		{
			if (StartPos != startPos)
			{
				if (getElement(startPos).Status == ElementStatus.EMPTY)
				{
					StartPos = startPos;
				}
				else
				{
					return false;
				}
				if (StartPos == FinishPos)
				{
					FinishPos = null;
				}
				return true;
			}
			else
			{
				StartPos = null;
				return true;
			}
		}

		// Set finish position and report success
		public bool SetFinishPosition(ElementPosition finishPos)
		{
			if (FinishPos != finishPos)
			{
				if (getElement(finishPos).Status == ElementStatus.EMPTY)
				{
					FinishPos = finishPos;
				}
				else
				{
					return false;
				}
				if (FinishPos == StartPos)
				{
					StartPos = null;
				}
				return true;
			}
			else
			{
				FinishPos = null;
				return true;
			}
		}

		public void SetStartAndFinishPositions(ElementPosition startPos, ElementPosition finishPos)
		{
			SetStartPosition(startPos);
			SetFinishPosition(finishPos);
		}

		// Sets Start and Finish automatically
		public void SetStartAndFinishPositions()
		{
			SetStartPosition(randomPosition());
			SetFinishPosition(randomPositionNotInTouchWith(StartPos));
		}

		/// <summary>
		/// Randomizes the maze using Depth-first search algorithm
		/// </summary>
		public void RandomizeBlocksUsingDepthFirstSearchAlg()
		{
			StartPos = null;
			FinishPos = null;

			// Block entire map
			foreach (Element element in Map)
			{
				element.Status = ElementStatus.BLOCK;
			}
			BlocksCount = (ushort)(Width * Height);

			Stack<ElementPosition> pathStack = new Stack<ElementPosition>();
			ElementPosition start = new ElementPosition((byte)(rnd.Next(0, Height / 2) * 2), (byte)(rnd.Next(0, Width / 2) * 2));
			getElement(start).Status = ElementStatus.EMPTY;
			pathStack.Push(start);

			while (pathStack.Count != 0)
			{
				if (!hasSuitableNeighborForPath(pathStack.Peek()))
				{
					pathStack.Pop();
				}
				else
				{
					byte rand = (byte)rnd.Next(0, suitableNeighborsForPathCount(pathStack.Peek()));
					byte index = 0;
					// Check right
					if (isRightNeghborSuitableForPath(pathStack.Peek()))
					{
						if (rand == index)
						{
							getElement(pathStack.Peek().Row, (byte)(pathStack.Peek().Col + 1)).Status = ElementStatus.EMPTY;
							getElement(pathStack.Peek().Row, (byte)(pathStack.Peek().Col + 2)).Status = ElementStatus.EMPTY;
							BlocksCount -= 2;
							pathStack.Push(getElement(pathStack.Peek().Row, (byte)(pathStack.Peek().Col + 2)).Position);
							continue;
						}
						else
						{
							index++;
						}
					}
					// Check bottom
					if (isBottomNeghborSuitableForPath(pathStack.Peek()))
					{
						if (rand == index)
						{
							getElement((byte)(pathStack.Peek().Row + 1), pathStack.Peek().Col).Status = ElementStatus.EMPTY;
							getElement((byte)(pathStack.Peek().Row + 2), pathStack.Peek().Col).Status = ElementStatus.EMPTY;
							BlocksCount -= 2;
							pathStack.Push(getElement((byte)(pathStack.Peek().Row + 2), pathStack.Peek().Col).Position);
							continue;
						}
						else
						{
							index++;
						}
					}
					// Check left
					if (isLeftNeghborSuitableForPath(pathStack.Peek()))
					{
						if (rand == index)
						{
							getElement(pathStack.Peek().Row, (byte)(pathStack.Peek().Col - 1)).Status = ElementStatus.EMPTY;
							getElement(pathStack.Peek().Row, (byte)(pathStack.Peek().Col - 2)).Status = ElementStatus.EMPTY;
							BlocksCount -= 2;
							pathStack.Push(getElement(pathStack.Peek().Row, (byte)(pathStack.Peek().Col - 2)).Position);
							continue;
						}
						else
						{
							index++;
						}
					}
					// Check top
					if (isTopNeghborSuitableForPath(pathStack.Peek()))
					{
						if (rand == index)
						{
							getElement((byte)(pathStack.Peek().Row - 1), pathStack.Peek().Col).Status = ElementStatus.EMPTY;
							getElement((byte)(pathStack.Peek().Row - 2), pathStack.Peek().Col).Status = ElementStatus.EMPTY;
							BlocksCount -= 2;
							pathStack.Push(getElement((byte)(pathStack.Peek().Row - 2), pathStack.Peek().Col).Position);
							continue;
						}
					}
				}
			}
		}

		/// <summary>
		/// Randomizes the maze by giving a percent
		/// </summary>
		/// <param name="percent"></param>
		public void RandomizeMazeBlocksByPercent(byte percent)
		{
			clearMap();
			for (ushort i = 0; i < blocksCountByPercent(ref percent); i++)
			{
				getElement(randomEmptyPosition()).Status = ElementStatus.BLOCK;
				BlocksCount++;
			}
		}

		/// <summary>
		/// Solves the maze using fluid simulation algorithm and reports success
		/// </summary>
		/// <returns></returns>
		public bool SolveUsingFluidSimulationAlg()
		{
			bool solved = false;
			SolutionPath = null;
			if (FluidAlgSolutionProcedure == null)
			{
				FluidAlgSolutionProcedure = new List<ElementPosition[]>();
			}
			FluidAlgSolutionProcedure.Clear();

			// Solve process
			List<ElementPosition> edgeElementsPositions = new List<ElementPosition>();
			getElement(StartPos).Status = ElementStatus.MARKED;
			edgeElementsPositions.Add(StartPos);
			FluidAlgSolutionProcedure.Add(new ElementPosition[] { StartPos });
			solutionLength = 1;

			while (getElement(FinishPos).Status != ElementStatus.MARKED && edgeElementsPositions.Count != 0)
			{
				solutionLength++;
				// Add unmarked neighbors to new edge list
				List<ElementPosition> newEdgeElements = new List<ElementPosition>();
				foreach (ElementPosition elementPos in edgeElementsPositions)
				{
					markEmptyNeighborsAndAddElementsWithEmptyNeighborsToList(elementPos, ref newEdgeElements);
				}
				// Update edge elements list
				edgeElementsPositions.Clear();
				edgeElementsPositions = newEdgeElements;
				newEdgeElements = null;
				// Construct solution procedure
				ElementPosition[] temp = new ElementPosition[edgeElementsPositions.Count];
				for (short i = 0; i < edgeElementsPositions.Count; i++)
				{
					temp[i] = edgeElementsPositions[i];
				}
				FluidAlgSolutionProcedure.Add(temp);
			}

			edgeElementsPositions.Clear();
			edgeElementsPositions = null;

			if (getElement(FinishPos).Status == ElementStatus.MARKED)
			{
				solved = true;
				exportSolution();
			}
			// Clear elements source positions
			clearSourcePositions();
			// Restore elements status
			restoreMazeElementsStatus();

			return solved;
		}

		/// <summary>
		/// Solves the maze using mouse solving algorithm and reports success
		/// </summary>
		/// <param name="moveRandomly"></param>
		/// <returns></returns>
		public bool SolveUsingMouseAlg(bool moveRandomly)
		{
			bool solved = false;
			SolutionPath = null;
			if (MouseAlgSolutionProcedure == null)
			{
				MouseAlgSolutionProcedure = new List<ElementPosition>();
			}
			MouseAlgSolutionProcedure.Clear();

			// Stack solve process
			Stack<ElementPosition> solutionStack = new Stack<ElementPosition>();
			getElement(StartPos).Status = ElementStatus.MARKED;
			solutionStack.Push(StartPos);
			MouseAlgSolutionProcedure.Add(StartPos);

			// Choose direction by random
			if (moveRandomly)
			{
				while (getElement(FinishPos).Status != ElementStatus.MARKED && solutionStack.Count != 0)
				{
					// Construct solution procedure
					if (solutionStack.Peek() != MouseAlgSolutionProcedure[MouseAlgSolutionProcedure.Count - 1])
					{
						MouseAlgSolutionProcedure.Add(solutionStack.Peek());
					}
					// Check for neighbors
					if (!hasEmptyNeighbor(solutionStack.Peek()))
					{
						solutionStack.Pop();
					}
					else
					{
						byte rand = (byte)rnd.Next(0, emptyNeighborsCount(solutionStack.Peek()));
						byte index = 0;
						if (isRightNeghborEmpty(solutionStack.Peek()))
						{
							if (rand == index)
							{
								Element t = getElement(solutionStack.Peek().Row, (byte)(solutionStack.Peek().Col + 1));
								t.Status = ElementStatus.MARKED;
								solutionStack.Push(t.Position);
								continue;
							}
							else
							{
								index++;
							}
						}
						if (isBottomNeghborEmpty(solutionStack.Peek()))
						{
							if (rand == index)
							{
								Element t = getElement((byte)(solutionStack.Peek().Row + 1), solutionStack.Peek().Col);
								t.Status = ElementStatus.MARKED;
								solutionStack.Push(t.Position);
								continue;
							}
							else
							{
								index++;
							}
						}
						if (isLeftNeghborEmpty(solutionStack.Peek()))
						{
							if (rand == index)
							{
								Element t = getElement(solutionStack.Peek().Row, (byte)(solutionStack.Peek().Col - 1));
								t.Status = ElementStatus.MARKED;
								solutionStack.Push(t.Position);
								continue;
							}
							else
							{
								index++;
							}
						}
						if (isTopNeghborEmpty(solutionStack.Peek()))
						{
							Element t = getElement((byte)(solutionStack.Peek().Row - 1), solutionStack.Peek().Col);
							t.Status = ElementStatus.MARKED;
							solutionStack.Push(t.Position);
						}
					}
				}
			}
			// Choose directions in order of "right, bottom, left, top"
			else
			{
				while (getElement(FinishPos).Status != ElementStatus.MARKED && solutionStack.Count != 0)
				{
					// Construct solution procedure
					if (solutionStack.Peek() != MouseAlgSolutionProcedure[MouseAlgSolutionProcedure.Count - 1])
					{
						MouseAlgSolutionProcedure.Add(solutionStack.Peek());
					}
					// Check for neighbors
					if (isRightNeghborEmpty(solutionStack.Peek()))
					{
						Element t = getElement(solutionStack.Peek().Row, (byte)(solutionStack.Peek().Col + 1));
						t.Status = ElementStatus.MARKED;
						solutionStack.Push(t.Position);
					}
					else if (isBottomNeghborEmpty(solutionStack.Peek()))
					{
						Element t = getElement((byte)(solutionStack.Peek().Row + 1), solutionStack.Peek().Col);
						t.Status = ElementStatus.MARKED;
						solutionStack.Push(t.Position);
					}
					else if (isLeftNeghborEmpty(solutionStack.Peek()))
					{
						Element t = getElement(solutionStack.Peek().Row, (byte)(solutionStack.Peek().Col - 1));
						t.Status = ElementStatus.MARKED;
						solutionStack.Push(t.Position);
					}
					else if (isTopNeghborEmpty(solutionStack.Peek()))
					{
						Element t = getElement((byte)(solutionStack.Peek().Row - 1), solutionStack.Peek().Col);
						t.Status = ElementStatus.MARKED;
						solutionStack.Push(t.Position);
					}
					else
					{
						solutionStack.Pop();
					}
				}
			}

			if (getElement(FinishPos).Status == ElementStatus.MARKED)
			{
				solved = true;
				MouseAlgSolutionProcedure.Add(FinishPos);
				SolutionPath = new ElementPosition[((ushort)solutionStack.Count)];
				SolutionPath = solutionStack.ToArray();
				Array.Reverse(SolutionPath);
			}
			// Restore elements status
			restoreMazeElementsStatus();

			return solved;
		}

		/// <summary>
		/// Exports solution after using fluid solving algorithm
		/// </summary>
		private void exportSolution()
		{
			SolutionPath = new ElementPosition[solutionLength];
			SolutionPath[solutionLength - 1] = FinishPos;
			for (short i = (short)(solutionLength - 2); i >= 0; i--)
			{
				SolutionPath[i] = Map[SolutionPath[i + 1].Row, SolutionPath[i + 1].Col].SourcePos;
			}
		}


		private void clearSourcePositions()
		{
			for (byte i = 0; i < Height; i++)
			{
				for (byte j = 0; j < Width; j++)
				{
					Map[i, j].SourcePos = null;
				}
			}
		}

		/// <summary>
		/// Removes marks in the maze and restores the maze to the initial state
		/// </summary>
		private void restoreMazeElementsStatus()
		{
			for (byte i = 0; i < Height; i++)
			{
				for (byte j = 0; j < Width; j++)
				{
					if (Map[i, j].Status == ElementStatus.MARKED)
					{
						Map[i, j].Status = ElementStatus.EMPTY;
					}
				}
			}
		}

		/// <summary>
		/// Returns a random position in the maze
		/// </summary>
		/// <returns></returns>
		private ElementPosition randomPosition()
		{
			ElementPosition outputPosition = new ElementPosition();
			outputPosition.Col = (byte)(rnd.Next(0, Width));
			outputPosition.Row = (byte)(rnd.Next(0, Height));
			return outputPosition;
		}

		/// <summary>
		/// Returns a random empty position in the maze
		/// </summary>
		/// <returns></returns>
		private ElementPosition randomEmptyPosition()
		{
			ElementPosition outputPosition = new ElementPosition();
			ushort randomPositionIndex = (ushort)(rnd.Next(1, Width * Height - BlocksCount + 1 - ((StartPos != null) ? 1 : 0) - ((FinishPos != null) ? 1 : 0)));
			bool flag = false;
			ushort stepCounter = 0;
			for (byte i = 0; i < Width; i++)
			{
				outputPosition.Col = i;
				for (byte j = 0; j < Height; j++)
				{
					outputPosition.Row = j;
					if (getElement(outputPosition).Status == ElementStatus.EMPTY)
					{
						if (outputPosition != StartPos && outputPosition != FinishPos)
						{
							if (++stepCounter == randomPositionIndex)
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			return outputPosition;
		}

		/// <summary>
		/// Returns a random position not in touch with another element
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private ElementPosition randomPositionNotInTouchWith(ElementPosition pos)
		{
			ElementPosition outputPosition = new ElementPosition();
			ushort randomPositionIndex = (ushort)(rnd.Next(1, Width * Height - neighborsCount(ref pos)));
			bool flag = false;
			ushort stepCounter = 0;
			for (byte i = 0; i < Height; i++)
			{
				outputPosition.Row = i;
				for (byte j = 0; j < Width; j++)
				{
					outputPosition.Col = j;
					if (!outputPosition.IsNextTo(pos))
					{
						stepCounter++;
						if (stepCounter == randomPositionIndex)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			return outputPosition;
		}

		/// <summary>
		/// Returns neighbors count of an element.(wether neighbors are block or not)
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private byte neighborsCount(ref ElementPosition pos)
		{
			if (
				pos.Col - 1 >= 0 &&
				pos.Row - 1 >= 0 &&
				pos.Col + 1 < Width &&
				pos.Row + 1 < Height
				)
			{
				return 8;
			}
			else if (
				(pos.Col - 1 >= 0 && pos.Row - 1 >= 0) &&
				(pos.Col + 1 >= Width || pos.Row + 1 >= Height)
				)
			{
				if (
					pos.Col + 1 >= Width &&
					pos.Row + 1 >= Height
					)
				{
					return 3;
				}
				else
				{
					return 5;
				}
			}
			else
			{
				if (
					pos.Col - 1 < 0 &&
					pos.Row - 1 < 0
					)
				{
					return 3;
				}
				else
				{
					return 5;
				}
			}
		}

		/// <summary>
		/// Returns empty neighbors count of an element
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private byte emptyNeighborsCount(ElementPosition pos)
		{
			byte count = 0;
			if (isRightNeghborEmpty(pos))
			{
				count++;
			}
			if (isBottomNeghborEmpty(pos))
			{
				count++;
			}
			if (isLeftNeghborEmpty(pos))
			{
				count++;
			}
			if (isTopNeghborEmpty(pos))
			{
				count++;
			}
			return count;
		}

		/// <summary>
		/// Check if right neighbor is empty
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isRightNeghborEmpty(ElementPosition position)
		{
			if (position.Col + 1 < Width)
			{
				return getElement(position.Row, (byte)(position.Col + 1)).Status == ElementStatus.EMPTY;
			}
			return false;
		}

		/// <summary>
		/// Check if bottom neighbor is empty
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isBottomNeghborEmpty(ElementPosition position)
		{
			if (position.Row + 1 < Height)
			{
				return getElement((byte)(position.Row + 1), position.Col).Status == ElementStatus.EMPTY;
			}
			return false;
		}

		/// <summary>
		/// Check if left neighbor is empty
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isLeftNeghborEmpty(ElementPosition position)
		{
			if (position.Col - 1 >= 0)
			{
				return getElement(position.Row, (byte)(position.Col - 1)).Status == ElementStatus.EMPTY;
			}
			return false;
		}

		/// <summary>
		/// Check if top neighbor is empty
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isTopNeghborEmpty(ElementPosition position)
		{
			if (position.Row - 1 >= 0)
			{
				return getElement((byte)(position.Row - 1), position.Col).Status == ElementStatus.EMPTY;
			}
			return false;
		}

		/// <summary>
		/// Returns suitable neighbors count of an element
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private byte suitableNeighborsForPathCount(ElementPosition position)
		{
			byte count = 0;
			if (isRightNeghborSuitableForPath(position))
			{
				count++;
			}
			if (isBottomNeghborSuitableForPath(position))
			{
				count++;
			}
			if (isLeftNeghborSuitableForPath(position))
			{
				count++;
			}
			if (isTopNeghborSuitableForPath(position))
			{
				count++;
			}
			return count;
		}

		/// <summary>
		/// Check if 2nd right element is suitable for making a path
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isRightNeghborSuitableForPath(ElementPosition position)
		{
			if (position.Col + 2 < Width)
			{
				if (getElement(position.Row, (byte)(position.Col + 2)).Status == ElementStatus.BLOCK)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Check if 2nd bottom element is suitable for making a path
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isBottomNeghborSuitableForPath(ElementPosition position)
		{
			if (position.Row + 2 < Height)
			{
				if (getElement((byte)(position.Row + 2), position.Col).Status == ElementStatus.BLOCK)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Check if 2nd left element is suitable for making a path
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isLeftNeghborSuitableForPath(ElementPosition position)
		{
			if (position.Col - 2 >= 0)
			{
				if (getElement(position.Row, (byte)(position.Col - 2)).Status == ElementStatus.BLOCK)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Check if 2nd top element is suitable for making a path
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool isTopNeghborSuitableForPath(ElementPosition position)
		{
			if (position.Row - 2 >= 0)
			{
				if (getElement((byte)(position.Row - 2), position.Col).Status == ElementStatus.BLOCK)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Marks empty neighbors on an element and adds them to a list
		/// </summary>
		/// <param name="elementPos"></param>
		/// <param name="edgeElementsPositions"></param>
		private void markEmptyNeighborsAndAddElementsWithEmptyNeighborsToList(ElementPosition elementPos, ref List<ElementPosition> edgeElementsPositions)
		{
			if (isTopNeghborEmpty(elementPos))
			{
				Map[elementPos.Row - 1, elementPos.Col].Status = ElementStatus.MARKED;
				Map[elementPos.Row - 1, elementPos.Col].SourcePos = elementPos;
				edgeElementsPositions.Add(Map[elementPos.Row - 1, elementPos.Col].Position);
			}
			if (isBottomNeghborEmpty(elementPos))
			{
				Map[elementPos.Row + 1, elementPos.Col].Status = ElementStatus.MARKED;
				Map[elementPos.Row + 1, elementPos.Col].SourcePos = elementPos;
				edgeElementsPositions.Add(Map[elementPos.Row + 1, elementPos.Col].Position);
			}
			if (isLeftNeghborEmpty(elementPos))
			{
				Map[elementPos.Row, elementPos.Col - 1].Status = ElementStatus.MARKED;
				Map[elementPos.Row, elementPos.Col - 1].SourcePos = elementPos;
				edgeElementsPositions.Add(Map[elementPos.Row, elementPos.Col - 1].Position);
			}
			if (isRightNeghborEmpty(elementPos))
			{
				Map[elementPos.Row, elementPos.Col + 1].Status = ElementStatus.MARKED;
				Map[elementPos.Row, elementPos.Col + 1].SourcePos = elementPos;
				edgeElementsPositions.Add(Map[elementPos.Row, elementPos.Col + 1].Position);
			}
		}

		/// <summary>
		/// Check if an element has any empty neighbors
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool hasEmptyNeighbor(ElementPosition position)
		{
			if (isTopNeghborEmpty(position))
			{
				return true;
			}
			if (isBottomNeghborEmpty(position))
			{
				return true;
			}
			if (isLeftNeghborEmpty(position))
			{
				return true;
			}
			if (isRightNeghborEmpty(position))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Check if an element has any suitable neighbors for making a path
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private bool hasSuitableNeighborForPath(ElementPosition position)
		{
			if (isRightNeghborSuitableForPath(position))
			{
				return true;
			}
			if (isBottomNeghborSuitableForPath(position))
			{
				return true;
			}
			if (isLeftNeghborSuitableForPath(position))
			{
				return true;
			}
			if (isTopNeghborSuitableForPath(position))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Clculates future blocks count by giving block pecentage
		/// </summary>
		/// <param name="percent"></param>
		/// <returns></returns>
		private ushort blocksCountByPercent(ref byte percent)
		{
			return (ushort)((Width * Height * percent / 100) - ((StartPos != null) ? 1 : 0) - ((FinishPos != null) ? 1 : 0));
		}

		public void ClearMaze()
		{
			clearMap();
		}

		/// <summary>
		/// Makes all of the maze elements empty
		/// </summary>
		private void clearMap()
		{
			for (byte i = 0; i < Height; i++)
			{
				for (byte j = 0; j < Width; j++)
				{
					Map[i, j].Status = ElementStatus.EMPTY;
				}
			}
			BlocksCount = 0;
		}

		/// <summary>
		/// Clears previous stored solutions
		/// </summary>
		public void ClearSolutions()
		{
			SolutionPath = null;
			if (MouseAlgSolutionProcedure != null)
			{
				MouseAlgSolutionProcedure.Clear();
			}
			if (FluidAlgSolutionProcedure != null)
			{
				FluidAlgSolutionProcedure.Clear();
			}
		}
	}
}