(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.orders = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getOrders = getOrders;
        $scope.keyword = '';
        $scope.deleteOrder = deleteOrder;
        $scope.search = search;
   
        function search() {
            getOrders();
        }
        function deleteOrder(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/order/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }
        function getOrders(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/order/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.orders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getOrders();
    }
})(angular.module('LND.orders'));