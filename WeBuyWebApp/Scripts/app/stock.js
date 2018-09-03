
var stocksModule = (function () {

    function init(element) {
        $.ajax({
            url: "/api/Stocks"
        }).done(render);

        function render(stocks) {
            var stockCount = 0;
            stocks.forEach(function (stock) {
                BuildTableRows(stock, stockCount);
                stockCount++;
            });
        }
    }

    function buy() {
        if (!validateForm())
            return;

        $.ajax({
            url: "/api/Stocks/Buy/" + $('input[name=stocks]:checked').val() + "/" + $("#txt_Qty").val(),
            dataType: "JSON"
        }).done(render);

        function render(stocks) {
            var stockCount = 0;
            stocks.forEach(function (stock) {
                //stock one bind with radio button
                BuildTableRows(stock, stockCount);
                stockCount++;
            });
        }
    }

    function sell() {
        if (!validateForm())
            return;

        if ($("#txt_Qty").val() > $('input[name=stocks]:checked').data("qty")) {
            alert("Please enter below than " + $('input[name=stocks]:checked').data("qty") + "..");
            $("#txt_Qty").val($('input[name=stocks]:checked').data("qty"));
            return;
        }

        $.ajax({
            url: "/api/Stocks/Sell/" + $('input[name=stocks]:checked').val() + "/" + $("#txt_Qty").val(),
            dataType: "JSON"
        }).done(render);

        function render(stocks) {
            var stockCount = 0;
            stocks.forEach(function (stock) {
                BuildTableRows(stock, stockCount);
                stockCount++;
                if (stock.Quantity <= 0) {
                    $("#btn_Sell").attr("disabled", "disabled");
                }
            });
        }
    }

    function validateForm() {

        if ($('input[name=stocks]:checked').length == 0) {
            alert("please select product");
            return false;
        }

        if ($("#txt_Qty").val() === "") {
            alert("please enter product Quantity");
            return false;
        }

        return true;
    }


    function BuildTableRows(stock, stockCount) {
        if (stockCount == 0) {
            $("input[id=rad-1]").attr("data-qty", stock.Quantity);
        } else {
            $("input[id=rad-2]").attr("data-qty", stock.Quantity);
        }

        var tableRow = "<tr><td>" + stock.ItemName + "</td><td><b>" + stock.Quantity + "</b></td><td><b>" + stock.Price + "</b></td><td>" + stock.BasePrice + "</td></tr>";
        $("#stocks").find("table").append(tableRow);
    }

    return {
        init: init,
        Buy: buy,
        Sell: sell
    };
})();



$(document).ready(function () {
    //Load Stocks on page Load Event
    stocksModule.init($("#stocks"));

    $(document).on("input", "#txt_Qty", function () {
        this.value = this.value.replace(/\D/g, '');
    });

    $("input[name=stocks]").click(function () {
        if ($(this).attr("data-qty") <= 0) {
            $("#btn_Sell").attr("disabled", "disabled");
        } else {
            $("#btn_Sell").removeAttr("disabled");
        }
    });

});


