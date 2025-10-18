<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ChartControl.ascx.cs"
    Inherits="OliWeb.Controls.Floor.Chart.ChartControl" %>
<%@ Register TagPrefix="uc1" TagName="StammChartList" Src="StammChartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItChartList" Src="PostItChartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabChartList" Src="TopLabChartList.ascx" %>

<%--Stamm Chart--%>
<div class="Stamm" style="border-width: 3px; margin: 0; margin-top: 1em;">
    <h2>
        <asp:Label runat="server" ID="Label2" Text="<%$ Resources:SAPCT, S%> "></asp:Label></h2>
    <table id="StammTable" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td valign="top" width="50%">
                <h3>
                    <asp:Label runat="server" ID="LabelRich" meta:resourcekey="LabelRichResource1" Text="rich"></asp:Label></h3>
                <uc1:StammChartList ID="ReichsteStammChartList" runat="server"></uc1:StammChartList>
            </td>
            <td valign="top" width="50%">
                <h3>
                    <asp:Label runat="server" ID="LabelPoor" meta:resourcekey="LabelPoorResource1" Text="poor"></asp:Label></h3>
                <uc1:StammChartList ID="AermsteStammChartList" runat="server"></uc1:StammChartList>
            </td>
        </tr>
    </table>
</div>

<%--PostIt Chart--%>
<div class="PostIt" style="border-width:3px; margin:0; margin-top:1em;">
    <h2>
        <asp:Label runat="server" ID="Label1" Text="<%$ Resources:SAPCT, P%> "></asp:Label></h2>
    <table id="PostItKooKTable" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td valign="top" width="50%">
                <h3>
                    <asp:Label Text="teuerste" runat="server" meta:resourcekey="LabelResource2" /></h3>
                <uc1:PostItChartList ID="TeuerstePostItChartList" runat="server"></uc1:PostItChartList>
            </td>
            <td valign="top" width="50%">
                <h3>
                    <asp:Label Text="billigste" runat="server" meta:resourcekey="LabelResource3" /></h3>
                <uc1:PostItChartList ID="BilligstePostItChartList" runat="server"></uc1:PostItChartList>
            </td>
        </tr>
    </table>
</div>

<%--TopLab Chart--%>
<div class="TopLab" style="border-width:3px; margin:0; margin-top:1em;">
    <h2>
        <asp:Label runat="server" ID="locToplab" Text="<%$ Resources:SAPCT, T%> "></asp:Label></h2>
    <table id="TopLabTable" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td valign="top" width="50%">
                <h3>Top</h3>
                <uc1:TopLabChartList ID="TopTopLabChartList" runat="server"></uc1:TopLabChartList>
            </td>
            <td valign="top" width="50%">
                <h3>Flop</h3>
                <uc1:TopLabChartList ID="FlopTopLabChartList" runat="server"></uc1:TopLabChartList>
            </td>
        </tr>
    </table>
</div>

<%--Click Chart --%>
<div style="border:solid 3px #c3c3c3; margin-top:1em;">
<h2>Clicks</h2>
<table id="PostItHitsTable" cellspacing="1" cellpadding="1" width="100%" border="0">
    <tr>
        <td valign="top" width="50%">
            <h3>Top</h3>
            <uc1:PostItChartList ID="VielBeachtetPostItChartList" runat="server"></uc1:PostItChartList>
        </td>
        <td valign="top" width="50%">
            <h3>Flop</h3>
            <uc1:PostItChartList ID="UnBeachtetPostItChartList" runat="server"></uc1:PostItChartList>
        </td>
    </tr>
</table>
</div>
