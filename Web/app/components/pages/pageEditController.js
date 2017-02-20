/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('pageEditController', pageEditController);

    pageEditController.$inject = ['$scope', 'apiService', '$stateParams', 'notificationService', 'commonService', '$state'];

    function pageEditController($scope, apiService, $stateParams, notificationService, commonService, $state) {
        $scope.page = [];
        function loadPageDetail() {
            apiService.get('api/page/getbyid/' + $stateParams.id, null, function (result) {
                $scope.page = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        loadPageDetail();

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.UpdatePage = UpdatePage;
        function UpdatePage() {
            apiService.put('/api/page/update', $scope.page, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được sửa.');
                $state.go('pages');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }

    }
})(angular.module('shop.pages'));