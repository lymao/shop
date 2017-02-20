/// <reference path="D:\CSharp\shop\Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('pageListController', pageListController);

    pageListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function pageListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.pages = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.search = search;

        $scope.keyword = '';
        function search() {
            $scope.getPages();
        }

        $scope.getPages = getPages;
        function getPages(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page
                    //pageSize: 4
                }
            }
            apiService.get('/api/page/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                else {
                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                }
                $scope.pages = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log("Load data failed");
            });
        }
        $scope.getPages();

        $scope.deletePage = deletePage;
        function deletePage(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa không?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/page/delete', config, function () {
                    notificationService.displaySuccess('Đã xóa thành công.');
                    $scope.getPages();
                }, function () {
                    notificationService.displayError('Xóa thất bại.');
                });
            });

        }

        //Xóa nhiều bản ghi
        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedPages: JSON.stringify(listId)
                }
            }
            apiService.del('api/page/deletemulti', config, function (result) {
                notificationService.displaySuccess('Đã xóa ' + result.data + ' bản ghi.');
                $scope.getPages();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        // hàm logic: đầu tiên isAll= true, khi click vào hàm selectAll ở View kiểm tra đk nếu isAll=true thì lăp qua productCategories và gán cho ng-model=item.checked
        // ở ngoài View là true, ngược lại isAll=false
        $scope.selectAll = selectAll;
        $scope.isAll = true;
        function selectAll() {
            if ($scope.isAll === true) {
                angular.forEach($scope.pages, function (item) {
                    item.checked = true;
                });
                $scope.isAll = false;
            } else {
                angular.forEach($scope.pages, function (item) {
                    item.checked = false;
                });
                $scope.isAll = true;
            }
        }

        //phương thức lắng nghe khi ta check vào checkbox
        $scope.$watch("pages", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

    }
})(angular.module('shop.pages'));