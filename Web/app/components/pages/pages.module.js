/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.min.js" />
(function () {
    angular.module('shop.pages', ['shop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('pages', {
            url: '/pages',
            templateUrl: '/app/components/pages/pageListView.html',
            controller: 'pageListController',
            parent: 'base'
        }).state('page_add', {
            url: '/page_add',
            templateUrl: '/app/components/pages/pageAddView.html',
            controller: 'pageAddController',
            parent: 'base'
        }).state('page_edit', {
            url: '/page_edit/:id',
            templateUrl: '/app/components/pages/pageEditView.html',
            controller: 'pageEditController',
            parent: 'base'
        });
    }
})();