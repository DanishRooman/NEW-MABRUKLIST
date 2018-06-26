var Roles = function () {
    //private Static
    var handleCreateRoles = function () {

        //Ajax call
        $.ajax({
            url: '/Roles/AddRoles',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateRoles").empty();
                $("#CreateRoles").html(result);
                $("#CreateRoles").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleRolesSucsess = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateRoles").modal("hide");
            handleRoleList();
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
    var handleRoleList = function () {

        $.ajax({
            url: '/Roles/RolesListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divRoles").empty();
                $("#divRoles").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleDeleteRoles = function (id) {
        $.confirm({
            title: 'Delete Roles',
            content: 'Are you sure you want to delete this Roles?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Roles/DeleteRoles',
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
                                    handleRoleList();
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


    //public static
    return {

        initCreateRoles: function () {
            handleCreateRoles();
        },
        initRolesSucsess: function (result) {
            handleRolesSucsess(result)
        },
        initRolesList: function () {
            handleRoleList();
        },
        initDeleteRoles: function (id) {
            handleDeleteRoles(id);

        },
    };


}();
//Document Ready Load Page
$(function () {
    Roles.initRolesList();

});
