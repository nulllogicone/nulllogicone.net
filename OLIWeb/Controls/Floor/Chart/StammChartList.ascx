<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammChartList.ascx.cs"
    Inherits="OliWeb.Controls.Floor.Chart.StammChartList" %>
<asp:Repeater ID="StammRepeater" runat="server">
    <ItemTemplate>
        <div  class="Stamm" >
            <div class="meta" >
                <div class="boundKooK">
                    <%# Eval("KooK","{0:f2}") %></div>
            </div>
            <div>
                <a href='<%# "~/S/" + Eval("StammGuid") + ".aspx" %>' >
                    <asp:Image runat="server" ID="ImageStamm" ImageUrl='<%# OliEngine.OliUtil.MakeImageSrc( Eval("Datei").ToString()) %>'
                        Visible='<%# Eval("Datei").ToString().Length > 0 %>' AlternateText='Stamm image'
                        Style="width: 80px; border: solid 1px #c1c1c1; margin-right: 10px; float: left;" />
                    <asp:Panel runat="server" ID="PanelImagePlaceholder" Style="width: 80px; min-height: 40px;
                        border: solid 1px #c1c1c1; margin-right: 10px; float: left;" Visible='<%# Eval("Datei").ToString().Length == 0 %>'>
                    </asp:Panel>
                    <asp:Label runat="server" ID="LabelStamm" Text='<%# Eval("Stamm") %>'></asp:Label>
                    <asp:Label runat="server" ID="LabelBeschreibung" Text='<%# Eval("Beschreibung") %>' Style="font-size: 0.75em;"></asp:Label></a> 
            </div>
        </div>
        <br />
    </ItemTemplate>
</asp:Repeater>
