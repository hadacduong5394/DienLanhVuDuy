var feed = {
    init: function () {
        feed.events();
    },
    events: function () {
        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/FeedBack/Detail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (response) {
                    var data = response.data;
                    var jsonDate = data.CreatedDate;
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    curr_month++; //January is represented by 0 
                    var curr_year = date.getFullYear();
                    var date = curr_date + "-" + curr_month + "-" + curr_year;

                    if (response.status == true) {
                        $('.lbName').html(data.Name);
                        $('.lbPhone').html(data.DienThoai);
                        $('.lbEmail').html(data.Address);
                        $('.lbCreateDate').html(date);
                        $('.lbContent').html(data.Content);
                        if (data.Status) { $('.lbStatus').html('Đã trả lời'); } else { $('.lbStatus').html('Chưa trả lời'); }



                        $('#ViewDetail').modal();
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
feed.init();