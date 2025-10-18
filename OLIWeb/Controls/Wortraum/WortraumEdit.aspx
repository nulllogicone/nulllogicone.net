<%@ Page language="c#" Codebehind="WortraumEdit.aspx.cs" AutoEventWireup="false" Inherits="OliWeb.Controls.Wortraum.WortraumEdit" smartNavigation="True"%>
<%@ Register TagPrefix="uc2" TagName="WortraumController" Src="WortraumController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WortraumEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="OliWeb.css" type="text/css" rel="stylesheet">
		<LINK href="../../OliWeb.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="WortraumEdit" method="post" runat="server">
			<uc2:WortraumController id="WortraumController1" runat="server" ShowEdit="true"></uc2:WortraumController>
		</form>
	</body>
</HTML>
