using System;
using System.Collections.Generic;
using System.Linq;
using WeBuyWebApp.Data;
using WeBuyWebApp.Models;


namespace WeBuyWebApp.Services
{
	public class StocksService
	{
		private readonly ItemRepository _itemRepository;
		public StocksService(ItemRepository itemRepository)
		{
			_itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
		}

		public List<Stock> GetStocks()
		{
			return _itemRepository.GetAlItems()
				.Select(it => new Stock
				{
					ItemName = it.Name,
					Quantity = it.Quantity,
					Price = it.Price,
					BasePrice = it.BasePrice
				}).ToList();
		}

		public List<Stock> UpateStocks(int id, int qty)
		{

			return _itemRepository.UpdateItem(id, qty)
				.Select(it => new Stock
				{
					ItemName = it.Name,
					Quantity = it.Quantity,
					Price = it.Price,
					BasePrice = it.BasePrice
				}).ToList();
		}

		public List<Stock> DeleteStocks(int id, int qty)
		{

			return _itemRepository.DeleteItem(id, qty)
				.Select(it => new Stock
				{
					ItemName = it.Name,
					Quantity = it.Quantity,
					Price = it.Price,
					BasePrice = it.BasePrice
				}).ToList();
		}

		public bool IsStockAvailable(int? Qty = 0)
		{

			var stockQty = _itemRepository.GetAlItems().Select(x => x.Quantity - Qty);
			if (Convert.ToInt16(stockQty.ToString()) > 0)
				return true;

			return false;
		}
	}
}