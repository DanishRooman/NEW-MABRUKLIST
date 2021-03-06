﻿var Category = function () {
    //private static
    var handleCreateCategory = function () {
        //Ajax call
        $.ajax({
            url: '/Category/AddCategory',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#createCategory").empty();
                $("#createCategory").html(result);
                $("#createCategory").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    //toster Messages
    var handleCategorySuccess = function (result) {
        if (result.key) {
            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#createCategory").modal("hide");
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
    };
    /// end toster messages
    /// Select process
    var handleCategoryList = function () {
        $.ajax({
            url: '/Category/CategoryListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divCategory").empty();
                $("#divCategory").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleEditCategory = function (id) {
       
        $.ajax({
            url: '/Category/GetCategory',
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
                    $("#createCategory").empty();
                    $("#createCategory").html(result);
                    $("#createCategory").modal("show");
                }
            },
            error: function () {
                console.log("Error");
            }
        });
       
    };
    var handleDeleteCategory = function (id) {
        $.confirm({
            title: 'Delete Category',
            content: 'Are you sure you want to delete this Category?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Category/DeleteCategory',
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
                    }
                },
                cancel: function () {

                },
            }
        });
    };
    return {
        //public static
        initCreateCategory: function () {
            handleCreateCategory();
        },
        initCategorySuccess: function (result) {
            handleCategorySuccess(result);
        },
        initCategoryList: function () {
            handleCategoryList();
        },
        initEditCategory: function (id) {
            handleEditCategory(id);
        },
        initDeleteCategory: function (id) {
            handleDeleteCategory(id);
        },
        
    };
}();

//Document ready
$(function () {
    Category.initCategoryList();
});

