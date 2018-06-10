var Categories = function () {
    //private static

    var handleCreateCategories = function () {
        //Ajax Call

        $.ajax({
            url: '/Categories/AddCategories',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateCategories").empty();
                $("#CreateCategories").html(result);
                $("#CreateCategories").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });


    };

    var handleSucsessCategories = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateCategories").modal("hide");
            handleCategoriesList();

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

    var handleCategoriesList = function () {

        $.ajax({
            url: '/Categories/CategoriesListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divACategories").empty();
                $("#divACategories").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleEditCategories = function (id) {

        $.ajax({
            url: '/Categories/GetCategories',
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
                    $("#CreateCategories").empty();
                    $("#CreateCategories").html(result);
                    $("#CreateCategories").modal("show");
                }

            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleDeleteCategories = function (id) {

           $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Category?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Categories/DeleteCategories',
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
                                    handleCategoriesList();
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

        initCreateCategories: function () {


            handleCreateCategories();
        },
        initCategoriesSucsess: function (result) {

            handleSucsessCategories(result);

        },
        initCategoriesList: function () {

            handleCategoriesList();
        },
        initEditCategories: function (id) {

            handleEditCategories(id);
        },
        initDeleteCategories: function (id) {

            handleDeleteCategories(id)
        },





    
    };


}();
$(function () {

    Categories.initCategoriesList();
});