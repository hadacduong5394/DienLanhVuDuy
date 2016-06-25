var category = {
    init: function () {
        category.regesterEvents();
    },
    regesterEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            var htmlLock = '<i class="' + "fa fa-lock" + '"></i>';
            var htmlunLock = '<i class="' + "fa fa-unlock" + '"></i>';
            $.ajax({
                url: "/Admin/Category/ShowOnHome",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.html(htmlunLock);
                    } else {
                        btn.html(htmlLock);
                    }
                }
            });
        });
        $('#confirm-delete').on('show.bs.modal', function (e) {
            $(this).find('.btn-ok').attr('href', $(e.relatedTarget).data('href'));

            $('.debug-url').html('Delete URL: <strong>' + $(this).find('.btn-ok').attr('href') + '</strong>');
        });
    }
}
category.init();