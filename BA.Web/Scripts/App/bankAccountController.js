var App = angular.module('App', ['ngRoute']);

// configure our routes
App.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'BankAccounts.html',
            controller: 'bankAccountController'
        });
});
App.controller('bankAccountController', function ($scope, $http) {
    $scope.sendOperation = function (form) {
        switch (form.method)
        {
            case "balance":
                $http({
                    url: "http://localhost:1883/api/BankAccounts/" + form.account + "/balance",
                    method: "GET"
                }).then(function (xhr) {
                    if (xhr.data.Successfull)
                        $scope.message = "Account Number: " + xhr.data.AccountNumber + ",Balance: " + xhr.data.Balance + ", Currency:" + xhr.data.Currency;
                    else
                        $scope.message = xhr.data.Message;
                });
                break;
            case "deposit":
                $http({
                    url: "http://localhost:1883/api/BankAccounts/Deposit",
                    method: "PUT",                   
                    headers: { "Content-Type": "application/json;charset=UTF-8" },
                    data: {'AccountNumber':form.account,'Amount':form.amount,'Currency': form.currency}
                }).then(function (xhr) {
                    if (xhr.data.Successfull)
                        $scope.message = "The deposit has been done! Available amount for the acount number :" + xhr.data.AccountNumber + " is " + xhr.data.Balance + " " +xhr.data.Currency ;
                    else
                        $scope.message = xhr.data.Message;
                });
                break;
            case "withdraw":
                $http({
                    url: "http://localhost:1883/api/BankAccounts/Withdraw",
                    method: "DELETE",
                    headers: { "Content-Type": "application/json;charset=UTF-8" },
                    data: { 'AccountNumber': form.account, 'Amount': form.amount, 'Currency': form.currency }
                }).then(function (xhr) {
                    if (xhr.data.Successfull)
                        $scope.message = "The withdraw has been done! Account number :" + xhr.data.AccountNumber + ",Amount " + xhr.data.Balance + "," + xhr.data.Currency;
                    else
                        $scope.message = xhr.data.Message;
                });
                break;
        }        
    }
    $scope.changeOperation = function () {
        $scope.disabledAmount = ($scope.bankAccountModel.method == "balance");
        $scope.disableCurrency = ($scope.bankAccountModel.method == "balance");
    }
});