var alter = {
    init: function(){
        alter.eventAlter();
    },
    eventAlter: function(){
        $('#ddlCategory').off('change').on('change', function (e) {
            e.preventDefault();
            var id = $(this).val();
            console.log(id);
            var page = 1;
            $.ajax({
                url: "/Admin/Product/partialProduct",
                data: { page: page, categoryID: id },
                type: "POST",
                success: function (response) {
                    $('#targetProduct').html(response);
                }
            });
        });
    }
}
alter.init();