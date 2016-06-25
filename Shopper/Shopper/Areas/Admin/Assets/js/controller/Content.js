var content = {
    init: function () {
        content.events();
    },
    events: function () {
        $('#btnCreate').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/Admin/News/Create";
        });

        $('.btn-uptop').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/News/upTop",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        alert("bài viết này đã lên đầu trang nhất");
                    } else {
                        alert("đưa bài viết lên trang nhất thất bại, hãy thử lại");
                    }
                }
            });
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var btn = $(this);
            var htmlLock = '<i class="' + "fa fa-lock" + '"></i>';
            var htmlunLock = '<i class="' + "fa fa-unlock" + '"></i>';
            $.ajax({
                url: "/Admin/News/changeStatus",
                data: { id: id },
                type: "post",
                dataType: "json",
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

        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/News/Detail",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (response) {
                    var data = response.data;
                    var jsonDate = data.content.CreateDate;
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    curr_month++; //January is represented by 0 
                    var curr_year = date.getFullYear();
                    var date = curr_date + "/" + curr_month + "/" + curr_year;

                    var jsonDate1 = data.content.ModifiedDate;
                    var date1 = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date1 = date1.getDate();
                    var curr_month1 = date1.getMonth();
                    curr_month1++; //January is represented by 0 
                    var curr_year1 = date1.getFullYear();
                    var date1 = curr_date1 + "/" + curr_month1 + "/" + curr_year1;


                    if (response.status == true) {
                        $('.lbName').html(data.content.Name);
                        $('.lbDescription').html(data.content.Description);
                        $('.lbDetail').html(data.content.Detail);
                        $('.lbImage').html(data.content.Image);
                        $('.lbCategory').html(data.category.Name);
                        $('.lbCreateDate').html(date);
                        console.log(date);
                        $('.lbCreateBy').html(data.content.CreateBy);
                        $('.lbModifineDate').html(date1);
                        $('.lbModifineBy').html(data.content.ModifiedBy);
                        $('.lbCount').html(data.content.ViewCount);
                        if (data.content.Status) { $('.lbIsPub').html('đang hoạt động'); } else { $('.lbIsPub').html('khóa'); }



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
content.init();