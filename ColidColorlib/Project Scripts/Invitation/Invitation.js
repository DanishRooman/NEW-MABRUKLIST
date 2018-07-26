var Invitation = function () {
    var handleAddInvitation = function () {
        $.ajax({
            url: '/Invitations/FileUploadModal',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $('#InvitationUploadModal').empty();
                $('#InvitationUploadModal').html(result);
                $("#InvitationUploadModal").modal({ backdrop: 'static', keyboard: false })
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleInvitationList = function () {
        $.ajax({
            url: '/Invitations/InvitationListings',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#InvitationsLists").empty();
                $("#InvitationsLists").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleZoom = function ($this) {
        $($this).parents(".thumbnail:first").find(".lightzoom:first").click();
    };
    var handleDeleteInvitation = function (id) {
        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to remove this Invitation Template?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Invitations/DeleteInvitation',
                            type: 'GET',
                            dataType: 'json',
                            data: { "Id": id },
                            success: function (result) {
                                if (result.key) {
                                    handleInvitationList();
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
                },
                cancel: function () {

                },
            }
        });
    };
    return {
        initAddInvitation: function () {
            handleAddInvitation();
        },
        initInvitationList: function () {
            handleInvitationList();
        },
        deleteInvitation: function (id) {
            handleDeleteInvitation(id);
        },
        initZoom: function ($this) {
            handleZoom($this);
        },
    };
}();


