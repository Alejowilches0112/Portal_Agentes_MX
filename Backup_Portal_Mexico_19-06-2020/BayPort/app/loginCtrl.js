app.controller('loginCtrl', ['$scope','loginFactory',
    function ($scope, loginFactory) {

    $scope.invalido = false;
    $scope.cargando = false;
    $scope.mensaje = "";
    
    $scope.data = {};

    $scope.ingresar = function (data) {
        console.log(data);
        if (data.UserName.length < 2) {
            $scope.invalido = true;
            $scope.mensaje = "Usuario/Password Incorrecto";
            return;
        } else if (data.Password.length < 2) {
            $scope.invalido = true;
            $scope.mensaje = "Usuario/Password Incorrecto";
            return;
        }

        $scope.invalido = false;
        $scope.cargando = true;

        loginFactory.login(data, function(response){
            if (data.statusText !== "OK") {
                $scope.$worked = false;
            } else {
                $scope.$worked = true;
            }
        


        });
    


    };
    



}]);