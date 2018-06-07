var Title = function(){

    //private static functions
    var handleCreateTitle = function () {
        //Ajax Call
        debugger
        $.ajax({
            url: '/Title/AddTitle',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateTitle").empty();
                $("#CreateTitle").html(result);
                $("#CreateTitle").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleTitleSucsess = function (result) {
        debugger
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            $("#CreateTitle").modal("hide");
            //handleGroupList();
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

    var handleTitleList = function ()
    {
        $.ajax({
            url: '/Title/TitleListing',
            type: 'GET',
            dataType: 'html',
            data: {},
            success: function (result) {
                $("#divTitle").empty();
                $("#divTitle").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleEditTitle = function (id) {
        debugger
        $.ajax({
            url: '/Title/GetTitle',
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
                    $("#CreateTitle").empty();
                    $("#CreateTitle").html(result);
                    $("#CreateTitle").modal("show");
                }

            },
            error: function () {
                console.log("Error");
            }
        });
    };

    var handleDeleteTitle = function (id) {

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
                            url: '/Title/DeleteTitle',
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
                                    handleTitleList();
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
    //Public static functions
    return {

        initCreateTitle: function () {
            handleCreateTitle();
        },
        initTitleSucsess: function (result) {
            handleTitleSucsess(result);
        },
        initTitleList: function () {
            handleTitleList();
        },
        initEditTitle: function (id) {
            handleEditTitle(id);
        },
        initDeleteTitle: function (id) {
            handleDeleteTitle(id);
        },

    }

}();

//Document ready load page
$(function () {
    Title.initTitleList();
});