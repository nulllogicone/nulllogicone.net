<%@ Register TagPrefix="uc1" TagName="AnglerOrgan" Src="../../Controls/Koerper/Organ/AnglerOrgan.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AnglerKoerper.ascx.cs" Inherits="OliWeb.Controls.Koerper.AnglerKoerper" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../Wortraum/WortraumController.ascx" %>
<table class="rechtsobenrund" id="AnglerKoerperTable" cellspacing="1" cellpadding="10"
	width="600" border="0">
	<tr class="linksobenrund">
		<td valign="top">
			<asp:hyperlink id="ExitHyperLink" runat="server" navigateurl="~/Sites/StammSite.aspx?cmd=exitA"
				tooltip="Angler schliessen" cssclass="ExitButton">&nbsp;</asp:hyperlink>
			<asp:hyperlink id="EditHyperLink" runat="server" tooltip="Angler editieren" navigateurl="~/Sites/Edit/AnglerEdit.aspx"
				cssclass="EditButton" visible="False">&nbsp;</asp:hyperlink>
			<div>
				<asp:label id="QLabel" tooltip="Sprachabstraktion" runat="server" cssclass="q" font-size="XX-Small"></asp:label></A>
			</div>
			<span style="FLOAT: left">
				<uc1:anglerorgan id="AnglerOrgan1" runat="server"></uc1:anglerorgan></span><br>
			<asp:ImageButton id="RdfImageButton" runat="server" ImageUrl="~/images/rdf.JPG"></asp:ImageButton>
			<asp:HyperLink id="RdfHyperLink" runat="server" Font-Size="XX-Small"></asp:HyperLink>
		</td>
	</tr>
</table>
