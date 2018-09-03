using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;
using WeBuyWebApp.Models;
using WeBuyWebApp.Services;

namespace WeBuyWebApp.Controllers
{
    public class StocksController : ApiController
    {
        private readonly StocksService _stocksService;
               
        public StocksController(StocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        [Route("api/Stocks")]
        [AcceptVerbs("GET")]
        public List<Stock> Get()
        {
            List<Stock> stocks = _stocksService.GetStocks();
            return stocks;
        }

        [Route("api/Stocks/Buy/{id}/{qty}")]
        [AcceptVerbs("GET")]
        public List<Stock> Buy(int id, int qty)
        {
            List<Stock> stocks = _stocksService.UpateStocks(id, qty);
            return stocks;
        }

        [Route("api/Stocks/Sell/{id}/{qty}")]
        [AcceptVerbs("GET")]
        public List<Stock> Sell(int id, int qty)
        {
            List<Stock> stocks = _stocksService.DeleteStocks(id, qty);
            return stocks; ;
        }



    }
}