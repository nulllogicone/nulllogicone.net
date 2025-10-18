<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CodeKoerper.ascx.cs" Inherits="OliWeb.Controls.Koerper.CodeKoerper" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../Wortraum/WortraumController.ascx" %>
<table class=rechtsobenrund id=CodeKoerperTable cellSpacing=1 cellPadding=10 width=600
	border=0>
	<tr>
		<td vAlign=top>
			<div><asp:hyperlink id=ExitHyperLink cssclass="ExitButton" navigateurl="~/Sites/PostItSite.aspx?cmd=exitC"
					tooltip="Markierung schließen" runat="server">&nbsp;</asp:hyperlink><asp:label id=QLabel cssclass="q" tooltip="Sprachabstraktion" runat="server" font-size="XX-Small"></asp:label></div>
			<font color=#003399>
				<div><asp:label id=CodeLabel runat="server" font-bold="True"></asp:label><br>
			</font>
			<asp:textbox id=KommentarTextBox runat="server" width="304px"></asp:textbox></DIV>
			<div align=center><strong>diesen Code speichern und vergleichen</strong>
				<asp:imagebutton id=FischenImageButton tooltip="nach Nachrichten filtern" runat="server" borderstyle="Outset"
					backcolor="White" imageurl="~/images/Ringe_trans.gif"></asp:imagebutton></div>
			<p>
				<asp:datagrid id="ShortCutsDataGrid" runat="server" font-size="Smaller" width="250px" borderstyle="None"
					gridlines="Horizontal" datakeyfield="ShortCutsGuid" cellpadding="0" borderwidth="1px" bordercolor="#DEBA84"
					autogeneratecolumns="False">
					<selecteditemstyle font-bold="True" forecolor="White" backcolor="FloralWhite"></selecteditemstyle>
					<edititemstyle backcolor="FloralWhite"></edititemstyle>
					<itemstyle forecolor="#8C4510"></itemstyle>
					<headerstyle font-bold="True" forecolor="White" backcolor="#A55129"></headerstyle>
					<footerstyle forecolor="#8C4510" backcolor="#F7DFB5"></footerstyle>
					<columns>
						<asp:templatecolumn headertext="Favoriten Markierungen">
							<itemtemplate>
								<asp:linkbutton id="LinkButton3" runat="server" commandname="select" forecolor="Sienna" font-bold="True">
									<%# DataBinder.Eval(Container, "DataItem.ShortCut") %>
								</asp:linkbutton>
							</itemtemplate>
							<edititemtemplate>
								<asp:TextBox id="ShortCutsTextBox" runat="server" width="200" Text='<%# DataBinder.Eval(Container, "DataItem.ShortCut") %>'>
								</asp:TextBox>
							</edititemtemplate>
						</asp:templatecolumn>
						<asp:templatecolumn>
							<itemtemplate>
								<asp:CheckBox id="CheckBox1" runat="server" Enabled="False" Checked='<%# DataBinder.Eval(Container.DataItem, "auto") %>'>
								</asp:CheckBox>
							</itemtemplate>
							<edititemtemplate>
								<div style="TEXT-ALIGN: right">
									<asp:linkbutton id="Linkbutton12" title="Diesen ShortCut in oben ausgewählten Code übernehmen" runat="server"
										cssclass="GridButton" text="copy" commandname="set"></asp:linkbutton></div>
							</edititemtemplate>
						</asp:templatecolumn>
					</columns>
					<pagerstyle horizontalalign="center" forecolor="#8C4510" mode="NumericPages"></pagerstyle>
				</asp:datagrid>
				<asp:button id="CancelButton" runat="server" visible="False" text="cancel"></asp:button></p>
			<p><asp:panel id=WortraumControllerPanel runat="server">
					<uc1:wortraumcontroller id="WortraumController1" runat="server"></uc1:wortraumcontroller>
				</asp:panel></p>
			<P>
				<asp:ImageButton id="RdfImageButton" runat="server" ImageUrl="~/images/rdf.JPG"></asp:ImageButton>&nbsp;
				<asp:HyperLink id="RdfHyperLink" runat="server" Font-Size="XX-Small"></asp:HyperLink></P>
		</td>
	</tr>
</table>
