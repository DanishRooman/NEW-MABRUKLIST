﻿var Event = function () {
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
    var handleSetInvitation = function (eventID) {
        $.ajax({
            url: '/Events/SetInvitationCard',
            type: 'GET',
            dataType: 'HTML',
            data: { "eventID": eventID },
            success: function (result) {
                $('#step-6').empty();
                $('#step-6').html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleVerifyContacts = function (eventID) {
        $.ajax({
            url: '/Events/VerifyContacts',
            type: 'GET',
            dataType: 'HTML',
            data: { "eventID": eventID },
            success: function (result) {
                $('#step-3').empty();
                $('#step-3').html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };


    var handleSendEmail = function () {
        var eventId = $(".txtEventId").val();
        if (eventId != 0 && eventId != "" && eventId != "0" && eventId != undefined && eventId != null) {
            $.ajax({
                url: '/Events/SendInvitations',
                type: 'GET',
                dataType: 'HTML',
                data: { "eventID": eventId },
                success: function (result) {
                    result = JSON.parse(result);
                    if (result.key) {
                        $.toast({
                            heading: 'Success',
                            text: result.value,
                            showHideTransition: 'slide',
                            icon: 'success'
                        });
                    }
                    else {
                        $.toast({
                            heading: 'Error',
                            text: result.value,
                            showHideTransition: 'fade',
                            icon: 'error'
                        });
                    }
                },
                error: function () {
                    console.log("Error");
                }
            });
        }
        else {
            $.toast({
                heading: 'Error',
                text: "Please add an event",
                showHideTransition: 'fade',
                icon: 'error'
            });
        }

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
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "data": result,
                    "columnDefs": [{
                        orderable: false,
                        targets: 2
                    }],
                    "columns": [
                        {
                            "data": "Id"
                        },
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
            handleSetInvitation(result.eventKey);
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

    var handleSendInvitations = function () {
        var eventId = $(".txtEventId").val();
        if (eventId != 0 && eventId != "" && eventId != "0" && eventId != undefined && eventId != null) {
            $.ajax({
                url: "/Events/SendInvitations",
                data: { "group": eventId },
                type: "GET",
                cache: false,
                success: function (result) {

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $.toast({
                        heading: 'Error',
                        text: "Unable to process your request. Please contact your admin",
                        showHideTransition: 'fade',
                        icon: 'error'
                    });
                }
            });
        }
        else {
            $.toast({
                heading: 'Error',
                text: "Please add an Event",
                showHideTransition: 'fade',
                icon: 'error'
            });
        }
    };
    var handleChooseContacts = function () {
        var guests = [];
        gTable = $('#tblContacts').DataTable();
        var eventId = $(".txtEventId").val();
        if (eventId != 0 && eventId != "" && eventId != "0" && eventId != undefined && eventId != null) {
            $('#tblContacts > tbody  > tr').each(function () {
                var cbox = $(this).find(".userscbox:first");
                if (cbox.prop("checked") == true) {
                    var userid = cbox.val();
                    guests.push({ userId: userid, EventId: eventId });
                }
            });

            if (guests.length > 0) {
                guests = JSON.stringify({ 'guests': guests });
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    type: 'POST',
                    url: '/Events/AddGuests',
                    data: guests,
                    success: function (result) {
                        if (result.key) {

                            $.toast({
                                heading: 'Success',
                                text: result.value,
                                showHideTransition: 'slide',
                                icon: 'success'
                            });
                            $("#linkStep_3").click();
                            handleVerifyContacts(eventId);
                        }
                        else {
                            $.toast({
                                heading: 'Error',
                                text: result.value,
                                showHideTransition: 'fade',
                                icon: 'error'
                            });
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert("Error");
                    }
                });
            }
            else {
                $.toast({
                    heading: 'Error',
                    text: "Please select a user",
                    showHideTransition: 'fade',
                    icon: 'error'
                });
            }
        }
        else {
            $.toast({
                heading: 'Error',
                text: "Please add an Event",
                showHideTransition: 'fade',
                icon: 'error'
            });
        }


    };

    var handleRemoveContact = function (guestKey) {
        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to remove this contact?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Events/DeleteGuest',
                            type: 'GET',
                            dataType: 'json',
                            data: { "guestKey": guestKey },
                            success: function (result) {
                                if (result.key) {
                                    $.toast({
                                        heading: 'Success',
                                        text: result.value,
                                        showHideTransition: 'slide',
                                        icon: 'success'
                                    });
                                    var eventId = $(".txtEventId").val();
                                    if (eventId != 0 && eventId != "" && eventId != "0" && eventId != undefined && eventId != null) {
                                        handleVerifyContacts(eventId);
                                    }
                                }
                                else {
                                    $.toast({
                                        heading: 'Error',
                                        text: result.value,
                                        showHideTransition: 'fade',
                                        icon: 'error'
                                    });
                                }
                            },
                            error: function () {
                                console.log("Error");
                            }
                        });
                    }
                },
                cancel: function () {

                },
            }
        });
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
        initCreateEvent: function (data) {
            handleCreateEvent(data);
        },
        initSendEmail: function () {
            handleSendEmail();
        },
        initRemoveContact: function (guestKey) {
            handleRemoveContact(guestKey);
        }

    };
}();


$(function () {
    Event.initRenderContact();
    Event.initContactDatatable();

});


