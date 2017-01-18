/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.min.js" />
(function () {
    angular.module('shop', ['shop.products', 'shop.product_categories', 'shop.common']).config(config);
    
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: '/admin',
            templateUrl: '/app/components/home/homeView.html',
            controller: 'homeController'
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();