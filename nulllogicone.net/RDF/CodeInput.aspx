<%@ Register TagPrefix="uc1" TagName="PostItAnglerGrid" Src="../Controls/Koerper/ViewGrids/PostItAnglerGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../HeaderControl.ascx" %>

<%@ Page Language="c#" ValidateRequest="false" CodeBehind="CodeInput.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.RDF.CodeInput" %>

<%@ Register TagPrefix="uc1" TagName="AjaxWortraumControl" Src="../Controls/AjaxWortraum/AjaxWortraumControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/CodeInput</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <p>
            <uc1:HeaderControl id="HeaderControl1" runat="server"></uc1:HeaderControl>
        </p>
        <h1>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="default.aspx">IO.</asp:HyperLink>Code 
				Input</h1>
        <p>
            URL<br>
            <asp:TextBox ID="UrlTextBox" runat="server" Width="528px">http://nulllogic.de/rdf/code1.rdf</asp:TextBox>
            <asp:Button ID="GetRdfButton" runat="server" Text="get RDF" OnClick="GetRdfButton_Click"></asp:Button>
        </p>
        <p>
            <asp:TextBox ID="InputTextBox" runat="server" TextMode="MultiLine" Wrap="False" Height="250px"
                Width="100%"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="ParseButton" runat="server" Text="Parse RDF" OnClick="ParseButton_Click"></asp:Button>
        </p>
        <p>
            <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
                <tr>
                    <td style="width: 259px">
                        <asp:CheckBox ID="DataGridCheckBox" runat="server" Text="show DataGrid View"></asp:CheckBox><br>
                        <asp:CheckBox ID="WortraumCheckBox" runat="server" Text="show Wortraum View"></asp:CheckBox><br>
                        <asp:CheckBox ID="SaveCheckBox" runat="server" Text="save in Database"></asp:CheckBox><br>
                        <asp:CheckBox ID="MatchCheckBox" runat="server" Text="show matching Angler"></asp:CheckBox></td>
                    <td valign="top">
                        <asp:Label ID="MsgLabel" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                </tr>
            </table>
            <br>
        </p>
        <p>
            <asp:Panel ID="DataGridPanel" runat="server" Visible="False">
        </p>
        <h2>DataGrid View</h2>
        <p>
            <asp:DataGrid ID="CodeDataGrid" runat="server" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                BackColor="White" CellPadding="3" GridLines="Horizontal">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
                <ItemStyle ForeColor="#4A3C8C" BackColor="#E7E7FF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="StammGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/Stamm/?" + DataBinder.Eval(Container, "DataItem.StammGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.StammGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="PostItGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/PostIt/?" + DataBinder.Eval(Container, "DataItem.PostItGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.PostItGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="CodeGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/Code/?" + DataBinder.Eval(Container, "DataItem.CodeGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.CodeGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </p>
        <p>
            <asp:DataGrid ID="RingeDataGrid" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                BackColor="White" CellPadding="3" GridLines="Vertical">
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="NetzGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/Netz/?" + DataBinder.Eval(Container, "DataItem.NetzGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.NetzGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="KnotenGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/Knoten/?" + DataBinder.Eval(Container, "DataItem.KnotenGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.KnotenGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="BaumGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/Baum/?" + DataBinder.Eval(Container, "DataItem.BaumGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.BaumGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="ZweigGuid">
                        <ItemTemplate>
                            <a href='<%# "https://nulllogicone.net/Zweig/?" + DataBinder.Eval(Container, "DataItem.ZweigGuid") %>'>
                                <%# DataBinder.Eval(Container, "DataItem.ZweigGuid") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </p>
        <p>
            </asp:Panel>
				<asp:Panel ID="WortraumPanel" runat="server" Visible="False">
        </p>
        <h2>Wortraum View</h2>
        <p>
            <uc1:AjaxWortraumControl id="AjaxWortraumControl1" runat="server"></uc1:AjaxWortraumControl>
        </p>
        </asp:Panel>
			<asp:Panel ID="SavePanel" runat="server" Visible="False">
                <h2>Save in Database</h2>
            </asp:Panel>
        <asp:Panel ID="MatchPanel" runat="server" Visible="False">
            <h2>Matching Angler</h2>
            <uc1:PostItAnglerGrid id="PostItAnglerGrid1" runat="server"></uc1:PostItAnglerGrid>
        </asp:Panel>
        <p>
            <table id="Table2" cellspacing="0" cellpadding="3" align="right" bgcolor="#f1f1f1" border="0">
                <tr>
                    <td style="font-size: 8pt">
                        <p>
                            Diese Seite&nbsp;ist mit Hilfe des&nbsp;<a href="http://www.schemaweb.info/parser/Parser.aspx">VicSoft 
									RDF Parser</a>&nbsp;realisiert worden.
                        </p>
                    </td>
                </tr>
            </table>
        </p>
        <p>&nbsp;</p>
        <p>
            <uc1:FussZeile id="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
