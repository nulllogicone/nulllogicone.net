<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AnglerOrgan.ascx.cs" Inherits="OliWeb.Controls.Koerper.Organ.AnglerOrgan" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<span class="AnglerOrgan">
	<asp:Panel id="Panel1" runat="server" CssClass="Angler" height="30px" width="535">
		<table cellSpacing="1" cellPadding="1" width="535" border="0">
			<TR>
				<TD>
					<h2><asp:Label id="AnglerLabel" CssClass="Angler" runat="server" Font-Size="Larger" Font-Bold="True">Angler</asp:Label>
					</h2><asp:Label id="BeschreibungLabel" runat="server" Font-Size="Smaller" Font-Italic="True"></asp:Label></TD>
				<TD vAlign="top" align="right" width="10%"></TD>
			</TR>
		</table>
	</asp:Panel>
	<br>
</span>
