(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return 'Kích hoạt';
            else
                return 'Khóa';
        }
    });
    app.filter('OrderStatusFilter', function () {
        return function (input) {
            if (input == false)
                return 'Đang chờ duyệt và vận chuyển';
            else
                return 'Đã nhận hàng';
        }
    });
    app.filter('PaymentStatusFilter', function () {
        return function (input) {
            if (input == null)
                return 'Chưa thanh toán';
            else
                return 'Đã thanh toán'; //online
        }
    });
})(angular.module('LND.common'));