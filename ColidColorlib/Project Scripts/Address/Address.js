var Address = function () {
    //private static Function

    var handleCreateAddress = function () {
        debugger
        //Ajax call
        $.ajax({
            url: '/Address/AddAddress',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateAddress").empty();
                $("#CreateAddress").html(result);
                $("#CreateAddress").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleAddressSucsess = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateAddress").modal("hide");
            handleAddressList();

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

    var handleAddressList = function () {

        $.ajax({
            url: '/Address/AddressListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divAddress").empty();
                $("#divAddress").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleEditAddress = function (id) {
        debugger
        $.ajax({
            url: '/Address/GetAddress',
            type: 'GET',
            dataType: 'html',
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
                    $("#CreateAddress").empty();
                    $("#CreateAddress").html(result);
                    $("#CreateAddress").modal("show");
                }

            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleDeleteAddress = function (id) {
        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Address?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Address/DeleteAddress',
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


    // Public static Function
    return {
        initCreateAddress: function () {
            handleCreateAddress();
        },
        initAddressSucsess: function (result) {
            handleAddressSucsess(result);
        },
        initAddressList: function () {
            handleAddressList();
        },
        iniEditAddress: function (id) {
            handleEditAddress(id);
        },
        initDeleteAddress: function (id) {
            handleDeleteAddress(id)
        },

    };

}();
//Document ready load page

$(function () {
    Address.initAddressList();
});