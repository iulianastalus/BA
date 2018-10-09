var app = angular.module("app", ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider

            // route for the home page
            .when('/', {
                templateUrl: '',
                controller: 'bankAccountController'
            })
});
app.controller("banckAccountController", function ($scope,$http) {
    alert("aaaa");
});