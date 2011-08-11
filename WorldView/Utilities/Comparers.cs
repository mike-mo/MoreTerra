using System;
using System.Collections;
using System.Windows.Forms;

namespace MoreTerra.Utilities
{
	class ChestComparerX : IComparer
	{
		int IComparer.Compare(object one, object two)
		{
			if (((TreeNode)one).Parent != null)
				return 1;

			Int32 oneX = getXCoord(((TreeNode)one).Text);
			Int32 twoX = getXCoord(((TreeNode)two).Text);

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
			if (((TreeNode)one).Parent != null)
				return 1;

			Int32 oneX = getYCoord(((TreeNode)one).Text);
			Int32 twoX = getYCoord(((TreeNode)two).Text);

			return oneX - twoX;
		}

		private Int32 getYCoord(String s)
		{
			String[] sa;
			//			"Chest at (" "," ")"
			sa = s.Split(new char[] { ',', ')' });

			return Int32.Parse(sa[1]);
		}
	}

}
