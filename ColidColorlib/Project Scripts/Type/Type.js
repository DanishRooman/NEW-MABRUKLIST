var Type = function () {
    //private static
    var handleCreateType = function () {

        //Ajax call
        $.ajax({
            url: '/Type/AddType',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#CreateType").html(result);
                $("#CreateType").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleTypeSuccess = function (result) {

        if (result.key) {


            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });

            $("#CreateType").modal("hide");

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
    var handleTypeList = function () {

        $.ajax({
            url: '/Type/TypeListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divType").empty();
                $("#divType").html(result);
            },
            error: function () {
                console.log("Error");
            }

        });
       
    };

    var handleEditType = function (id) {

        $.ajax({
            url: '/Type/GetType',
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
                    $("#CreateType").empty();
                    $("#CreateType").html(result);
                    $("#CreateType").modal("show");
                }
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleDeleteType = function (id) {
        $.confirm({
            title: 'Delete Type',
            content: 'Are you sure you want to delete the Type?',
            theme: 'Material',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/Type/DeleteType',
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
                                handleCategoryList();
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
                },
                cancel: function () {

                },
            }
        });
    };
    return {
        //public static
        initCreateType: function () {
            handleCreateType();
        },
        initTypeSuccess: function (result) {
            handleTypeSuccess(result)

        },
        initTypeList: function () {
            handleTypeList();
        },
        initEditType: function (id) {
            handleEditType(id);
        },
        initDeleteType: function (id) {
            handleDeleteType(id);
        },

    };
}();
//Document ready

$(function () {
    Type.initTypeList();
});




