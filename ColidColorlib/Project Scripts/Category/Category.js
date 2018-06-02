var Category = function () {
    //private static
    var handleCreateCategory = function () {
        //ajax call
        $.ajax({
            url: '/Category/AddCategory',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#createCategory").html(result);
                $("#createCategory").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleCategorySuccess = function (result) {
        if(result.key)
        {
            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#createCategory").modal("hide");
        }
        else
        {
            $.toast({
                heading: 'Error',
                text: result.value,
                showHideTransition: 'fade',
                icon: 'error'
            });
        }
    };
    return {
        //public static
        initCreateCategory: function () {
            handleCreateCategory();
        },
        initCategorySuccess:function(result)
        {
            handleCategorySuccess(result);
        },
    };
}();

