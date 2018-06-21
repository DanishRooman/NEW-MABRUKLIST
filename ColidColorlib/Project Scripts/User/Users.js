var User = function () {
    //Private Static





    var handleUserSucsess = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateGroup").modal("hide");

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
    //Public static
    return {

        initUserSucsess: function (result) {

            handleUserSucsess(result);


        },

    };



}();