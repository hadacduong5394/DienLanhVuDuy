var rejCart = {
    init: function () {
        rejCart.events();
    },
    events: function () {
        $('#btnComeBackToCart').off('click').on('click', function () {
            window.location.href = "/gio-hang";
        });
        $('#btnPay').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
    }
}
rejCart.init();