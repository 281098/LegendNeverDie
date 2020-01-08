(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.orders = [];
        $scope.status = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getOrders = getOrders;
        $scope.keyword = '';
        $scope.deleteOrder = deleteOrder;
        $scope.search = search;
        $scope.transferOrder = transferOrder;
        $scope.completeOrder = completeOrder;
       

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
        function transferOrder(id) {
            $ngBootbox.confirm('Bạn có chắc muốn duyệt đơn hàng này?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/order/transfer', config, function () {
                    notificationService.displaySuccess('Đã duyệt');
                    //$('#btntransferOrder').css("display", "none");

                    search();

                }, function () {
                    notificationService.displayError('Duyệt đơn hàng thất bại');
                })
            });
        }
        function completeOrder(id) {
            $ngBootbox.confirm('Xác nhận đơn hàng đã thanh toán').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/order/complete', config, function () {
                    notificationService.displaySuccess('Đơn hàng đã được thanh toán');
                   
                    // search();
                    getOrders();
                }, function () {
                    notificationService.displayError('Thất bại');
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
                $.each($scope.orders, function (i, item) {
                    if (item.OrderStatus == "Đơn hàng đã được duyệt, đang trong quá trình vận chuyển.") {
                        $('#btntransferOrder').css("display", "none");
                    }
                    if (item.OrderStatus === "Đã nhận hàng") {
                        
                        $('#btncompleteOrder').css("display", "none");
                        console.log("Đã nhận hàng");
                    }


                });
            }, function () {
                console.log('Load order failed.');
            });
        }

        $scope.getOrders();
           
    }
})(angular.module('LND.orders'));