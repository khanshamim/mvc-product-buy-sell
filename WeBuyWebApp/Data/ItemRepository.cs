using System.Collections.Generic;
using WeBuyWebApp.Helpers;
using WeBuyWebApp.Models;

namespace WeBuyWebApp.Data
{
	public class ItemRepository
	{
		private static readonly List<Item> _items;
		public enum PriceType { Increased = 0, Decreased = 1 };


		public List<Item> GetAlItems()
		{
			return _items;
		}

		public List<Item> UpdateItem(int id, int qty)
		{
			_items.Find(x => x.Id == id).Quantity += qty;

			_items.Find(x => x.Id == id).Price = GetCalculatedPrice(_items.Find(x => x.Id == id).BasePrice, _items.Find(x => x.Id == id).Price, (int)PriceType.Decreased);

			return _items;
		}

		public List<Item> DeleteItem(int id, int qty)
		{
			_items.Find(x => x.Id == id).Quantity -= qty;

			_items.Find(x => x.Id == id).Price = GetCalculatedPrice(_items.Find(x => x.Id == id).BasePrice, _items.Find(x => x.Id == id).Price, (int)PriceType.Increased);

			return _items;
		}

		public int GetCalculatedPrice(int basePrice, int price, int Type)
		{
			if (Type == (int)PriceType.Decreased)
			{
				var PriceDecreasedByPercentage = ConfigurationManagerHelper.PriceDecreaseBy();

				var PriceRangeMinPercentage = ConfigurationManagerHelper.PriceMinimum();

				var MinimumPrice = basePrice * PriceRangeMinPercentage / 100;

				var PriceDecreasedValue = price - (price * PriceDecreasedByPercentage / 100);

				if (PriceDecreasedValue < MinimumPrice)
				{
					//Set Minimum value-- Should not fall below 75% of base price
					return basePrice;
				}
				return PriceDecreasedValue;
			}
			else
			{
				var PriceIncreasedByPercentage = ConfigurationManagerHelper.PriceIncreaseBy();

				var PriceRangeMaxPercentage = ConfigurationManagerHelper.PriceMaximum();

				var MaximumPrice = basePrice * PriceRangeMaxPercentage / 100;

				var PriceIncreasedValue = price + (price * PriceIncreasedByPercentage / 100);

				if (PriceIncreasedValue > MaximumPrice)
				{
					//Set Maximum value-- Should not rise above 125% of base price
					return basePrice;
				}
				return PriceIncreasedValue;
			}

		}


		static ItemRepository()
		{
			_items = new List<Item>
			{
				new Item
				{
					Id = 1,
					Name = "item A",
					Price = 100,
					Quantity = 10,
					BasePrice = 100
				},
				new Item
				{
					Id = 2,
					Name = "Item B",
					Price = 150,
					Quantity = 10,
					BasePrice = 100
				}
			};
		}



	}
}