<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="JournalControl.ascx.cs"
    Inherits="OliWeb.Controls.Floor.Journal.JournalControl" EnableViewState="True" %>
<%@ Import Namespace="OliEngine" %>
<asp:Panel runat="server" ID="PanelLinesAndFilter" Style="padding: 10px; margin-top: 10px;
    font-weight: bold; background-color: #f1f1f1; border: solid 1px #cccccc;">
    <div style="float:right;">
        
        <asp:TextBox ID="ZeilenTextBox" Font-Size="XX-Small" Width="36" runat="server">20</asp:TextBox>
    </div>
    <div>
        
        <asp:Button ID="AlleButton" Text="*" runat="server" OnClick="AlleButton_Click">
        </asp:Button>
        <asp:Button ID="StammButton" Text="<%$ Resources:SAPCT, S %>" runat="server" OnClick="StammButton_Click">
        </asp:Button>
        <asp:Button ID="PostItButton" Text="<%$ Resources:SAPCT, P %>"  runat="server" OnClick="PostItButton_Click">
        </asp:Button>
        <asp:Button ID="AnglerButton" Text="<%$ Resources:SAPCT, A %>" runat="server" OnClick="AnglerButton_Click">
        </asp:Button>
        <asp:Button ID="TopLabButton" Text="<%$ Resources:SAPCT, T %>" runat="server" OnClick="TopLabButton_Click">
        </asp:Button></div>
</asp:Panel>
<hr/>
<asp:DataGrid ID="JournalDataGrid" runat="server" ShowHeader="false" AllowSorting="True"
    GridLines="None" CellPadding="0" BorderWidth="0" CellSpacing="0" DataKeyField="Zeichen"
    AutoGenerateColumns="False">
    <Columns>
        <asp:BoundColumn DataField="Zeichen" ItemStyle-VerticalAlign="Top" Visible="false">
        </asp:BoundColumn>
        <asp:TemplateColumn SortExpression="Wert">
            <ItemTemplate>
                <asp:Panel runat="server" ID="PanelItem">
                    <div class="meta">
                        <asp:Label ID="Label1" runat="server" Text='<%# OliUtil.MakeDateTimeDiff(Eval("Datum")) %>'
                            CssClass="datum">
                        </asp:Label></div>
                    <asp:Image ImageUrl='<%# OliUtil.MakeImageSrc(Eval("Datei").ToString()) %>' AlternateText='Journal image'
                        Style="width: 50px; float: left;" runat="server" ID="ImageJournal" Visible='<%# Eval("Datei").ToString().Length > 0 %>' />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/" + SubDir(Eval("zeichen").ToString()) + "/" + Eval("guid") + ".aspx" %>'>

                        <strong>
                            <%# Eval("Titel") %>
                        </strong>
                        <%# OliUtil.MakeHtmlLineBreak(OliUtil.FirstXWords(Eval("Wert").ToString(), 42)) %>
                    </asp:HyperLink></asp:Panel>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
