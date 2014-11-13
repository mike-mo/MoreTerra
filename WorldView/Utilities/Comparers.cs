using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using MoreTerra.Structures;

namespace MoreTerra.Utilities
{
	class ChestComparerX : IComparer
	{
		int IComparer.Compare(object one, object two)
		{
			Int32 oneX, twoX;

			if (one.GetType() == typeof(TreeNode))
			{
				if (((TreeNode)one).Parent != null)
					return 1;

				oneX = getXCoord(((TreeNode)one).Text);
				twoX = getXCoord(((TreeNode)two).Text);

			} else {
				throw new NotImplementedException("ChestComparerX can not handle type " + one.GetType().ToString());
			}

			return oneX - twoX;
		}

		private Int32 getXCoord(String s)
		{
			String[] sa;
			//			"Chest at (" "," ")"
			sa = s.Split(new char[] { '(', ',' });

			return Int32.Parse(sa[1]);
		}
	}

	class ChestComparerY : IComparer
	{
		int IComparer.Compare(object one, object two)
		{
			Int32 oneY, twoY;

			if (one.GetType() == typeof(TreeNode))
			{
				if (((TreeNode)one).Parent != null)
					return 1;

				oneY = getYCoord(((TreeNode)one).Text);
				twoY = getYCoord(((TreeNode)two).Text);

			}
			else
			{
				throw new NotImplementedException("ChestComparerY can not handle type " + one.GetType().ToString());
			}

			return oneY - twoY;
		}

		private Int32 getYCoord(String s)
		{
			String[] sa;
			//			"Chest at (" "," ")"
			sa = s.Split(new char[] { ',', ')' });

			return Int32.Parse(sa[1]);
		}
	}

	class ChestListComparerX : IComparer<Chest>
	{
		public int Compare(Chest one, Chest two)
		{
			Int32 oneX, twoX;

			if (one == null || two == null)
				return 1;

			oneX = one.Coordinates.X;
			twoX = two.Coordinates.X;

			return oneX - twoX;
		}
	}

	class ChestListComparerY : IComparer<Chest>
	{
		public int Compare(Chest one, Chest two)
		{
			Int32 oneY, twoY;

			if (one == null || two == null)
				return 1;

			oneY = one.Coordinates.Y;
			twoY = two.Coordinates.Y;

			return oneY - twoY;
		}
	}

}
