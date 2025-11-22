<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="WortraumController.ascx" %>
<%@ Page language="c#" Codebehind="DoppelMatch.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Controls.Wortraum.DoppelMatch" %>
<!DOCTYPE HTML>
<html>
	<head>
		<title>DoppelMatch</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">		
		<link href="../../OliWeb.css" type="text/css" rel="stylesheet">
	</head>
	<body>
		<form id="DoppelMatch" method="post" runat="server">
			<table id="DoppelMatchTable" cellspacing="1" cellpadding="1" width="100%" border="1">
				<tr>
					<td valign="top" align="left" width="50%">
						<asp:label id="MatchLabel" runat="server">match</asp:label></td>
					<td valign="top" align="right"></td>
				</tr>
				<tr>
					<td valign="top" align="left" width="50%">
						<uc1:wortraumcontroller id="CodeWortraumController" runat="server"></uc1:wortraumcontroller></td>
					<td valign="top" align="right">
						<uc1:wortraumcontroller id="AnglerWortraumController" runat="server"></uc1:wortraumcontroller></td>
				</tr>
			</table>
		</form>
		<!-- (c) Frederic Luchting -->
	</body>
</html>
