﻿/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.min.js" />
(function () {
    angular.module('shop', ['shop.products', 'shop.product_categories', 'shop.common']).config(config);
    
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('base', {
            url: '',
            templateUrl: '/app/shared/views/baseView.html',
            abstract: true
        }).state('login', {
            url: '/login',
            templateUrl: '/app/components/login/loginView.html',
            controller: 'loginController'
        }).state('home', {
            url: '/admin',
            templateUrl: '/app/components/home/homeView.html',
            controller: 'homeController',
            parent:'base'
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();