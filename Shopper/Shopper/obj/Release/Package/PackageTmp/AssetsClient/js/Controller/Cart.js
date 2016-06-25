var Cart = {
    init: function () {
        Cart.events();
    },
    events: function () {
        $('#btnBack').off('click').on('click', function () {
            window.location.href = "/";
        });
       
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: "/Cart/DeleteAll",
                dataType: "json",
                type: "Post",
                success: function (res) {
                    if(res.status == true){
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });
        $('.deleteCartItem').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: "/Cart/Delete",
                data: {productID: $(this).data('id')},
                dataType: "json",
                type: "Post",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            var lstProduct = $('.txtQuantity');
            var cartList = [];
            $.each(lstProduct, function (i, item) {
                cartList.push({
                    quantity: $(this).val(),
                    product:{
                        ID: $(this).data('id')
                    }
                    
                });
            });

            $.ajax({
                url: "/Cart/UpdateCart",
                data: { model: JSON.stringify(cartList) },
                dataType: "json",
                type: "Post",
                success: function (res) {
                    if (res.status == 1) {
                        window.location.href = "/xac-nhan";
                    } else if (res.status == 0) {
                        var productID = res.data;
                        alert('mã hàng ' + productID + ' vượt quá số lượng cửa hàng có!');
                    }
                }
            });
        });
    }
}
Cart.init();