/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.getProductCategories = [];
        $scope.getProductCategories = getProductCategories;
        function getProductCategories() {
            apiService.get('api/productcategory/getall', null, function (result) {
                $scope.getProductCategories = result.data;
            }, function () {
                console.log('Load productcategory fail');
            });
        }
        $scope.getProductCategories();
    }
})(angular.module('shop.product_categories'));