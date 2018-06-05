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
            dataType: 'HTML',
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
    var handleEditGroup = function () {

    };
    var handleDeleteGroup = function () {

    };
    //public Satic function
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
        initEditGroup: function () {
            handleEditGroup();
        },
        initDeleteGroup: function () {
            handleDeleteGroup();
        },

    };

}();
//Document ready load page
$(function () {
    Group.initGroupList();
});