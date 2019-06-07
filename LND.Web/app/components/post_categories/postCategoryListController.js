﻿(function (app) {
    app.controller('postCategoryListController', postCategoryListController);

    postCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function postCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.postCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getPostCagories = getPostCagories;
        $scope.keyword = '';

        $scope.search = search;
        // xóa nhi?u b?n ghi:multi
        $scope.deletePostCategory = deletePostCategory;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            $ngBootbox.confirm('Bạn có chắc muốn xóa ' + listId.length + ' bản ghi.').then(function () {
                var config = {
                    params: {
                        checkedPostCategories: JSON.stringify(listId)
                    }
                }
                apiService.del('api/postcategory/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' b?n ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }
        //checkbox: selected
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.postCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.postCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("postCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        // xoá 1 b?n ghi
        function deletePostCategory(id) {
            $ngBootbox.confirm('B?n có ch?c mu?n xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/postcategory/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getPostCagories();
        }

        function getPostCagories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/postcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có b?n ghi nào du?c tìm th?y.');
                }
                $scope.postCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load postcategory failed.');
            });
        }

        $scope.getPostCagories();
    }
})(angular.module('LND.post_categories'));