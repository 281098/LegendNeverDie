var common = {
    init: function () {
        
        common.registerEvents();
    },
    registerEvents: function () {
        var countItem = 0;
        $('#countItem').html(countItem);
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
          
            var productId = parseInt($(this).data('id'));
            $.ajax({
                url: '/ShoppingCart/Add',
                data: {
                    productId: productId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert('Thêm sản phẩm thành công.');
                        countItem++;
                        $('#countItem').html(countItem);
                    }
                }
            });
        });
       
    },
  
}
common.init();