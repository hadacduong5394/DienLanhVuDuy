var about = {
    init: function () {
        about.events();
    },
    events: function () {
        $('.editModel').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            window.location.href = "/Admin/About/Edit?id=" + id;
        });

        $('.ViewDetail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/About/Detial",
                data: { id: id },
                type: "post",
                dataType: "json",
                success: function (response) {
                    var data = response.data;
                    var jsonDate = data.CreateDate;
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date = date.getDate();
                    var curr_month = date.getMonth();
                    curr_month++; //January is represented by 0 
                    var curr_year = date.getFullYear();
                    var date = curr_month + "/" + curr_date + "/" + curr_year;

                    var jsonDate1 = data.ModifiedDate;
                    var date1 = new Date(parseInt(jsonDate.substr(6)));
                    var curr_date1 = date1.getDate();
                    var curr_month1 = date1.getMonth();
                    curr_month1++; //January is represented by 0 
                    var curr_year1 = date1.getFullYear();
                    var date1 = curr_month1 + "/" + curr_date1 + "/" + curr_year1;


                    if (response.status == true) {
                        $('.lbName').html(data.Name);
                        $('.lbDescription').html(data.Description);
                        $('.lbDetail').html(data.Detail);
                        $('.lbCreateDate').html(date);
                        $('.lbCreateBy').html(data.CreateBy);
                        $('.lbModifineDate').html(date1);
                        $('.lbModifineBy').html(data.ModifiedBy);
                        if (data.Status) { $('.lbIsPub').html('đang hoạt động'); } else { $('.lbIsPub').html('khóa'); }
                        $('#ViewDetail').modal();
                    }
                }
            });
        });
    }
}
about.init();