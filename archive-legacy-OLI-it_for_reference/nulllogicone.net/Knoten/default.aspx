<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Knoten._default" %>
<!DOCTYPE HTML>
<HTML>
	<HEAD>
		<title>nulllogicone.net/Knoten</title>
		<LINK href="../nulllogicone.css" type=text/css rel=stylesheet>
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<H1>Knoten</H1>
			<TABLE id="Table1" cellSpacing="1" cellPadding="3" border="0">
				<TR>
					<TD style="WIDTH: 119px" bgColor="whitesmoke"><STRONG>URIref</STRONG>
					</TD>
					<TD>
						<asp:HyperLink id="UriHyperLink" runat="server" Font-Size="Smaller" Font-Bold="True"></asp:HyperLink></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="whitesmoke"><STRONG>Name</STRONG>
					</TD>
					<TD>
						<asp:Label id="KnotenLabel" runat="server" Font-Size="120%" Font-Bold="True"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5"><STRONG>Beschreibung</STRONG></TD>
					<TD>
						<asp:Label id="BeschreibungLabel" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5">gehört zu <STRONG>Netz</STRONG></TD>
					<TD>
						<asp:HyperLink id="NetzHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5"><STRONG>OLIs</STRONG></TD>
					<TD>
						<asp:Label id="OlisLabel" runat="server"></asp:Label><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5"><STRONG>Get</STRONG></TD>
					<TD>
						<asp:Label id="GetLabel" runat="server"></asp:Label><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5"><STRONG>ILOs</STRONG></TD>
					<TD>
						<asp:Label id="IlosLabel" runat="server"></asp:Label><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5"><STRONG>Fit</STRONG></TD>
					<TD>
						<asp:Label id="FitLabel" runat="server"></asp:Label><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5">weiter<STRONG> Netz</STRONG></TD>
					<TD>
						<asp:HyperLink id="WeiterNetzHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="#f5f5f5">weiter<STRONG> Baum</STRONG></TD>
					<TD>
						<asp:HyperLink id="WeiterBaumHyperLink" runat="server" Font-Size="Smaller"></asp:HyperLink></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 119px" bgColor="whitesmoke">
						<P><STRONG>Datum</STRONG>
						</P>
					</TD>
					<TD>
						<asp:Label id="DatumLabel" runat="server"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
		<P>&nbsp;</P>
		<P>
			<uc1:FussZeile id="FussZeile1" runat="server"></uc1:FussZeile></P>
	</body>
</HTML>
