var Neighborhood = function () {
    // Private Static
   
    var handleCreateNeighborhood = function () {
        debugger
        $.ajax({
            url: '/Neighborhood/AddNeighborhood',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#CreateNeighborhood").html(result);
                $("#CreateNeighborhood").modal("show");
                handleNeighborhoodList();
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleNeighborhoodSucsess = function (result) {
        if (result.key) {
                $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });

                $("#CreateNeighborhood").modal("hide");
                handleNeighborhoodList();

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

    var handleNeighborhoodList = function () {
        
        $.ajax({
            url: '/Neighborhood/NeighborhoodListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divNeighborhood").empty();
                $("#divNeighborhood").html(result);
            },
            error: function () {
                console.log("Error");
            }

        });
    };
    var handleEditNeighborhood = function (id) {
       
        $.ajax({
            url: '/Neighborhood/GetNeighborhood',
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
                    $("#CreateNeighborhood").empty();
                    $("#CreateNeighborhood").html(result);
                    $("#CreateNeighborhood").modal("show");
                  

                }
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleDeleteNeighborhood = function (id) {
        debugger
        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Neighborhood?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Neighborhood/DeleteNeighborhood',
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
                                    handleNeighborhoodList();
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

        initCreateNeighborhood: function () {

            handleCreateNeighborhood();
        },
        initNeighbordhoodSucsess: function (result) {
            handleNeighborhoodSucsess(result);

        },
        initNeighboorhoodList: function () {
            handleNeighborhoodList();
        },
        initEditNeighborhood: function (id) {
            handleEditNeighborhood(id);
        },
        initDeleteNeighborhood: function (id) {
            handleDeleteNeighborhood(id);
        },

    }

}();
//Document ready

$(function () {
    Neighborhood.initNeighboorhoodList();
});