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
        var handleEditType = function (id) {
            //App.blockUI({
            //    boxed:true
            //});
        };

        var handleDeleteType = function (id) {
            //App.blockUI({
            //    boxed: true
            //});
        };
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
            //handleEditType();
        },
        initDeleteType: function (id) {
            //handleDeleteType();

        },

    };
}();
//Document ready

$(function () {
    Type.initTypeList();
});




