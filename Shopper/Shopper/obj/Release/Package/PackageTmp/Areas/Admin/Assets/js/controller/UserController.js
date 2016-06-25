var user = {
    init: function () {
        user.regesterEvents();
    },
    regesterEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            var htmlLock = '<i class="' + "fa fa-lock" + '"></i>';
            var htmlunLock = '<i class="' + "fa fa-unlock" + '"></i>';
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { userID: id },
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

        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/User/Detail",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {

                    var data = response.data;

                    var jsonDate = data.CreateDate;
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    curr_month++; //January is represented by 0 
                    var curr_year = date.getFullYear();
                    var date = curr_date + "/" + curr_month + "/" + curr_year;

                    var jsonDate1 = data.ModifiedDate;
                    var date1 = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date1 = date1.getDate();
                    var curr_month1 = date1.getMonth();
                    curr_month1++; //January is represented by 0 
                    var curr_year1 = date1.getFullYear();
                    var date1 = curr_date1 + "/" + curr_month1 + "/" + curr_year1;

                    if (response.status == true) {
                        $('.lbUserName').html(data.UserName);
                        $('.lbFullName').html(data.FullName);
                        $('.lbAddress').html(data.Address);
                        $('.lbPhone').html(data.Phone);
                        $('.lbEmail').html(data.Email);
                        $('.lbCreateDate').html(date);
                        $('.lbCreateBy').html(data.CreateBy);
                        $('.lbModifineBy').html(data.ModifiedBy);
                        $('.lbModifineDate').html(date1);
                        if (data.PrivateID == 1) {
                            $('.lbPermision').html('ADMIN');
                        } else if (data.PrivateID == 2) {
                            $('.lbPermision').html('USER');
                        } else {
                            $('.lbPermision').html('MOD');
                        }
                        if (data.Status) { $('.lbIsPub').html('đang hoạt động'); } else { $('.lbIsPub').html('khóa'); }

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
user.init();