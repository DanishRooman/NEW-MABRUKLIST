var Event=function(){

    var handleDropdownList = function (e) {
       
        //Ajax call
        $.ajax({
            url: '/Events/AddContact',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $('.ddlList').html(result);
                $('')
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    return {
        initDropDownList: function (e) {
        
            handleDropdownList(e);
        },

    };



}();