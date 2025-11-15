<%@ Page language="c#" Codebehind="WortraumEdit.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Controls.Wortraum.WortraumEdit" smartNavigation="True" %>
<%@ Register TagPrefix="uc2" TagName="WortraumController" Src="WortraumController.ascx" %>
<!DOCTYPE HTML>
<HTML>
	<HEAD>
		<title>WortraumEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">		
		<LINK href="OliWeb.css" type="text/css" rel="stylesheet">
		<LINK href="../../OliWeb.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WortraumEdit" method="post" runat="server">
			<uc2:WortraumController id="WortraumController1" runat="server" ShowEdit="true"></uc2:WortraumController>
		</form>
	</body>
</HTML>
