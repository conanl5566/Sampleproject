<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Fcity.UI.Portal.Content.ckeditor.app.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>

<body>
   <form enctype="multipart/form-data" method="POST" dir="ltr" lang="zh-cn" action="Upload.aspx?type=Images&CKEditor=editor1&CKEditorFuncNum=2&langCode=zh-cn">
　　<label id="cke_153_label" for="cke_154_fileInput_input" style="display:none">上传到服务器上</label>
　　<input id="cke_154_fileInput_input" aria-labelledby="cke_153_label" type="file" name="upload" size="38">
</form>
</body>
</html>
