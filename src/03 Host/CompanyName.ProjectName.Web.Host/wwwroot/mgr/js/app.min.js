$.loading = function (bool, options) {
    var defaults = {
        target: 'body',
        boxed: true,
        message: '加载中',
        zIndex: 29999999
    }
    $.extend(defaults, options);
    if (bool) {
        top.App.blockUI(defaults);
    } else {
        top.App.unblockUI(defaults.target);
    }
}
var rowStyle = function (row, index) {
    var classes = ['success', 'info'];
    if (index % 2 === 0) {//偶数行
        return { classes: classes[0] };
    } else {//奇数行
        return { classes: classes[1] };
    }
}


//兼容各种插件中loading 
loading = function (bool, options) {
    var defaults = {
        target: 'body',
        boxed: true,
        message: '加载中',
        zIndex: 29999999
    }
    $.extend(defaults, options);
    if (bool) {
        top.App.blockUI(defaults);
    } else {
        top.App.unblockUI(defaults.target);
    }
}

tabiframeId = function () {
    var iframeId = top.$(".tab_iframe:visible").attr("id");
    return iframeId;
};



$.fn.authorizeButton = function () {
    var $element = $(this);
    $element.find("a.btn").attr("authorize", "no");
    $element.find("ul.dropdown-menu").find("li").attr("authorize", "no");
    var moduleId = tabiframeId().substring(11);

    $.getJSON("/SysMgr/PermissionUserMgr/GetUserPermissionButtonsByModuleId", { moduleId: moduleId }, function (data) {
        if (data) {
            $.each(data, function(i) {
                $element.find("." + data[i].EnCode).attr("authorize", "yes");
            });
        }
        $element.find("[authorize=no]").remove();
       
    });
};
$.fn.authorizeColModel = function () {
    var $element = $(this);
    var columnModel = $element.jqGrid("getGridParam", "colModel");
    $.each(columnModel, function (i) {
        if (columnModel[i].name != "rn") {
            $element.hideCol(columnModel[i].name);
        }
    });
    var moduleId = tabiframeId().substr(6);
    var data = top.authorizeColumnData[moduleId];
    if (data != undefined) {
        $.each(data, function (i) {
            $element.showCol(data[i].F_EnCode);
        });
    }
};


//http://www.layui.com/doc/modules/layer.html


$.fn.modalOpen = function (options) {


   

    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        // btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null,
        allowOverParent: false
    };
    var options = $.extend(defaults, options);
    var _width = options.width, _height = options.height;
    if (!options.allowOverParent) {
        _width = top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
        _height = top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';
    }

    top.layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        yes: function (index) {
            options.callBack("layui-layer-iframe" + index)
        },
        cancel: function (index) {
            if (options.cancelCallBack) {
                options.cancelCallBack("layui-layer-iframe" + index);
            }
            return true;
        }
    });
};

$.fn.modalConfirm = function (content, callBack) {
    top.layer.confirm(content, {
        icon: 3,
        title: "系统提示",
        btn: ['确认', '取消'],
        //  btnclass: ['btn btn-primary', 'btn btn-danger'],
    }, function () {
        callBack(true);
    }, function () {
        callBack(false)
    });
};
$.fn.modalAlert = function (content, type) {
    var icon = "";
    var iconType = 0;
    if (type == 'success') {
        icon = "fa-check-circle";
        iconType = 1;
    }
    if (type == 'error') {
        icon = "fa-times-circle";
        iconType = 2;
    }
    if (type == 'warning') {
        icon = "fa-exclamation-circle";
        iconType = 3;
    }
    top.layer.alert(content, {
        icon: iconType,
        title: "系统提示",
        btn: ['确认'],
        //  btnclass: ['btn btn-primary'],
    });
};
$.fn.modalMsg = function (content, type) {
    var iconType = 0;
    if (type != undefined) {
        var icon = "";
        if (type == 'success') {
            icon = "fa-check-circle";
            iconType = 1;
        }
        if (type == 'error') {
            icon = "fa-times-circle";
            iconType = 2;
        }
        if (type == 'warning') {
            icon = "fa-exclamation-circle";
            iconType = 3;
        }
        top.layer.msg(content, { icon: iconType, time: 4000, shift: 5 });
        $(".layui-layer-msg").find('i.' + iconType).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else {
        top.layer.msg(content);
    }
};
$.fn.modalClose = function () {
    var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
    var IsClose = $IsdialogClose.is(":checked");
    if ($IsdialogClose.length == 0) {
        IsClose = true;
    }
    if (IsClose) {
        top.layer.close(index);
    } else {
        location.reload();
    }
};

$.fn.submitForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        success: null,
        close: true,
        // loading 参数
        message: '正在提交数据...'
    };
    var options = $.extend(defaults, options);
   $.loading(true, options);
    window.setTimeout(function () {
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            success: function (data) {
            
                if (data.code == 0) {
                    options.success(data);
                    $.fn.modalMsg(data.msg, "success");
                    if (options.close == true) {
                        $.fn.modalClose();
                    }
                } else {
                    $.fn.modalAlert(data.msg, "danger");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
         
                $.fn.modalMsg(errorThrown, "error");
            },
            //beforeSend: function () {
            //    $.loading(true, options);
            //},
            complete: function () {
             $.loading(false, options);
            }
        });
    }, 500);
}
//提交ajax请求
$.fn.submitAjax = function (options) {
    $.loading(true, options);
    window.setTimeout(function () {
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            success: function (data) {
                if (data.code == 0) {
                    options.success(data);
                } else {
                    $.fn.modalAlert(data.msg, "danger");
                }
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                $.loading(false, options);
                $.fn.modalMsg(errorThrown, "error");
            },
            //beforeSend: function () {
            //    App.unblockUI(options.target);
            //},
            complete: function () {
                //App.unblockUI(options.target);
                $.loading(false, options);
            }
        });
    }, 500);
};

