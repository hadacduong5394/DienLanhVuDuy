var c = {
    init: function () {
        c.events();
    },
    events: function () {
        $('.editContact').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            window.location.href = "/Admin/Contact/Edit?id=" + id;
        });

        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Contact/Detail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (response) {
                    var data = response.data;
                    $('.lbDetail').html(data.Content);
                    if (data.Status) { $('.lbIsPub').html('đang hoạt động'); } else { $('.lbIsPub').html('khóa'); }
                    $('#ViewDetail').modal();

                }
            });
        });
    }
}
c.init();