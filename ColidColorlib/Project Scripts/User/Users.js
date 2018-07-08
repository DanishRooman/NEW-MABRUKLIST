var User = function () {
    //Private Static
var handleUserSucsess = function (result) {
        if (result.key) {
            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateGroup").modal("hide");
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
    var handleCreateList = function () {
        $.ajax({
            url: '/Account/AddUserListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divUserPanel").empty();
                $("#divUserPanel").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });


    };
  var  handleDeleteUser = function (id) {

        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Uaer?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Account/DeleteUser',
                            type: 'GET',
                            dataType: 'json',
                            data: { "id": id },
                            success: function (result) {
                                if (result.key) {
                                    $.toast({
                                        heading: 'Success',
                                        text: result.value,
                                        showHideTransition: 'slide',
                                        icon: 'success'
                                    });
                                    handleCreateList();
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


    //Public static
    return {

        initUserSucsess: function (result) {
            handleUserSucsess(result);
        },
        initUserList: function () {
            handleCreateList();
        },
        initDeleteUser: function (id) {
            handleDeleteUser(id);
        },
    };

}();
$(function () {
    User.initUserList();
});