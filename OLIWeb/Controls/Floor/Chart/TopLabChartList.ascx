<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="TopLabChartList.ascx.cs"
    Inherits="OliWeb.Controls.Floor.Chart.TopLabChartList" %>
<asp:Repeater ID="TopLabRepeater" runat="server">
    <ItemTemplate>
        <div>
            <div class="meta" style="margin-right: 5px; margin-top: 2px;">
                <div class="boundKooK">
                    <%# Eval("Lohn","{0:f2}") %></div>
            </div>
            <div class="TopLab">
                <a href='<%# "~/T/" + Eval("TopLabGuid") + ".aspx" %>'>
                    <asp:Image ImageUrl='<%# OliEngine.OliUtil.MakeImageSrc(Eval("Datei").ToString()) %>'
                        AlternateText='TopLab image' Style="width: 50px;" runat="server" ID="ImageTopLab"
                        Visible='<%# Eval("Datei").ToString().Length > 0 %>' />
                    <strong>
                        <%# Eval("Titel") %></strong>
                    <%# Eval("TopLab") %>
                </a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
