/*****************************************************************************
 *
 * @author 
 * 
 * @requires jQuery HSCP 
 * 
 * @description   对plupload做的简单封装，前台使用data-xx属性传递参数
 *
 *****************************************************************/
(function ($, window) {

    $.fn.imagePlupload = function (options) {
       // alert(1332323);
        //参数
         var defaults = {  
             //uploadform: "#imagePluploadForm",//根据id或class绑定的表单验证
             uploadstyle: 0,//上传的样式（0：有上传按钮可显示多张；1：上传一张图片按钮）
             //original: 0,//是否保存原图（0：否;1：是）
             iswidthheight: 0,// 是否显示宽高（0：否;1：是）
             //callbackimg: 0,// 上传成功后，回调的图片路径数组
             upvalidate: 0,// 是否传验证（0：否; 其他：$(id或class).validate()）根据validate表单验证
             savesize: "[]"//上传图片后的大小,使用格式为：[宽x高-宽x高-宽x高...]注：宽高中间用小写"x"隔开，请从小到大排列，请和后台设置一致
         }
        var options = $.extend(defaults, options);
        //var upForm = options.uploadform;
        var upStyle = options.uploadstyle;
        //var upOriginal = options.original;
        var upIsWidthHeight = options.iswidthheight;
        //var upCallbackimg = options.callbackimg;
        var upSaveSize = options.savesize;
        var upValidate = options.upvalidate;
        var validator;
        //验证
        if (upValidate != 0) {
            validator = upValidate;;
        }
        var up = this;
        var upType = $(up).attr("data-type");
        var imgDiv =$(up).attr("data-imgdiv");
        var upId = $(up).attr("id");
        var field = $(up).attr("data-field");
        var imgWidth = $(up).width();
        var imgHeight = $(up).height();
        // console.log(upType, imgDiv, upId)
        //console.log(imgWidth, imgHeight)
        if (upStyle == 0) {
         //   alert(1);
        var uploader = new plupload.Uploader({
            runtimes: 'html5,flash,silverlight,html4',
            multi_selection: false,
            browse_button: upId,
            //url: '/Image/Img?type=' + upType + '&original=' + upOriginal + '&savesize=' + upSaveSize,
            url: '/Image/Img?savesize=' +upSaveSize,
            flash_swf_url: '/js/plupload-2.1.2/js/Moxie.swf',
            silverlight_xap_url: '/js/js/Moxie.xap',
            filters: {
                max_file_size: '500kb',
                mime_types: [
                    { title: "图片", extensions: "jpg" }
                ]
            },
            init: {
                PostInit: function () {

                },
                FilesAdded: function (up, files) {
                    plupload.each(files, function (file) {
                        $(imgDiv).html("<div class='ing' style='background-color:blue;height:30px; width:1px'></div>");
                        up.start();
                    });
                },
                BeforeUpload: function (up, file) {

                },
                UploadProgress: function (up, file) {
                //    alert(2);
                    $(imgDiv).find(".ing").css("width", file.percent + "%");
                },
                FileUploaded: function (up, file, rep) {
                    //console.log(up, file, rep)
                  //  alert(rep.response);
                    var d = $.parseJSON(rep.response)
                    if (d.error != undefined)
                        $(imgDiv).html("上传失败" + d.error.message);
                    else {
                        var imgData = d.imgthum;
                        var sizeArray = upSaveSize.replace("[", "").replace("]", "").split('-');
                        //console.log(sizeArray, imgData)
                        var imgHtml = "";
                        var imgHtmlCol1 = "";
                        var imgHtmlCol2 = "";
                        for (var i = 0; i < sizeArray.length; i++) {
                            var wh = sizeArray[i].split('x');
                            if (wh.length == 2) {
                                //imgHtmlCol1 += "<td style='text-align: center;padding-right:20px'><img src='" + imgData + "'style='width:" + wh[0] + "px; height:" + wh[1] + "px;' /></td>";
                                //imgHtmlCol2 += "<td style='text-align: center;padding-right:20px'>图片大小：" + sizeArray[i] + "</td>";
                                imgHtmlCol1 += "<td style='text-align: center;padding-right:20px'><img src='" + imgData + "'style='width:100px; height:100px;' /></td>";
                                imgHtmlCol2 += "<td style='text-align: center;padding-right:20px'>图片大小：" + sizeArray[i] + "</td>";


                            }
                        }
                        imgHtml = "<table><tr>" + imgHtmlCol1 + "</tr><tr>" + imgHtmlCol2 + "</tr></table>";
                        if (upIsWidthHeight == 0) {
                             imgHtml = "<table><tr>" +imgHtmlCol1 + "</tr></table>";
                        }
                      //  alert(imgHtml);
                     //   alert(d.result);
                        $(imgDiv).html(imgHtml);
                        $("#" + field).val(d.result)
                        //验证
                        if (upValidate != 0) {
                            validator.element($("#" + field))
                        }
                       
                    }
                },
                Error: function (up, err) {
                    alert(err.message);
                }
            }
        });

        uploader.init();
        }
        if (upStyle == 1) {
          //  alert(3);
            var uploader = new plupload.Uploader({
                runtimes: 'html5,flash,silverlight,html4',
                multi_selection: false,
                browse_button: upId,
                    url: '/Image/Img?savesize=' + upSaveSize,
                    flash_swf_url: '/js/plupload-2.1.2/js/Moxie.swf',
                    silverlight_xap_url: '/js/plupload-2.1.2/js/Moxie.xap',
                filters: {
                    max_file_size: '500kb',
                    mime_types: [
                        { title: "图片", extensions: "jpg" }
                    ]
                },
                init: {
                    PostInit: function () {

                    },
                    FilesAdded: function (up, files) {
                        plupload.each(files, function (file) {
                            $(imgDiv).html("<div class='ing' style='background-color:blue;height:30px; width:1px'></div>");
                            up.start();
                        });
                    },
                    BeforeUpload: function (up, file) {

                    },
                    UploadProgress: function (up, file) {
                        $(imgDiv).find(".ing").css("width", file.percent + "%");
                    },
                    FileUploaded: function (up, file, rep) {
                        //  console.log(up, file, rep)
                     //   alert(1111);
                        var d = $.parseJSON(rep.response)
                        if (d.error != undefined)
                            $(imgDiv).html("上传失败" + d.error.message);
                        else {
                            var imgData = d.imgthum;
                            //var imgHtml = "<img style='width:" + imgWidth + "px; height: " + imgHeight + "px' src='" + imgData + "' />";

                            var imgHtml = "<img style='width:100px; height: 100px' src='" + imgData + "' />";

                            $(imgDiv).html(imgHtml);
                            $("#" + field).val(d.result)
                            $("#" + field).blur();
                          //  console.log($("#" + field).val())
                             //验证
                        if (upValidate != 0) {
                            validator.element($("#" +field))
                            }
                        }
                    },
                    Error: function (up, err) {
                        alert(err.message);
                    }
                }
            });

            uploader.init();
        }
    }
})(jQuery, window);
