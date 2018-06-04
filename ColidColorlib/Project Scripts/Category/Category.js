var Category = function () {
    //private static
    var handleCreateCategory = function () {
        //Ajax call
        $.ajax({
            url: '/Group/AddGroup',
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

       
    };
    var handleDeleteCategory = function (id) {
       
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

