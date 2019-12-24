// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('LND.orders', ['LND.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('orders', {
            url: "/orders",
            templateUrl: "/app/components/orders/orderListView.html",
            parent: 'base',
            controller: "orderListController"
        });
           
    }
})();