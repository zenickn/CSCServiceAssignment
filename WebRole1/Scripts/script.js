
//get data from json file
//var urlForJson = "data.json";


//get data from Restful web Service in development environment
var urlForJson = "https://localhost:44364/api/v1/GetAllTalents";

//get data from Restful web Service in production environment
//var urlForJson= "http://csc123.azurewebsites.net/api/talents";

//Url for the Cloud image hosting
var urlForCloudImage = "http://res.cloudinary.com/doh5kivfn/image/upload/v1460006156/talents/";

//token 
var inToken = "";
var searchField;
var myExp;
var msgBox = $('#msgBox');

$('#login').click( function () {
    var inUsername = $('#username').val();
    var inPassword = $('#password').val();
    msgBox.text('Retrieving data....');

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "https://localhost:44364/token",
        "method": "POST",
        "headers": {
            "content-type": "application/x-www-form-urlencoded",
            "cache-control": "no-cache",
            "postman-token": "f8fa0b1a-be33-43fa-153f-cc7fcf7098a1"
        },
        "data": {
            "Username": inUsername,
            "Password": inPassword,
            "grant_type": "password"
        }
    }
    $.ajax(settings).done(function (response) {
        console.log(response);
        inToken = response.access_token;
        console.log("Token Valid");
        msgBox.text("Getting Talents...");
        getData();
    }).error(function (response) {
        //alert("Username or password is wrong!")
        
        msgBox.text('Username or password is invaild');
        $('#update').html("");
    });
});

$('#search').keyup(function () {
   
    if(inToken.length>0){
     searchField = $('#search').val();
     myExp = new RegExp(searchField, "i");
    getData();
    }
});
function getData()
{

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": urlForJson,
        "method": "GET",
        "headers": {
            "authorization": "bearer " + inToken,
            "cache-control": "no-cache",
            "postman-token": "0fc47585-a2cb-1bf5-5179-62a43e182492"
        }
    }

    $.ajax(settings).done(function (data) {
        msgBox.text("Talents Retrieved");
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            console.log(data);
            if ((val.Name.search(myExp) != -1) ||
           (val.Bio.search(myExp) != -1)) {
                output += '<li>';
                output += '<h2>' + val.Name + '</h2>';
                //get the absolute path for local image
                //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                //get the image from cloud hosting
                output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                output += '<p>' + val.Bio + '</p>';
                output += '</li>';
            }
        });
        output += '</ul>';
        $('#update').html(output);
    }).error(function () {
        alert("There is a error in retrieving data, please login again");
        
    });
}
	
	
	
	


