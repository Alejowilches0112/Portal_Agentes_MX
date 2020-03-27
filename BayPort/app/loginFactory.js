app.factory('loginFactory', ['$http', '$q', 
function ($http,$q) {
   
    var self = {
        login: function (data) {
   
            $http.post('/Home/authenticate', data).then(function (msg)  {
                console.log(msg);
                if (msg.loginSucceeded === "true") {
                    console.log("opa")
                } else {
                    console.log("den");
                }
            });

        }

};





    return self;
    
        
}]);