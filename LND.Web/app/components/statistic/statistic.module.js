// <reference path="/Assets/admin/libs/angular/angular.js" />

(function() {
    angular.module('LND.statistics', ['LND.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('statistic_revenue', {
            url: "/statistic_revenue",
            templateUrl: "/app/components/statistic/revenueStatisticView.html",
            parent: 'base',
            controller: "revenueStatisticController"
        });

    }
})();