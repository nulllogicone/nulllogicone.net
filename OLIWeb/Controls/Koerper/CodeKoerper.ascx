<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="CodeKoerper.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.CodeKoerper" %>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../Wortraum/WortraumController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControl" Src="../AjaxWortraum/AjaxWortraumControl.ascx" %>
<div class="rechtsobenrund">
    <div>
        <asp:HyperLink ID="ExitHyperLink" CssClass="exitButton" NavigateUrl="~/Sites/PostItSite.aspx?cmd=exitC"
            ToolTip="Markierung schließen" runat="server">&nbsp;</asp:HyperLink><asp:Label ID="QLabel"
                CssClass="q" ToolTip="Sprachabstraktion" runat="server" Font-Size="XX-Small"></asp:Label>
        <asp:TextBox ID="KommentarTextBox" runat="server" Width="480px" Font-Bold="True"
            Font-Size="X-Large" ForeColor="Navy"></asp:TextBox>&nbsp;
        <asp:LinkButton ID="FischenImageButton" ToolTip="Nachricht verschicken" runat="server" CssClass="MatchButton"
           OnClick="FischenImageButton_Click">
           <asp:image imageurl="~/images/Ringe_trans.gif" runat="server" /> match
        </asp:LinkButton></div>
    <p>
        <asp:Panel ID="WortraumControllerPanel" runat="server">
            <uc1:WortraumController ID="WortraumController1" runat="server"></uc1:WortraumController>
            <uc1:AjaxWortraumControl ID="AjaxWortraumControl1" runat="server"></uc1:AjaxWortraumControl>
        </asp:Panel>
    </p>
    <div>
        <script type="text/javascript">
            function toggleShortCuts() {
                $("#DivShortCuts").toggle("slow");
            }
    </script>
        <h4 onclick="toggleShortCuts()" style="cursor: pointer; text-decoration: underline;">ShortCuts anzeigen</h4>
        <div id="DivShortCuts" style="display: none">
            <asp:DataGrid ID="ShortCutsDataGrid" runat="server" Font-Size="Smaller" Width="250px"
                BorderStyle="None" GridLines="Horizontal" DataKeyField="ShortCutsGuid" CellPadding="0"
                BorderWidth="1px" BorderColor="#DEBA84" AutoGenerateColumns="False">
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="FloralWhite"></SelectedItemStyle>
                <EditItemStyle BackColor="FloralWhite"></EditItemStyle>
                <ItemStyle ForeColor="#8C4510"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#A55129"></HeaderStyle>
                <FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="Favoriten Markierungen">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="select" ForeColor="Sienna"
                                Font-Bold="True">
										<%# Eval("ShortCut") %>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="ShortCutsTextBox" runat="server" Width="200" Text='<%# Eval("ShortCut") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" Checked='<%# Eval("auto") %>'>
                            </asp:CheckBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <div style="text-align: right">
                                <asp:LinkButton ID="Linkbutton12" title="Diesen ShortCut in oben ausgewählten Code übernehmen"
                                    runat="server" CssClass="GridButton" Text="copy" CommandName="set"></asp:LinkButton></div>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <asp:Button ID="CancelButton" runat="server" Visible="False" Text="cancel" OnClick="CancelButton_Click">
        </asp:Button>
    </div>
</div>
