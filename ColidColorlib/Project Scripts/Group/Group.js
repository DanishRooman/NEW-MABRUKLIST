var Group = function () {

    //private Static function
    var handleCreatGroup = function () {

        //Ajax call
        $.ajax({
            url: '/Group/AddGroup',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateGroup").empty();
                $("#CreateGroup").html(result);
                $("#CreateGroup").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleGroupSucsess = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateGroup").modal("hide");
            handleGroupList();
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

    //groupList Table
    //Select Process
    var handleGroupList = function () {

        $.ajax({
            url: '/Group/GroupListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divGroup").empty();
                $("#divGroup").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleEditGroup = function (id) {

        $.ajax({
            url: '/Group/GetGroup',
            type: 'GET',
            dataType: 'HTML',
            data: { "id": id },
            success: function (result) {
                if (result.key == false) {
                    $.toast({
                        heading: 'Error',
                        text: result.value,
                        showHideTransition: 'fade',
                        icon: 'error'
                    });
                }
                else {
                    $("#CreateGroup").empty();
                    $("#CreateGroup").html(result);
                    $("#CreateGroup").modal("show");
                }

            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleDeleteGroup = function (id) {

        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this group?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Group/DeleteGroup',
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
                                    handleGroupList();
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
    //public Static functions
    return {
        initCreateGroup: function () {

            handleCreatGroup();
        },
        initGroupSeccess: function (result) {

            handleGroupSucsess(result);
        },
        initGroupList: function () {
            handleGroupList();
        },
        initEditGroup: function (id) {
            handleEditGroup(id);
        },
        initDeleteGroup: function (id) {
            handleDeleteGroup(id);
        },

    };

}();
//Document ready load page
$(function () {
    Group.initGroupList();
});