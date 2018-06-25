var Event = function () {

    var handleRenderContact = function () {
        $.ajax({
            url: '/Events/AddContact',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $('#step-2').empty();
                $('#step-2').html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    return {
        initRenderContact: function () {
            handleRenderContact();
        },
    };
}();


$(function () {
    Event.initRenderContact();
});
