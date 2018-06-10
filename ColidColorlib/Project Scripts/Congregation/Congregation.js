var Congregation = function () {
    //private static

    var handleCreateCongregation = function () {

        //Ajax call
        $.ajax({
            url: '/Congregation/AddCongregation',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateCongregation").empty();
                $("#CreateCongregation").html(result);
                $("#CreateCongregation").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };

    var handleCongregationSucsess = function (result) {
  
        if (result.key) {
            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateCongregation").modal("hide");
            handleCongregationList();
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

    var handleCongregationList = function () {
        $.ajax({
            url: '/Congregation/CongregationListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divCongregation").empty();
                $("#divCongregation").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleEditCongregation = function (id) {

        $.ajax({
            url: '/Congregation/GetCongregation',
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
                    $("#CreateCongregation").empty();
                    $("#CreateCongregation").html(result);
                    $("#CreateCongregation").modal("show");
                }
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleDeleteCongregation = function (id) {

        $.confirm({
            title: 'Delete Category',
            content: 'Are you sure you want to delete this Congregation?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Congregation/DeleteCongregation',
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
                                    handleCongregationList();
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

        initCreateCongregation: function () {


            handleCreateCongregation();
        },
        initCongregationSucsess: function (result) {

            handleCongregationSucsess(result)
        },

        initCongregationList: function () {

            handleCongregationList();
        },
        initEditCongregation: function (id) {

            handleEditCongregation(id);

        },
        initDeleteCongregation: function (id) {

            handleDeleteCongregation(id);
        },

    };



}();

$(function () {

    Congregation.initCongregationList();
});