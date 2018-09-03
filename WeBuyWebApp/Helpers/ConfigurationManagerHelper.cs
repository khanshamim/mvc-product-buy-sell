using System;
using System.Configuration;

namespace WeBuyWebApp.Helpers
{
	public static class ConfigurationManagerHelper
	{
		public static int PriceIncreaseBy()
		{
			return Convert.ToInt16(ConfigurationManager.AppSettings["price_increase_percentage"].ToString());
		}
		public static int PriceDecreaseBy()
		{
			return Convert.ToInt16(ConfigurationManager.AppSettings["price_decrease_percentage"].ToString());
		}
		public static int PriceMaximum()
		{
			return Convert.ToInt16(ConfigurationManager.AppSettings["price_range_max"].ToString());
		}
		public static int PriceMinimum()
		{
			return Convert.ToInt16(ConfigurationManager.AppSettings["price_range_min"].ToString());
		}
	}

}