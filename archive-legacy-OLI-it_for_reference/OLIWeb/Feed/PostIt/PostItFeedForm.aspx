<%@ Page language="c#" Codebehind="PostItFeedForm.aspx.cs" AutoEventWireup="false" Inherits="OliWeb.Feed.PostIt.PostItFeedForm" %>
<%@ Register TagPrefix="uc1" TagName="Repeater" Src="Repeater.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PostItFeedForm</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../OliWeb.css" type="text/css" rel="stylesheet">
		<LINK href="FeedStyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" bgColor="whitesmoke">
		<form id="PostItFeedForm" method="post" runat="server">
			<P>
				<TABLE id="Table1" borderColor="#ffffff" cellSpacing="0" cellPadding="1" width="100%" bgColor="lightgrey" border="1">
					<TR>
						<TD>StammGuid</TD>
						<TD>
							<asp:TextBox id="StammGuidTextBox" runat="server" Width="356px">B4111E0E-48D9-42C4-A6F6-EC4991264947</asp:TextBox></TD>
					</TR>
					<TR>
						<TD>AnglerGuid</TD>
						<TD>
							<asp:TextBox id="TextBox1" runat="server" Width="356px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>Anzahl</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>Sort</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>Style</TD>
						<TD>
							<asp:DropDownList id="StyleDropDownList" runat="server" AutoPostBack="True">
								<asp:ListItem Value="Repeater" Selected="True">Repeater</asp:ListItem>
								<asp:ListItem Value="Banner">Banner</asp:ListItem>
								<asp:ListItem Value="News">News</asp:ListItem>
								<asp:ListItem Value="AddWords">AddWords</asp:ListItem>
								<asp:ListItem Value="PageLets">PageLets</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD align="right">&nbsp;
							<asp:Button id="ShowButton" runat="server" Text="show"></asp:Button></TD>
					</TR>
				</TABLE>
				&nbsp;</P>
			<P>
				<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder></P>
		</form>
	</body>
</HTML>
