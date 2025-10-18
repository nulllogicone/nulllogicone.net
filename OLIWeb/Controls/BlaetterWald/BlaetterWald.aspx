<%@ Register TagPrefix="uc1" TagName="BaumControl" Src="BaumControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NetzControl" Src="NetzControl.ascx" %>
<%@ Page language="c#" Codebehind="BlaetterWald.aspx.cs" AutoEventWireup="false" Inherits="OliWeb.Controls.BlaetterWald.BlaetterWald" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BlaetterWald</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../OliWeb.css" type="text/css" rel="stylesheet">
		<LINK href="BlaetterWald.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="BlaetterWald" method="post" runat="server">
			<P>
				<TABLE id="Table1" cellSpacing="1" cellPadding="10" width="100%" border="0">
					<TR>
						<TD vAlign="top" align="left" width="60%">
							<P>
								<uc1:NetzControl id="NetzControl1" runat="server"></uc1:NetzControl></P>
						</TD>
						<TD vAlign="top">
							<P>
								<uc1:BaumControl id="BaumControl1" runat="server"></uc1:BaumControl></P>
						</TD>
					</TR>
				</TABLE>
			</P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
