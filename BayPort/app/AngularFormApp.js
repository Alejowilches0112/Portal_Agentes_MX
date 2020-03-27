var app = angular.module("loginApp", ["ngRoute", "ngResource"])

    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', {
            templateUrl: '/app/Templates/login.html',
            controller: 'loginCtrl'
        }).otherwise({
            redirectTo:'/'

        });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });



});