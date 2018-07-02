var AddUser = function () {


    var handleSuccsessAddUser = function (result) {
        debugger
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });

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

        initSucessAddUser: function (result) {

            handleSuccsessAddUser(result);
        },


    };



}();