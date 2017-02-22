/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('pageAddController', pageAddController);

    pageAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function pageAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.AddPage = AddPage;
        function AddPage() {
            apiService.post('api/page/create', $scope.page, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('pages');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '300px'
        }
    }
})(angular.module('shop.pages'));