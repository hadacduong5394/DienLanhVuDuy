var common = {
    init: function(){
        common.events();
    },
    events: function(){
        $('#AlertBox').removeClass('hide');
        $('#AlertBox').delay(2000).slideUp(500);
    }
}
common.init();