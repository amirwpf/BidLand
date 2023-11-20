using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidLand.Framework.Common;

public static class TraceTree
{
	public static List<Category> GetAllSubcategories(Category rootCategory)
	{
		if (rootCategory == null)
		{
			return new List<Category>();
		}

		var subcategories = new List<Category>();
		AddSubcategories(rootCategory, subcategories);
		return subcategories;
	}

	private static void AddSubcategories(Category category, List<Category> subcategories)
	{
		foreach (var subcategory in category.InverseParent)
		{
			subcategories.Add(subcategory);
			AddSubcategories(subcategory, subcategories);
		}
	}
}
