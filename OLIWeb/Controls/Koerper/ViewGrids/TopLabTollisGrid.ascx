<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="TopLabTollisGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.TopLabTollisGrid" %>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<div class="rechtsuntenrund" style="padding: 10px; clear: left;" runat="server" id="DivTollis">
    <strong>Bewertung</strong>
    <asp:Repeater ID="TollisRepeater" runat="server">
        <ItemTemplate>
            <div class="bewertung todo">
                <uc1:TollChart ID="Tollchart2" runat="server" TollWert='<%# int.Parse(Eval("Toll").ToString()) %>'></uc1:TollChart>
                <span style="font-size: xx-small;">
                    <%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("datum").ToString()) %></span>
                <a runat="server" class="Stamm" style="font-size: 8pt;" href='<%# OliWeb.Klassen.Helper.ScriptName() + "?sguid=" + Eval("StammGuid").ToString() %>'>
                    <%# OliEngine.OliMiddleTier.DbDirect.GiveStamm(new Guid(Eval("StammGuid").ToString())) %>
                </a><span class="Bewertung">
                    <%# Eval("TollText") %></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
