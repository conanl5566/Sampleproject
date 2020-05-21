//$(function () {
//    var dataJson = [
//    ];
//    $.ajax({
//        url: "/SysUser/GetClientsDataJson",
//        type: "get",
//        dataType: "json",
//        async: false,
//        success: function (r) {
//            if (r.code == 0) {
//            }
//            else
//                    $('[authorize=yes]').hide();
//                    if (r.data != null) {
//                        dataJson = r.data;
//                        if (dataJson != undefined) {
//                            $.each(dataJson, function (i) {
//                                $("#authorize" + dataJson[i].itemId).show();
//                            });
//                        }
//                    }
//        }
//    });
//});