$.fn.deleteForm = function (options) {
    var defaults = {
        prompt: "注：您确定要删除该项数据吗？",
        url: "",
        param: [],
        success: null,
        close: true,
        message: '正在提交数据...'
    };
    var options = $.extend(defaults, options);

    $.fn.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                       
                        if (data.code == 0) {
                            options.success(data);
                            $.fn.modalMsg(data.msg, "success");
                        } else {
                            $.fn.modalAlert(data.msg, "danger");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.loading(false, options);
                        $.fn.modalMsg(errorThrown, "error");
                    },
                    //beforeSend: function () {
                    //    $.loading(true, options);
                    //},
                    complete: function () {
                        $.loading(false, options);
                    }
                });
            }, 500);
        }
    });
};

$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}

reload = function () {
    location.reload();
    return false;
};

changeUrlParam = function (url, key, value) {
    var newUrl = "";
    var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)");
    var tmp = key + "=" + value;
    if (url.match(reg) != null) {
        newUrl = url.replace(eval(reg), tmp);
    } else {
        if (url.match("[\?]")) {
            newUrl = url + "&" + tmp;
        } else {
            newUrl = url + "?" + tmp;
        }
    }
    return newUrl;
};
$.currentIframe = function () {
    if ($.isbrowsername() == "Chrome" || $.isbrowsername() == "FF") {
        return top.frames[tabiframeId()].contentWindow;
    } else {
        return top.frames[tabiframeId()];
    }
};

$.isbrowsername = function () {
    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
    var isOpera = userAgent.indexOf("Opera") > -1;
    if (isOpera) {
        return "Opera";
    }; //判断是否Opera浏览器
    if (userAgent.indexOf("Firefox") > -1) {
        return "FF";
    } //判断是否Firefox浏览器
    if (userAgent.indexOf("Chrome") > -1) {
        if (window.navigator.webkitPersistentStorage.toString().indexOf("DeprecatedStorageQuota") > -1) {
            return "Chrome";
        } else {
            return "360";
        }
    } //判断是否Chrome浏览器//360浏览器
    if (userAgent.indexOf("Safari") > -1) {
        return "Safari";
    } //判断是否Safari浏览器
    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
        return "IE";
    }; //判断是否IE浏览器
};

$.download = function (url, data, method) {
    if (url && data) {
        data = typeof data == "string" ? data : jQuery.param(data);
        var inputs = "";
        $.each(data.split("&"), function () {
            var pair = this.split("=");
            inputs += "<input type=\"hidden\" name=\"" + pair[0] + "\" value=\"" + pair[1] + "\" />";
        });
        $("<form action=\"" + url + "\" method=\"" + (method || "post") + "\">" + inputs + "</form>").appendTo("body").submit().remove();
    };
};

$.standTabchange = function (object, forid) {
    $(".standtabactived").removeClass("standtabactived");
    $(object).addClass("standtabactived");
    $(".standtab-pane").css("display", "none");
    $("#" + forid).css("display", "block");
};

$.windowWidth = function () {
    return $(window).width();
};
$.windowHeight = function () {
    return $(window).height();
};

getQueryString=function(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

$.fn.formSerialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            if ($id.length == 0)
                $id = element.find('input[name="' + key + '"]');
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value == "true") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "radio":
                    $id.val(value);
                  
                    //if (value == "true") {
                    //    $id.attr("checked", 'checked');
                    //} else {
                    //    $id.removeAttr("checked");
                    //}
                    //$id.trigger("click")
                    break;
                case "select":
                    $id.val(value).trigger("change");
                    break;
                default:
                    $id.val(value);
                    break;
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,textarea').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            case "radio":
                var name = $this.attr('name');
                postdata[name] = element.find("input[name='" + name + "']").val(); 
                break;
            default:
                var value = $this.val() == "" ? "&nbsp;" : $this.val();
                if (!$.request("keyValue")) {
                    value = value.replace(/&nbsp;/g, '');
                }
                postdata[id] = value;
                break;
        }
    });
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};

$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        url: "",
        param: [],
        change: null,
        data:null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            success: function (data) {
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                });
                $element.select2({
                    minimumResultsForSearch: options.search == true ? 0 : -1
                });
                $element.on("change", function (e) {
                    if (options.change != null) {
                        options.change(data[$(this).find("option:selected").index()]);
                    }
                    $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
                });
            }
        });
    } else if (options.data!=null) {
        $.each(options.data, function (i) {
            $element.append($("<option></option>").val(options.data[i][options.id]).html(options.data[i][options.text]));
        });
        $element.select2({
            minimumResultsForSearch: options.search == true ? 0 : -1
        });
        $element.on("change", function (e) {
            if (options.change != null) {
                options.change(data[$(this).find("option:selected").index()]);
            }
            $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
        });
    }else {
        $element.select2({
            minimumResultsForSearch: -1
        });
    }
}

//处理全屏
var handleFullScreen = function () {
    var de = document.documentElement;

    if (de.requestFullscreen) {
        de.requestFullscreen();
    } else if (de.mozRequestFullScreen) {
        de.mozRequestFullScreen();
    } else if (de.webkitRequestFullScreen) {
        de.webkitRequestFullScreen();
    } else if (de.msRequestFullscreen) {
        de.msRequestFullscreen();
    }
    else {
        // App.alert({ message: "该浏览器不支持全屏！", type: "danger" });
        alert("当前浏览器不支持全屏！");
    }

};