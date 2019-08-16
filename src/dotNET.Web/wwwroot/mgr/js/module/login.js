(function ($) {
    $.login = {
        formMessage: function (msg) {
            $('.login_tips').find('.tips_msg').remove();
            $('.login_tips').append('<div class="tips_msg"><i class="fa fa-question-circle"></i>' + msg + '</div>');
        },
        loginClick: function () {
            var $username = $("#txt_account");
            var $password = $("#txt_password");
            var $code = $("#txt_code");
            if ($username.val() == "") {
                $username.focus();
                $.login.formMessage('请输入登录帐号。');
                return false;
            } else if ($password.val() == "") {
                $password.focus();
                $.login.formMessage('请输入登录密码。');
                return false;
            } else if ($code.val() == "") {
                $code.focus();
                $.login.formMessage('请输入验证码。');
                return false;
            } else {
                $("#login_button").attr('disabled', 'disabled').html("loading...");
                $.ajax({
                    url: "/Account/Login",
                    data: { Account: $.trim($username.val()), Password: $.md5($.trim($password.val())), Code: $.trim($code.val()), __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() },
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        if (data.state == "success") {
                            $("#login_button").html("登录成功，正在跳转...");
                            window.setTimeout(function () {
                                window.location.href = data.returnUrl;
                            }, 500);
                        } else {
                            $("#login_button").removeAttr('disabled').html("登录");
                            //$("#switchCode").trigger("click");
                            //$code.val('');
                            $.login.formMessage(data.message);
                        }
                    }
                });
            }
        },
        init: function () {
            $('.wrapper').height($(window).height());
            $(".container").css("margin-top", ($(window).height() - $(".container").height()) / 2 - 50);
            $(window).resize(function (e) {
                $('.wrapper').height($(window).height());
                $(".container").css("margin-top", ($(window).height() - $(".container").height()) / 2 - 50);
            });
            //$("#switchCode").click(function () {
            //    $("#imgcode").attr("src", "/Account/GetAuthCode?time=" + Math.random());
            //});
            $("#login_button").click(function () {
                $.login.loginClick();
            });
            document.onkeydown = function (e) {
                if (!e) e = window.event;
                if ((e.keyCode || e.which) == 13) {
                    document.getElementById("login_button").focus();
                    document.getElementById("login_button").click();
                }
            }
        }
    };
    $(function () {
        $.login.init();
        
        if (top.location != self.location)
            top.location = self.location;
    });
})(jQuery);