using System;
using System.Drawing;
using System.Collections.Generic;

namespace MoreTerra.Structures
{
	public class ColorListDataNode
	{
		public Color defaultColor;
		public Color currentColor;

		public String defaultColorName;
		public String currentColorName;

		public Int32 nodeId;

		public ColorListDataNode()
		{

		}

		public ColorListDataNode(Color color, String name, Int32 id)
		{
			defaultColor = color;
			defaultColorName = name;

			currentColor = color;
			currentColorName = name;

			nodeId = id;
		}
	}

	public class ColorListData
	{
		private Dictionary<String, Dictionary<String, ColorListDataNode>> data;
		private String name;

		public ColorListData()
		{
			data = new Dictionary<String, Dictionary<String, ColorListDataNode>>();
		}

		public void AddNewNode(String nodeName, String parentName, Color color, String colorName, Int32 id)
		{
			if (!data.ContainsKey(parentName))
				data.Add(parentName, new Dictionary<String, ColorListDataNode>());

			if (data[parentName].ContainsKey(nodeName))
				return;

			data[parentName].Add(nodeName, new ColorListDataNode(color, colorName, id));
		}

		public ColorListDataNode GetNode(String nodeName, String parentName = "")
		{
			if (parentName != String.Empty)
			{
				if (data.ContainsKey(parentName))
					if (data[parentName].ContainsKey(nodeName))
						return data[parentName][nodeName];
			}
			else
			{
				foreach (KeyValuePair<String, Dictionary<String, ColorListDataNode>> kvp in data)
				{
					if (kvp.Value.ContainsKey(nodeName))
						return kvp.Value[nodeName];
				}
			}
			return null;
		}

		public void Clear()
		{
			data.Clear();
		}

		public Int32 Count
		{
			get
			{
				Int32 count = 0;
				foreach(KeyValuePair<String, Dictionary<String, ColorListDataNode>> kvp in data)
				{
					count += kvp.Value.Count;
				}

				return count;
			}
		}

		public Dictionary<String, Dictionary<String, ColorListDataNode>> Data
		{
			get
			{
				return data;
			}
		}

		public String Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}
	}
}
