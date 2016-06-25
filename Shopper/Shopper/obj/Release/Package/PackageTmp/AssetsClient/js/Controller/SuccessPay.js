var pay = {
    init: function () {
        pay.events();
    },
    events: function () {
        $('#btnBackToShop').off('click').on('click', function () {
            window.location.href = "/";
        });
    }
}
pay.init();
