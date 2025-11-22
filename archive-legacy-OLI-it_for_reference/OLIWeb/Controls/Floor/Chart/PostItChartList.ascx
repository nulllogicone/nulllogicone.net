<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItChartList.ascx.cs"
    Inherits="OliWeb.Controls.Floor.Chart.PostItChartList" %>
<asp:Repeater ID="PostItRepeater" runat="server">
    <ItemTemplate>
        <div>
            <div class="meta" style="margin:5px;">
                <div class="flowKooK">
                    <%# Eval("KooK","{0:f2}") %></div>
                <div class="hits">
                    <%# Eval("Hits") %></div>
            </div>
            <div class="PostIt">
                <a href='<%# "~/P/" + Eval("PostItGuid") + ".aspx" %>'>
                 <asp:Image ImageUrl='<%# OliEngine.OliUtil.MakeImageSrc(Eval("Datei").ToString()) %>' 
                 AlternateText='PostIt image' style="width:80px; margin-right:10px; float:left;" runat="server" ID="ImagePostIt" Visible='<%# Eval("Datei").ToString().Length > 0 %>' />
                    <h4>
                        <%# Eval("Titel")  %></h4>
                    <%# OliEngine.OliUtil.FirstXWords (Eval("PostIt").ToString(), 30)  %>
                </a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
