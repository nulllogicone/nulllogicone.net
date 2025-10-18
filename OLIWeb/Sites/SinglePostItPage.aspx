<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SinglePostItPage.aspx.cs" Inherits="OliWeb.Sites.SinglePostItPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<%--OliWeb.css --%>
    <link href="/OliWeb.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<form id="form1" runat="server" style="margin:1em;">
		<div style="background-color: azure; margin: 1em; border: solid 1px blue; font-size:small; float:right;">
			Server: <asp:Label ID="lblServerName" runat="server" /><br/>
			DateTime: <asp:Label ID="lblDateTime" runat="server" /><br />
            OLI-it: <asp:HyperLink ID="lnkPostIt" runat="server" NavigateUrl="~/PostIt.aspx" Text="//P/" />
		</div>
		<div class="PostIt" >
			<h1>
				<asp:Label ID="lblTitle" runat="server" /></h1>
			<div>
				<asp:Label ID="lblDescription" runat="server" />
			</div>
            <asp:Image ID="imgPostIt" runat="server" />
		</div>
	</form>
</body>
</html>
