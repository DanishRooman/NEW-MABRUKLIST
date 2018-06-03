var Type = function () {
    //private static
    var handleCreateType = function () {

        //Ajax call
        $.ajax({
            url: '/Type/AddType',
            type: 'GET',
            dataType: 'HTML',
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
    return {
        //public static
        initCreateType: function () {
            handleCreateType();
        },
        initTypeSuccess: function (result) {
            handleTypeSuccess(result)

        },

    };
}();




