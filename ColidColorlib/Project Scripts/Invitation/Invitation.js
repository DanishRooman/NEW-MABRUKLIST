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
    return {
        initAddInvitation: function () {
            handleAddInvitation();
        },
    };
}();



