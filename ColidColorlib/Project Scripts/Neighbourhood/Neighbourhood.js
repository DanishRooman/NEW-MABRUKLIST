var Neighbourhood = function () {
    //private Static
    var handleCreateNeighbourhood = function () {

        //Ajax call
        $.ajax({
            url: '/Neighbourhood/AddNeighbourhood',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateNeighbourhood").empty();
                $("#CreateNeighbourhood").html(result);
                $("#CreateNeighbourhood").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };

    var handleNeighbourhoodSucsess = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateNeighbourhood").modal("hide");
            handleNeighbourhoodList();
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

    var handleNeighbourhoodList=  function(){
    
        $.ajax({
            url: '/Neighbourhood/NeighbourhoodListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divNeighbourhood").empty();
                $("#divNeighbourhood").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleEditNeighbourhood = function (id) {
      
        $.ajax({
            url: '/Neighbourhood/GetNeighbourhood',
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
                    $("#CreateNeighbourhood").empty();
                    $("#CreateNeighbourhood").html(result);
                    $("#CreateNeighbourhood").modal("show");
                }

            },
            error: function () {
                console.log("Error");
            }
        });

    };

    var handleDeleteNeighbourhood = function (id) {

        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this group?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Neighbourhood/DeleteNeighbourhood',
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
                                    handleNeighbourhoodList();
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
    //public Static
    return {

        initCreateNeighbourdhood: function () {

            handleCreateNeighbourhood();
        },

        initNeighbourhoodSucsess: function (result) {

            handleNeighbourhoodSucsess(result);

        },
        initNeighbourhoodList: function () {
            handleNeighbourhoodList();

        },
        initEditNeighbourhood: function (id) {

            handleEditNeighbourhood(id);

        },

        initDeleteNeighbourhood: function (id) {

            handleDeleteNeighbourhood(id);
        },


    }

}();
$(function () {
    Neighbourhood.initNeighbourhoodList();
});