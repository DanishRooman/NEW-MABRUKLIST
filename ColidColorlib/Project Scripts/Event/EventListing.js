var EventListing = function () {
    var handleEventListing = function () {
        $.ajax({
            url: '/Events/EventListings',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#EventListing_group").empty();
                $("#EventListing_group").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    return {
        initEventListing: function () {
            handleEventListing();
        },

    };
}();

$(function () {
    EventListing.initEventListing();
});