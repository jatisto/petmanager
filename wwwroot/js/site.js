// Write your JavaScript code.
$(function(){
    $("#file").on('change',function(e){
        var pickImage = $("#pick-image");
        var data = new FormData();
        var file = e.target.files[0];
        data.append('file',file);
        pickImage.html("Uploading. Please wait..");

        $.ajax({
            url: "/Pet/UploadImage",
            method: "post",
            data: data,
            contentType: false,
            processData: false,
            success: (result)=>{
                pickImage.html(result)
            },
            error: (error)=>{
                pickImage.html(error);
            }
        });
    });

    $("#notification").on('click',()=>{
        event.preventDefault();
        $.ajax({
            url: '/Notification/ReadNotifications',
            method: 'get',
            data: {},
            success: (result)=>{
                $("#notification").popover({
                    'html':true,
                    'content':result,
                    'title':'Notifications',
                    'placement':'bottom'
                });
            },
            error: (error)=>{
                console.log("Error occured");
            }
        });
    });

    function checkNotifications (){
        //check if notifications exists
        $.ajax({
            url: '/Notification/Index',
            method: 'get',
            data: {},
            success: (result)=>{
                if(result>0){
                    $("#notification").html(result);
                }
            },
            error: (error)=>{
                console.log(error);
            }
        });
    }


    let connection = new signalR.HubConnection('/signalServer');

    connection.on('readNotifications', () => {
       checkNotifications();
    });

    connection.start();
    
    checkNotifications();
});
