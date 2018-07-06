var Event = function () {
   // private static function
    var gTable;
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
    var handleContactDatatable = function () {
        var group = $("#ddlgroups").val();
        group = group != undefined ? group : "";
        var congregation = $("#ddlCongregations").val();
        congregation = congregation != undefined ? congregation : "";
        var neighbourhood = $("#ddlNieghbourhood").val();
        neighbourhood = neighbourhood != undefined ? neighbourhood : "";
        $.ajax({
            url: "/Events/UserDetails",
            data: { "group": group, "Congregations": congregation, "neighbourhood": neighbourhood },
            type: "GET",
            cache: false,
            success: function (result) {
                gTable = $('#tblContacts').DataTable({
                    "destroy": true,
                    "data": result,
                    "columnDefs": [{
                        orderable: false,
                        targets: 1
                    }],
                    "columns": [
                     {
                         "data": "Users"
                     },
                     {
                         "data": "Action"
                     }
                    ]
                });
            },
            error: function (xhr, ajaxOptions, thrownError) {
                // $('#lblCommentsNotification').text("Error encountered while saving the comments.");
            }
        });
    };
    var handleCreateEvent = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $(".txtEventId").val(result.eventKey);
            $("#linkStep_2").click();
        }
        else {
            $.toast({
                heading: 'Error',
                text: result.value,
                showHideTransition: 'fade',
                icon: 'error'
            });
        }
    };
    var handleChooseContacts = function () {
        var guestLists = [];
        gTable = $('#tblContacts').DataTable();
        var eventId = $(".txtEventId").val();
        gTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
            debugger
            var data = this.data();
            var obj = $.parseHTML(data.Action);
            var userid = $(obj).val();
            guestLists.push({ "userId": userid, "EventId": eventId });
        });
        if (guestLists.length > 0) {
            var datalist = JSON.stringify({ guests: guestLists })
            $.ajax({
                url: '/Events/AddGuests',
                data: datalist,
                type: "post",
                cache: false,
                success: function (data) {

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert("Error");
                }
            });
        }
        else {
            alert("Please select a user");
        }

    };




    //public static function
    return {
        initRenderContact: function () {
            handleRenderContact();
        },
        initContactDatatable: function () {
            handleContactDatatable();
        },
        initChooseContacts: function () {
            handleChooseContacts();
        },

    };
}();


$(function () {
    Event.initRenderContact();
    Event.initContactDatatable();
  
});


