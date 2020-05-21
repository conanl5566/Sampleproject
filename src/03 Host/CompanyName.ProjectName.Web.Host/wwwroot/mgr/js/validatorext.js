//// 电话号码验证 
//    $.validator.addMethod("phone", function (value, element) {
//    var tel = /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/;
//    return this.optional(element) || (tel.test(value));
//}, "电话号码格式错误");
//    $.validator.addMethod("mobile", function (value, element) {
//    var length = value.length;
//    var mobile = /^1\d{10}$/;
//    return this.optional(element) || (length == 11 && mobile.test(value));
//}, "手机号码格式错误");

//    // 联系电话(手机/电话皆可)验证 
//    $.validator.addMethod("isPhone", function (value, element) {
//        var length = value.length;
//        var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
//        var tel = /^\d{3,4}-?\d{7,9}$/;
//        return this.optional(element) || (tel.test(value) || mobile.test(value));

//    }, "请正确填写您的联系电话"); 


//$.fn.formValid = function (option) {
//    $.extend($.validator.messages, {
//        isPhone: "请正确填写您的联系电话",
//        phone: "电话号码格式错误",
//        mobile: "手机号码格式错误",
//        required: "必填字段",
//        remote: "请修正此字段",
//        email: "请输入有效的电子邮件地址",
//        url: "请输入有效的网址",
//        date: "请输入有效的日期",
//        dateISO: "请输入有效的日期 (YYYY-MM-DD)",
//        number: "请输入有效的数字",
//        digits: "只能输入数字",
//        creditcard: "请输入有效的信用卡号码",
//        equalTo: "你的输入不相同",
//        extension: "请输入有效的后缀",
//        maxlength: $.validator.format("最多可以输入{0}个字符"),
//        minlength: $.validator.format("最少要输入{0}个字符"),
//        rangelength: $.validator.format("请输入长度在{0}到{1}之间的字符串"),
//        range: $.validator.format("请输入范围在{0}到{1}之间的数值"),
//        max: $.validator.format("请输入不大于{0}的数值"),
//        min: $.validator.format("请输入不小于{0}的数值")
//    });

 

//    var curoption = {
//        errorPlacement: function (error, element) {
//            element.parents('.formValue').addClass('has-error');
//            element.parents('.has-error').find('i.error').remove();
//            element.parents('.has-error').append('<i class="fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error.html() + '"></i>');
//            if (element.parents('.input-group').hasClass('input-group')) {
//                element.parents('.has-error').find('i.error').css('right', '33px')
//            }
//         $("[data-toggle='tooltip']").tooltip();
//        },
//        success: function (element) {
//            var id = element.attr("id").replace("-error", "");
//            var pelement = $("#" + id);
//            var pe = pelement.parents('.input-group');
//            if (pe.length > 0) {
//                pe.parents('.formValue').removeClass('has-error').find('i.error').remove();
//            }
//            else
//                pelement.parent('.formValue').removeClass('has-error').find('i.error').remove();
//        }
//    };
//    $.extend(curoption, option)
//    $(this).validate(curoption);
//}
