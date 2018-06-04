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
        debugger
    };
    var handleDeleteCategory = function (id) {
        debugger
        App.blockUI({
            boxed: true
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

