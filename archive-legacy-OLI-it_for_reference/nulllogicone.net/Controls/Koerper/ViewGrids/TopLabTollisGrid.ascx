<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TopLabTollisGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.TopLabTollisGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<div class="rechtsuntenrund" style="padding:10px;">
	Bewertung
<table border="0">
	<asp:Repeater id="TollisRepeater" runat="server">
		<ItemTemplate>
			<tr>
				<td>
					<uc1:TollChart id="Tollchart2" runat="server" TollWert='<%# int.Parse(DataBinder.Eval(Container.DataItem, "Toll").ToString()) %>'>
					</uc1:TollChart>
					<span style="font-size:xx-small;"><%# OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container.DataItem, "datum").ToString()) %></span>					
					<a class="stamm" style="font-size:8pt;" href='<%# OliWeb.Klassen.Helper.ScriptName() + "?sguid=" + DataBinder.Eval(Container.DataItem,"StammGuid").ToString() %>'>
					
					<%# OliEngine.OliMiddleTier.DbDirect.GiveStamm(new Guid(DataBinder.Eval(Container.DataItem,"StammGuid").ToString())) %>
					</a>
					<span class="bewertung"><%# DataBinder.Eval(Container.DataItem, "TollText") %></span>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
</div>