﻿var Event = function () {

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
                $('#tblContacts').DataTable({
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

   var handleCreateDataEvent = function () {
        debugger
        alert("Hello World");
   };

    return {
        initRenderContact: function () {
            handleRenderContact();
        },
        initContactDatatable: function () {
            handleContactDatatable();
        },
        initcreateDataEvent: function () {
            handleCreateDataEvent();
        },
    };
}();


$(function () {
    Event.initRenderContact();
    Event.initContactDatatable();
});

